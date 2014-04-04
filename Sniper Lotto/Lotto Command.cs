/* Step 2

Create a limit to evaluate OnAnyChat, call it "Lotto Command". Set Action to None.

Set first_check to this Code: */
/* VERSION 0.9/R7 BF4 */
// CUSTOMIZE
double maxMinutes = 2; // Number of minutes to collect players for the lottery

String kWinners = "Lotto_Winners"; // plugin.RoundData String

if (Regex.Match(player.LastChat, @"(snipers|winners|list)", RegexOptions.IgnoreCase).Success) {
    if (plugin.RoundData.issetString(kWinners)) {
        plugin.SendGlobalMessage("This round's snipers: " + plugin.RoundData.getString(kWinners));
        return false;
    }
}

if (!Regex.Match(player.LastChat, @"^\s*!sniper?", RegexOptions.IgnoreCase).Success) return false;

String msg = "test";

Action<String> ChatPlayer = delegate(String who) {
    plugin.ServerCommand("admin.yell", msg, "15", "player", who);
    plugin.ServerCommand("admin.say", msg, "player", who);
};

String kState = "Lotto_State"; // plugin.RoundData int
String kEntries1 = "Lotto_Entries1"; // Team 1: plugin.RoundData Object (List<String>)
String kEntries2 = "Lotto_Entries2"; // Team 2: plugin.RoundData Object (List<String>)

int state = 0;
if (!plugin.RoundData.issetInt(kState)) {
    plugin.RoundData.setInt(kState, 0); // Waiting for the first entrant
} else {
    state = plugin.RoundData.getInt(kState);
}

// Got another entrant!

if (state == 0) {
    msg = "The sniper rifle lottery will run for " + maxMinutes + " minutes. Type !sniper to enter!";
    plugin.SendGlobalMessage(msg);
    plugin.ServerCommand("admin.yell", msg, "8");
    plugin.ConsoleWrite("^b[Lotto]^n " + msg);
    plugin.RoundData.setInt(kState, 1);
    state = 1;
}

if (state == 2) {
    msg = "The lottery is over for this round. No more entries allowed.";
    ChatPlayer(player.Name);
    return false;
}

// Otherwise, we are State 1: running the lotto and taking names

msg = "You already entered the sniper rifle lottery!";

if (player.TeamId == 1) {
    List<String> entrants1 = null;
    if (!plugin.RoundData.issetObject(kEntries1)) {
        entrants1 = new List<String>();
        plugin.RoundData.setObject(kEntries1, (Object)entrants1);
    } else {
        entrants1 = (List<String>)plugin.RoundData.getObject(kEntries1);
    }
        if (!entrants1.Contains(player.Name)) {
        entrants1.Add(player.Name);
        msg = "You are the #" + entrants1.Count + " entrant in the sniper rifle lottery!";
        }
} else if (player.TeamId == 2) {
    List<String> entrants2 = null;
    if (!plugin.RoundData.issetObject(kEntries2)) {
        entrants2 = new List<String>();
        plugin.RoundData.setObject(kEntries2, (Object)entrants2);
    } else {
        entrants2 = (List<String>)plugin.RoundData.getObject(kEntries2);
    }
        if (!entrants2.Contains(player.Name)) {
        entrants2.Add(player.Name);
        msg = "You are the #" + entrants2.Count + " entrant in the sniper rifle lottery!";
        }
}

ChatPlayer(player.Name);
plugin.ConsoleWrite("^b[Lotto]^n (" + player.Name + "): " + msg);
return false;