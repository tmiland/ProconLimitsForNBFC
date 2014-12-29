/* Set the limit to evaluate OnAnyChat and name it "Vote IO". Set the Action to None.

Set first_check to this Expression: */

(Regex.Match(server.Gamemode, @"(Conquest|Rush|SquAddeath)").Success)

/* Set second_check to this Code: */

/* VERSION 0.8/R1 - vote io */
double percent = 50; // CUSTOMIZE: percent of players needed to vote
double timeout = 10.0; // CUSTOMIZE: number of minutes before vote times out
int minPlayers = 16; // CUSTOMIZE: minimum players to enable vote
double minTicketPercent = 20; // CUSTOMIZE: minimum ticket percentage remaining in the round

String kVote = "voteinfantry";
String kVoteResult = "voteinfantry_result";
String kVoteTime = "voteinfantry_time";

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

Match nextMatch = Regex.Match(player.LastChat, @"^\s*!vote\s*i[on]f?a?n?t?r?y?", RegexOptions.IgnoreCase);

/* Bail out if not a proper vote */

if (!nextMatch.Success) return false;

/* Bail out if vote already succeeded/failed this round */

if (server.RoundData.issetBool(kVoteResult)) {
	bool succeeded = server.RoundData.getBool(kVoteResult);
	if (succeeded) {
		msg = "Vote already suceeded, next round will be infantry only!";
	} else {
		msg = "Vote failed this round, try again next round!";
	}
	ChatPlayer(player.Name);
	return false;
}

/* Bail out if round about to end */

