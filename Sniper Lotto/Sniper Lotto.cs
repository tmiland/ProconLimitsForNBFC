/* Create a limit to evaluate OnKill, call it "Sniper Lotto". Set the Action to None.

Set first_check to this Code: */
/* VERSION 0.9/R8 (BF4) */

// CUSTOMIZE
double maxMinutes = 5; // Number of minutes to collect players for the lottery
int maxSnipers = 3; // Number of snipers per team to choose from the lottery

String msg = "test";

Action<String> ChatPlayer = delegate(String who) {
    plugin.ServerCommand("admin.yell", msg, "15", "player", who);
    plugin.ServerCommand("admin.say", msg, "player", who);
};

// Use kills to update the lotto time limit
// Means it won't be exact

String kState = "Lotto_State"; // plugin.RoundData int
String kTime = "Lotto_Time"; // plugin.RoundData Object (DateTime)
String kEntries1 = "Lotto_Entries1"; // Team 1: plugin.RoundData Object (List<String>)
String kEntries2 = "Lotto_Entries2"; // Team 2: plugin.RoundData Object (List<String>)
String kPrefix = "Lotto_";
String kWinners = "Lotto_Winners"; // plugin.RoundData String
String key = kPrefix + killer.Name;

int state = 0;
if (plugin.RoundData.issetInt(kState)) state = plugin.RoundData.getInt(kState);

//bool sniperRifleUsed = (kill.Category == "SniperRifle");
bool sniperRifleUsed = (kill.Category == "SniperRifle") && !Regex.Match(kill.Weapon, @"(U_AMR2|U_AMR2_CQB|U_AMR2_MED|U_M82A3|U_M82A3_CQB|U_M82A3_MED)").Success;

// State 0: Waiting for first player to type the !sniper command
if (state == 0) {
    if (!sniperRifleUsed) return false;
    // Punish and remind
    msg = "No sniping until after lottery: type !sniper to enter the lottery";
    ChatPlayer(killer.Name); // Remind player
    plugin.KillPlayer(killer.Name, 5);
    plugin.ServerCommand("admin.yell", msg, "8"); // Remind everyone
    return false;
}

// State 1: Running the lotto for maxMinutes
if (state == 1) {
    DateTime start = DateTime.Now;
    if (!plugin.RoundData.issetObject(kTime)) {
        plugin.RoundData.setObject(kTime, (Object)start);
    } else {
        start = (DateTime)plugin.RoundData.getObject(kTime);
    }
    TimeSpan since = DateTime.Now.Subtract(start);
        if (((int)since.TotalSeconds) % 10 == 0) plugin.ConsoleWrite("^b[Lotto]^n Lottery has been running for " + since.TotalSeconds.ToString("F0") + " seconds ...");
    if (since.TotalMinutes < maxMinutes) {
        if (!sniperRifleUsed) return false;
        // Punish and remind
        msg = "No sniping until after lottery: type !sniper to enter the lottery";
        ChatPlayer(killer.Name); // Remind player
        plugin.KillPlayer(killer.Name, 5);
        plugin.ServerCommand("admin.yell", msg, "8"); // Remind everyone
        return false;
    }
    if (!plugin.RoundData.issetObject(kEntries1) && !plugin.RoundData.issetObject(kEntries2)) {
        plugin.ConsoleWrite("^b[Lotto]^n no entrants and time has expired!");
        plugin.RoundData.setInt(kState, 2);
        return false;
    }
    List<String> entrants1 = new List<String>();
        if (plugin.RoundData.issetObject(kEntries1)) entrants1 = (List<String>)plugin.RoundData.getObject(kEntries1);
    List<String> entrants2 = new List<String>();
        if (plugin.RoundData.issetObject(kEntries2)) entrants2 = (List<String>)plugin.RoundData.getObject(kEntries2);
    if (entrants1.Count == 0 && entrants2.Count == 0)  {
        plugin.ConsoleWrite("^b[Lotto]^n no entrants and time has expired!");
        plugin.RoundData.setInt(kState, 2);
        return false;
    }
    msg = "The lottery is over! The winners are:";
    plugin.SendGlobalMessage(msg);
    Random rnd = new Random();
    int pick = maxSnipers;
    // Team 1
    msg = ": may use sniper rifles this round!";
    while (pick > 0 && entrants1.Count > 0) {
        int winner = (entrants1.Count > 2) ? rnd.Next(entrants1.Count) : 0;
        String name = entrants1[winner];
        entrants1.RemoveAt(winner);
        key = kPrefix + name;
        plugin.RoundData.setBool(key, true);
        msg = name + msg;
        --pick;
        if (pick > 0) msg = ", " + msg;
    }
    // Team 2
    pick = maxSnipers;
    if (entrants2.Count > 0) msg = ", " + msg;
    while (pick > 0 && entrants2.Count > 0) {
        int winner = (entrants2.Count > 2) ? rnd.Next(entrants2.Count) : 0;
        String name = entrants2[winner];
        entrants2.RemoveAt(winner);
        key = kPrefix + name;
        plugin.RoundData.setBool(key, true);
        msg = name + msg;
        --pick;
        if (pick > 0) msg = ", " + msg;
    }
    plugin.SendGlobalMessage(msg);
    plugin.ServerCommand("admin.yell", msg, "15");
    plugin.ConsoleWrite("^b[Lotto]^n " + msg);
        plugin.RoundData.setString(kWinners, msg);

    plugin.RoundData.setInt(kState, 2);
    return false;
}

// State 2: Lottery is over, only winners may use sniper rifles
if (state == 2) {
    if (sniperRifleUsed && !plugin.RoundData.issetBool(key)) {
        msg = killer.Name + ": You are not allowed to use sniper rifles this round!";
        ChatPlayer(killer.Name);
        plugin.KillPlayer(killer.Name, 5);
        return false;    
    }
}

return false;