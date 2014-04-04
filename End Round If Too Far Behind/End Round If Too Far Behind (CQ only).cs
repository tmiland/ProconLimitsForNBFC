/* End Round If Too Far Behind
Create a limit to evaluate OnIntervalServer. Set the interval to 30 seconds. Set the Action to None.

Set first_check to this Expression: */
(!Regex.Match(server.Gamemode, @"(?:Squad|Gun)").Success && server.PlayerCount <= 32) //Less than 32 players on a 64 slot server.

/* Set second_check to this Code: */
/* Version 0.8/R7 */
/* CUSTOMIZE: */
double maxTicketDifference = 500;
double earlyWarning = (maxTicketDifference-100); // Give a warning at a lower ticket difference
double maxMinutes = 3;
Dictionary<String,String> maxDiffForMap = new Dictionary<String, String>(); // don't touch this
/* Values are "EXACT map file name", "maxTicketDifference/earlyWarning" */
//maxDiffForMap.Add("MP_Subway", "600/550"); // For Metro, max difference is 600, warn at 550
//maxDiffForMap.Add("MP_001", "400/300"); // For Bazaar, max difference is 400, warn at 300
// Add additional maps here ...


int level = 2;

try {
	level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
} catch (Exception e) {}



String exactMap = server.MapFileName;
if (maxDiffForMap.ContainsKey(exactMap)) {
	String[] val = maxDiffForMap[exactMap].Split('/');
	maxTicketDifference = Convert.ToDouble(val[0]);
	earlyWarning = Convert.ToDouble(val[1]);
}

if (level >= 4) plugin.ConsoleWrite("^b[Mercy]^n For map file " + exactMap + " max ticket difference is " + maxTicketDifference + " with warning at " + earlyWarning);


// Sanity check
if (maxTicketDifference <= earlyWarning) {
    plugin.ConsoleError("^b[Mercy]^n  maxTicketDifference: " + maxTicketDifference + " must be more than earlyWarning: " + earlyWarning + ". Fix your limit customizations!");
    return false;
}


String kTime = "Mercy_time"; // server.RoundData Object
String kComeback = "Mercy_comeback"; // Losing team made a comeback, disable for round
String kWarning = "Mercy_warning"; // Warning sent

if (Double.IsNaN(server.RemainTickets(1)) || Double.IsNaN(server.RemainTickets(2)) || server.TimeRound < (5*60)) {
    if (level >= 3 && (server.RoundData.issetObject(kTime) || plugin.RoundData.issetObject(kTime) || server.RoundData.issetBool(kComeback) || server.RoundData.issetBool(kWarning))) plugin.ConsoleWrite("^b[Mercy]^n ***** resetting all variables!");
    if (server.RoundData.issetObject(kTime)) server.RoundData.unsetObject(kTime);
    if (plugin.RoundData.issetObject(kTime)) plugin.RoundData.unsetObject(kTime);
    if (server.RoundData.issetBool(kComeback)) server.RoundData.unsetBool(kComeback);
    if (server.RoundData.issetBool(kWarning)) server.RoundData.unsetBool(kWarning);
    return false;
}

double diff = Math.Abs(server.RemainTickets(1) - server.RemainTickets(2));

if (level >= 3 && diff >= (maxTicketDifference-100)) {
    plugin.ConsoleWrite("^b[Mercy]^n " + server.MapFileName + " round " + (server.CurrentRound+1)  + " difference is " + diff);
    if (server.RoundData.issetObject(kTime)) {
        DateTime from = (DateTime)server.RoundData.getObject(kTime);
        plugin.ConsoleWrite("^b[Mercy]^n kTime since is " + (DateTime.Now-from).TotalMinutes.ToString("F2") + " minutes");
    }
    plugin.ConsoleWrite("^b[Mercy]^n Warning X sent? " + (server.RoundData.issetBool(kWarning)));
    plugin.ConsoleWrite("^b[Mercy]^n Server comeback set? " + (server.RoundData.issetBool(kComeback)));
}