if (!server.RoundData.issetObject(kVoteTime) && (server.RemainTicketsPercent(1) < minTicketPercent || server.RemainTicketsPercent(2) < minTicketPercent || (team3.players.Count > 0 && server.RemainTicketsPercent(3) < minTicketPercent) || (team4.players.Count > 0 && server.RemainTicketsPercent(4) < minTicketPercent))) {
	msg = "Round too close to ending to start a vote!";
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

if (!player.RoundData.issetBool(kVote)) player.RoundData.setBool(kVote, true);

if (level >= 2) plugin.ConsoleWrite("^b[Vote IO]^n " + player.FullName + " voted for infantry only");

msg = "You voted to make the next round infantry only!";
ChatPlayer(player.Name);

/* Tally the votes */

int votes = 0;
List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
all.AddRange(team1.players);
all.AddRange(team2.players);
if (team3.players.Count > 0) all.AddRange(team3.players);
if (team4.players.Count > 0) all.AddRange(team4.players);

/* Bail out if too much time has past */

if (!server.RoundData.issetObject(kVoteTime)) {
	server.RoundData.setObject(kVoteTime, DateTime.Now);
	if (level >= 2) plugin.ConsoleWrite("^b[Vote IO]^n vote timer started");
}
DateTime started = (DateTime)server.RoundData.getObject(kVoteTime);
TimeSpan since = DateTime.Now.Subtract(started);

if (since.TotalMinutes > timeout) {
	msg = "Voting time has expired, the vote is cancelled!";
	plugin.SendGlobalMessage(msg);
	plugin.ServerCommand("admin.yell", msg);
	if (level >= 2) plugin.ConsoleWrite("^b[Vote IO]^n vote timeout expired");
	foreach (PlayerInfoInterface can in all) {
		// Erase the vote
		if (can.RoundData.issetBool(kVote)) can.RoundData.unsetBool(kVote);
	}
	server.RoundData.setBool(kVoteResult, false);
	return false;
}

/* Otherwise tally */

foreach(PlayerInfoInterface p in all) {
    if (p.RoundData.issetBool(kVote)) votes++;
}
if (level >= 3) plugin.ConsoleWrite("^b[Vote IO]^n votes = " + votes + " of " + all.Count);

int needed = Convert.ToInt32(Math.Ceiling((double) all.Count * (percent/100.0)));
int remain = needed - votes;

if (level >= 3) plugin.ConsoleWrite("^b[Vote IO]^n " + percent.ToString("F0") + "% needed votes = " + needed);

if (remain > 0) {
	msg = remain + " votes needed to change to IO with " + Convert.ToInt32(Math.Ceiling(timeout - since.TotalMinutes)) + " mins left!";
	plugin.SendGlobalMessage(msg);
	plugin.ServerCommand("admin.yell", msg, "8");
	if (level >= 2) plugin.ConsoleWrite("^b[Vote IO]^n " + msg);
	return false;
}

/* Vote succeeded, set up the next round */

server.RoundData.setBool(kVoteResult, false);
plugin.Data.setBool(kVoteResult, true); // Signal the other limit to change to io

/* BF4 friendly map names, including China Rising, Second Assault, Naval Strike, Dragons Teeth and Final Stand */
Dictionary<String, String> Maps = new Dictionary<String, String>();
Maps.Add("MP_Abandoned", "Zavod 311");
Maps.Add("MP_Damage", "Lancang Dam");
Maps.Add("MP_Flooded", "Flood Zone");
Maps.Add("MP_Journey", "Golmud Railway");
Maps.Add("MP_Naval", "Paracel Storm");
Maps.Add("MP_Prison", "Operation Locker");
Maps.Add("MP_Resort", "Hainan Resort");
Maps.Add("MP_Siege", "Siege of Shanghai");
Maps.Add("MP_TheDish", "Rogue Transmission");
Maps.Add("MP_Tremors", "Dawnbreaker");
// XP1 (China Rising)
Maps.Add("XP1_001", "Silk Road");
Maps.Add("XP1_002", "Altai Range");
Maps.Add("XP1_003", "Guilin Peaks");
Maps.Add("XP1_004", "Dragon Pass");
// XP0 (Second Assault)
Maps.Add("XP0_Caspian", "Caspian Border 2014");
Maps.Add("XP0_Firestorm", "Operation Firestorm 2014");
Maps.Add("XP0_Metro", "Operation Metro 2014");
Maps.Add("XP0_Oman", "Gulf of Oman 2014");
// XP2 (Naval Strike)
Maps.Add("XP2_001", "Lost Islands");
Maps.Add("XP2_002", "Nansha Strike");
Maps.Add("XP2_003", "Wavebreaker");
Maps.Add("XP2_004", "Operation Mortar");
// XP3 (Dragons Teeth)
Maps.Add("XP3_MarketPl", "Pearl Market");
Maps.Add("XP3_Prpganda", "Propaganda");
Maps.Add("XP3_UrbanGdn", "Lumphini Garden");
Maps.Add("XP3_WtrFront", "Sunken Dragon");
// XP4 (Final Stand)
Maps.Add("XP4_Arctic", "Operation Whiteout");
Maps.Add("XP4_SubBase", "Hammerhead");
Maps.Add("XP4_Titan", "Hangar 21");
Maps.Add("XP4_WlkrFtry", "Giants Of Karelia");

/* BF4 friendly game modes */
Dictionary<String, String> Modes = new Dictionary<String, String>();    
Modes.Add("ConquestLarge0", "Conquest Large");
Modes.Add("ConquestSmall0", "Conquest Small");
Modes.Add("RushLarge0", "Rush");
Modes.Add("SquAddeathMatch0", "Squad Deathmatch");
Modes.Add("TeamDeathMatch0", "Team Deathmatch");

String mapName = (Maps.ContainsKey(server.NextMapFileName)) ? Maps[server.NextMapFileName] : server.NextMapFileName;
String modeName = (Modes.ContainsKey(server.NextGamemode)) ? Modes[server.NextGamemode] : server.NextGamemode;

msg = "Vote completed! Next round (" + mapName + "/" + modeName + ") will be infantry only!";
plugin.SendGlobalMessage(msg);
plugin.ServerCommand("admin.yell", msg, "15");
if (level >= 2) plugin.ConsoleWrite("^b[Vote IO]^n " + msg);

return false;