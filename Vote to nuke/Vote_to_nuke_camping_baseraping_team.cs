/* Set the limit to evaluate OnAnyChat. Set the Action to None.

Set first_check to this Expression: */
(Regex.Match(server.Gamemode, @"(Conquest|Rush|Domination)").Success)
/* Set second_check to this Code: */
/* VERSION 0.9.15/R6 */
double percent = 50; // CUSTOMIZE: of losing team that has to vote
double timeout = 5.0; // CUSTOMIZE: number of minutes before vote times out
int minPlayers = 16; // CUSTOMIZE: minimum players to enable vote
double minTicketPercent = 10; // CUSTOMIZE: minimum ticket percentage remaining in the round
double minTicketGap = 50; // CUSTOMIZE: minimum ticket gap between winning and losing teams

String kCamp = "votecamp";
String kOncePrefix = "votecamp_once_";
String kVoteTime = "votecamp_time";

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

Match campMatch = Regex.Match(player.LastChat, @"^\s*!vote\s*camp", RegexOptions.IgnoreCase);

/* Bail out if not a proper vote */

if (!campMatch.Success) return false;

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

/* Bail out if voter is not on the losing team */

int losing = (t1 < t2) ? 1 : 2;

if (player.TeamId != losing) {
    msg = "You are not on the losing team!";
    ChatPlayer(player.Name);
    return false;
}

/* Bail out if this team already completed a vote camp this round */

String key = kOncePrefix + losing;
if (server.RoundData.issetBool(key)) {
    msg = "Your team already completed a vote camp this round!";
    ChatPlayer(player.Name);
    return false;
}

/* Bail out if not enough players to enable vote */

if (server.PlayerCount < minPlayers) {
    msg = "Not enough players to hold a vote!";
    ChatPlayer(player.Name);
    return false;
}

/* Count the vote in the voter's dictionary */
/* Votes are kept with the voter */
/* If the voter leaves, his votes are not counted */

if (!player.RoundData.issetBool(kCamp)) player.RoundData.setBool(kCamp, true);

if (level >= 2) plugin.ConsoleWrite("^b[VoteCamp]^n " + player.FullName + " voted to stop camping");

msg = "You voted to stop the other team from camping your deployment";
ChatPlayer(player.Name);

/* Tally the votes */

int votes = 0;
List<PlayerInfoInterface> losers = (losing == 1) ? team1.players : team2.players;

/* Bail out if too much time has past */

if (!server.RoundData.issetObject(kVoteTime)) {
    server.RoundData.setObject(kVoteTime, DateTime.Now);
    if (level >= 2) plugin.ConsoleWrite("^b[VoteCamp]^n vote timer started");
}
DateTime started = (DateTime)server.RoundData.getObject(kVoteTime);
TimeSpan since = DateTime.Now.Subtract(started);

if (since.TotalMinutes > timeout) {
    msg = "The voting time has expired, the vote is cancelled!";
    plugin.SendGlobalMessage(msg);
    plugin.SendGlobalYell(msg, 10);
    if (level >= 2) plugin.ConsoleWrite("^b[VoteCamp]^n vote timeout expired");
    foreach (PlayerInfoInterface can in losers) {
        // Erase the vote
        if (can.RoundData.issetBool(kCamp)) can.RoundData.unsetBool(kCamp);
    }
    server.RoundData.unsetObject(kVoteTime);

    /* Losing team only gets to try this vote once per round */
    server.RoundData.setBool(key, true);

    return false;
}

/* Otherwise tally */

foreach(PlayerInfoInterface p in losers) {
    if (p.RoundData.issetBool(kCamp)) votes++;
}
if (level >= 3) plugin.ConsoleWrite("^b[VoteCamp]^n loser votes = " + votes + " of " + losers.Count);

int needed = Convert.ToInt32(Math.Ceiling((double) losers.Count * (percent/100.0)));
int remain = needed - votes;

if (level >= 3) plugin.ConsoleWrite("^b[VoteCamp]^n needed = " + needed);

String campers = (losing == 1) ? "RU" : "US"; // BF3
String voters = (losing == 1) ? "US" : "RU"; // BF3

if (server.GameVersion == "BF4") {
    String[] factionNames = new String[]{"US", "RU", "CN"};
    int f1 = (team1.Faction == -1 || team1.Faction > 2) ? 0 : team1.Faction;
    int f2 = (team2.Faction == -1 || team2.Faction > 2) ? 1 : team2.Faction;
    campers = (losing == 1) ? factionNames[f2] : factionNames[f1];
    voters = (losing == 1) ? factionNames[f1] : factionNames[f2];
}

if (remain > 0) {
    msg = remain + " " + voters + " votes needed to punish " + campers + " team! " + Convert.ToInt32(Math.Ceiling(timeout - since.TotalMinutes)) + " mins left to vote!";
    plugin.SendGlobalMessage(msg);
    plugin.SendGlobalYell(msg, 8);
    if (level >= 2) plugin.ConsoleWrite("^b[VoteCamp]^n " + msg);
    return false;
}

/* Punish the campers */

msg = "Vote succeeded: " + campers + " team is being nuked for camping your deployment! Break out now!";
plugin.SendTeamMessage(losing, msg);
plugin.SendGlobalMessage(msg, 15);
if (level >= 2) plugin.ConsoleWrite("^b[VoteCamp]^n " + msg);

List<PlayerInfoInterface> bad = (losing == 1) ? team2.players : team1.players;
foreach (PlayerInfoInterface nuke in bad) {
    plugin.KillPlayer(nuke.Name, 1);
    if (level >= 3) plugin.ConsoleWrite("^b[VoteCamp]^n killing " + nuke.FullName);
}

/* Losing team only gets to do this once */

server.RoundData.setBool(key, true);

return false;