if (server.RoundData.issetBool(kComeback)) {
    if (!plugin.RoundData.issetBool(kComeback)) {
        if (level >= 1) plugin.ConsoleWrite("^b[Mercy]^n Comeback flag is set, no mercy this round!");
        plugin.RoundData.setBool(kComeback, true);
    }
    return false;
}

if (diff < maxTicketDifference) {
    if (server.RoundData.issetObject(kTime)) {
        plugin.SendGlobalMessage("Nice comeback! The end of round has been cancelled!");
        if (level >= 2) plugin.ConsoleWrite("^b[Mercy]^n Setting comeback flag");
	server.RoundData.unsetObject(kTime);
	server.RoundData.setBool(kComeback, true); // disable for remainder of round
    } else if (diff >= earlyWarning && !server.RoundData.issetBool(kWarning)) {
	String warn = "BE ADVISED: A ticket lead of " + maxTicketDifference + " or more will end the round automatically";
        plugin.SendGlobalMessage("*** " + warn);
        plugin.ServerCommand("admin.yell", warn, "10");
        if (level >= 2) plugin.ConsoleWrite("^b[Mercy]^n WARNING SENT for tickets: " + server.RemainTickets(1) + " vs " + server.RemainTickets(2) + " is " + diff + " > " + earlyWarning);
        server.RoundData.setBool(kWarning, true);
    }
    return false;
}

if (!server.RoundData.issetObject(kTime) && (server.RemainTicketsPercent(1) < 10 || server.RemainTicketsPercent(2) < 10)) {
        if (level >= 2) plugin.ConsoleWrite("^b[Mercy]^n Less than 10% of tickets remain, no mercy ending will be scheduled.");
        server.RoundData.setBool(kComeback, true); // overload the use of this flag for this edge case
}

// Otherwise fall thru, the max ticket difference has been exceeded

if (level >= 2) plugin.ConsoleWrite("^b[Mercy]^n tickets: " + server.RemainTickets(1) + " vs " + server.RemainTickets(2) + " is " + diff + " > " + maxTicketDifference);

DateTime since = DateTime.Now;
if (!server.RoundData.issetObject(kTime)) {
	server.RoundData.setObject(kTime, (Object)since);
} else {
	since = (DateTime)server.RoundData.getObject(kTime);
}

TimeSpan elapsed = DateTime.Now - since;

double timeLeft = Math.Ceiling(maxMinutes - elapsed.TotalMinutes);

if (level >= 2) plugin.ConsoleWrite("^b[Mercy]^n " + timeLeft.ToString("F0") + " minutes left");

int winner = (server.RemainTickets(1) > server.RemainTickets(2)) ? 1 : 2;
if (timeLeft <= 0) {
	if (level >= 1) plugin.ConsoleWrite("^b[Mercy]^n ending round with winning team " + winner);
	plugin.ServerCommand("mapList.endRound", winner.ToString());
	return false;
}

String tn = (winner == 1) ? "US" : "RU";
String ls = (winner == 1) ? "RU" : "US";
// FOR BF4
if (server.GameVersion == "BF4") {
    String[] factionNames = new String[]{"US", "RU", "CN"};
    int f1 = (team1.Faction < 0 || team1.Faction > 2) ? 0 : team1.Faction;
    int f2 = (team2.Faction < 0 || team2.Faction > 2) ? 1 : team2.Faction;
    tn = (winner == 1) ? factionNames[f1] : factionNames[f2];
    ls = (winner == 1) ? factionNames[f2] : factionNames[f1];
}
String msg = timeLeft.ToString("F0") + " mins until " + tn + " declared winner! Team " + ls + " comeback will cancel!";

plugin.SendGlobalMessage("*** " + msg);
plugin.ServerCommand("admin.yell", msg, "15");
return false;