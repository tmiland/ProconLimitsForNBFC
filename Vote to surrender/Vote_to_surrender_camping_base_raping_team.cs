/* https://forum.myrcon.com/showthread.php?4533-Insane-Limits-V0-9-R6-Vote-to-nuke-camping-base-raping-team-or-!surrender-(CQ-Rush)&p=51515&viewfull=1#post51515
Vote to !surrender camping/base raping team
Set the limit to evaluate OnAnyChat. Set the Action to None.

Set first_check to this Expression: */
(Regex.Match(server.Gamemode, @"(Conquest|Rush)").Success)
/* Set second_check to this Code: */
/* VERSION 0.9.15/R8 - surrender */
double percent = 50; // CUSTOMIZE: of losing team that has to vote
double timeout = 5.0; // CUSTOMIZE: number of minutes before vote times out
int minPlayers = 16; // CUSTOMIZE: minimum players to enable vote
double minTicketPercent = 10; // CUSTOMIZE: minimum ticket percentage remaining in the round
double minTicketGap = 50; // CUSTOMIZE: minimum ticket gap between winning and losing teams

String kNext = "votenext";
String kVoteTime = "votenext_time";
String kNeeded = "votenext_needed";

int level = 2;

try {
	level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
} catch (Exception e) {}

String msg = "empty";

Action<String> ChatPlayer = delegate(String name) {
	// closure bound to String msg
	plugin.ServerCommand("admin.say", msg, "player", name);
	plugin.PRoConChat("ADMIN to " + name + " > *** " + msg);
};


/* Parse the command */

Match nextMatch = Regex.Match(player.LastChat, @"^\s*[@!](?:surrender|vote\s*next)", RegexOptions.IgnoreCase);

/* Bail out if not a proper vote */

if (!nextMatch.Success) return false;

/* Bail out if round about to end */

if (server.RemainTicketsPercent(1) < minTicketPercent || server.RemainTicketsPercent(2) < minTicketPercent) {
	msg = "Round too close to ending to hold a vote!";
	ChatPlayer(player.Name);
	return false;
}

/* Bail out if ticket ratio isn't large enough */

double t1 = server.RemainTickets(1);
double t2 = server.RemainTickets(2);
if (Math.Abs(t1 - t2) < minTicketGap) {
	msg = "Ticket counts too close to hold a vote!";
	ChatPlayer(player.Name);
	return false;
}

/* Determine losing team by tickets */

int losing = (t1 < t2) ? 1 : 2;

/* Bail out if not enough players to enable vote */

if (server.PlayerCount < minPlayers) {
	msg = "Not enough players to hold a vote!";
	ChatPlayer(player.Name);
	return false;
}

/* Count the vote in the voter's dictionary */
/* Votes are kept with the voter */
/* If the voter leaves, his votes are not counted */

if (!player.RoundData.issetBool(kNext)) {
	player.RoundData.setBool(kNext, true);
} else {
	msg = "You already voted!";
	ChatPlayer(player.Name);
	return false;
}

if (level >= 2) plugin.ConsoleWrite("^b[VoteNext]^n " + player.FullName + " voted to end the round");

msg = "You voted to end this round and start the next round!";
ChatPlayer(player.Name);

/* Tally the votes */

int votes = 0;
List<PlayerInfoInterface> losers = (losing == 1) ? team1.players : team2.players;
List<PlayerInfoInterface> winners = (losing == 1) ? team2.players : team1.players;

/* Bail out if too much time has past */

bool firstTime = false;

if (!server.RoundData.issetObject(kVoteTime)) {
	server.RoundData.setObject(kVoteTime, DateTime.Now);
	if (level >= 2) plugin.ConsoleWrite("^b[VoteNext]^n vote timer started");
	firstTime = true;
}
DateTime started = (DateTime)server.RoundData.getObject(kVoteTime);
TimeSpan since = DateTime.Now.Subtract(started);

if (since.TotalMinutes > timeout) {
	msg = "The voting time has expired, the vote is cancelled!";
	plugin.SendGlobalMessage(msg);
	plugin.SendGlobalYell(msg, 10);
	if (level >= 2) plugin.ConsoleWrite("^b[VoteNext]^n vote timeout expired");
	foreach (PlayerInfoInterface can in losers) {
		// Erase the vote
		if (can.RoundData.issetBool(kNext)) can.RoundData.unsetBool(kNext);
	}
	foreach (PlayerInfoInterface can in winners) {
		// Erase the vote
		if (can.RoundData.issetBool(kNext)) can.RoundData.unsetBool(kNext);
	}
	server.RoundData.unsetObject(kVoteTime);

	return false;
}

/* Otherwise tally */

foreach(PlayerInfoInterface p in losers) {
    if (p.RoundData.issetBool(kNext)) votes++;
}
if (level >= 3) plugin.ConsoleWrite("^b[VoteNext]^n loser votes = " + votes + " of " + losers.Count);

/* Votes on the winning side are counted as long as they are less than the votes on the losing side */

int losingVotes = votes;
int winningVotes = 0;
foreach(PlayerInfoInterface p in winners) {
    if (p.RoundData.issetBool(kNext)) {
        winningVotes++;
        if (winningVotes > losingVotes) break;
        ++votes;
    }
}

if (level >= 3) plugin.ConsoleWrite("^b[VoteNext]^n winner votes = " + winningVotes + " of " + winners.Count);

int needed = Convert.ToInt32(Math.Ceiling((double) losers.Count * (percent/100.0)));
if (server.RoundData.issetInt(kNeeded)) needed = Math.Min(needed, server.RoundData.getInt(kNeeded));
server.RoundData.setInt(kNeeded, needed);

int remain = needed - votes;

if (level >= 3) plugin.ConsoleWrite("^b[VoteNext]^n needed votes = " + needed);

String voters = (losing == 1) ? "US" : "RU";
String otherVoters = (losing == 1) ? "RU" : "US";

if (remain > 0) {
	if (firstTime) {
		msg = remain + " " + voters + " !surrender or " + otherVoters + " !votenext votes needed to end round with " + Convert.ToInt32(Math.Ceiling(timeout - since.TotalMinutes)) + " minutes left!";
	} else {
		msg = remain + " !surrender/!votenext needed to end round with " + Convert.ToInt32(Math.Ceiling(timeout - since.TotalMinutes)) + " mins left!";
	}
	plugin.SendGlobalMessage(msg);
	plugin.SendGlobalYell(msg, 8);
	if (level >= 2) plugin.ConsoleWrite("^b[VoteNext]^n " + msg);
	return false;
}

/* End the round */

String wteam = (losing == 1) ? "RU" : "US"; // BF3

if (server.GameVersion == "BF4") {
    String[] factionNames = new String[]{"US", "RU", "CN"};
    int f1 = (team1.Faction < 0 || team1.Faction > 2) ? 0 : team1.Faction;
    int f2 = (team2.Faction < 0 || team2.Faction > 2) ? 1 : team2.Faction;
    wteam = (losing == 1) ? factionNames[f2] : factionNames[f1];
}

msg = "Vote succeeded: round ends now, " + wteam + " team wins!";
plugin.SendGlobalMessage(msg);
plugin.SendGlobalYell(msg, 10);
if (level >= 2) plugin.ConsoleWrite("^b[VoteNext]^n " + msg);

String wid = (losing == 1) ? "2" : "1";

ThreadStart roundEnder = delegate {
    Thread.Sleep(10*1000);
    plugin.ServerCommand("mapList.endRound", wid);
};

Thread enderThread = new Thread(roundEnder);
enderThread.Start();
Thread.Sleep(10);

return false;