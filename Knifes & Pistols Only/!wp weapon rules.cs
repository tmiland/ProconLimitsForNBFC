/* Evaluate: OnAnyChat
Set first check to code: */
if ( Regex.Match(player.LastChat, @"\s*[@!#](?:wp)", RegexOptions.IgnoreCase).Success) {

// Weapons here
List<String> Weapons = new List<String>();
Weapons.Add("----- WEAPON RULES -----");
Weapons.Add("All pistols allowed except:");
Weapons.Add("93R, G18 and Shorty 12G");
Weapons.Add("Knife, Flashbang and Defib allowed");
Weapons.Add("Happy hunting! ;-)");
// Weapons End, keep it at 6 lines maximum.

Action<String> WeaponsMethod = delegate(String who) { // how to send the rules
	foreach (string Rule in Weapons) { // set up the way the rules are sent
		plugin.ServerCommand("admin.say", Rule, "player", who);
		plugin.ServerCommand ( "admin.yell" , Rule , "12" , "player" , who);
	}
};

List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
all.AddRange(team1.players);
all.AddRange(team2.players);
if (team3.players.Count > 0) all.AddRange(team3.players);
if (team4.players.Count > 0) all.AddRange(team4.players);

if (!player.LastChat.StartsWith("!wp")) return false; // stop if not chat did not start with the command

bool canKill = false;
bool canKick = false;
bool canBan = false;
bool canMove = false;
bool canChangeLevel = false;
bool hasAccount = plugin.CheckAccount(player.Name, out canKill, out canKick, out canBan, out canMove, out canChangeLevel);

if (!hasAccount)	{ // if not admin, send weapons to the player who issued the command
	WeaponsMethod(player.Name);
	return false;
}

Match RuleCommand = Regex.Match(player.LastChat, @"\s*[@!#](?:wp)", RegexOptions.IgnoreCase);
String RuleAskedByName = RuleCommand.Groups[1].Value;


/* This is the re-usable function that takes a substring and matches it against all the player names */

Converter<String,List<String>> ExactNameMatches = delegate(String sub) {

	List<String> matches = new List<String>();

	if (String.IsNullOrEmpty(sub)) return matches;

	foreach (PlayerInfoInterface p in all) {
		if (Regex.Match(p.Name, sub, RegexOptions.IgnoreCase).Success) {
			matches.Add(p.Name);
		}
	}
	return matches;
};

/* end of function */

// Use the function to find all matches
List<String> RuleAskedBy = ExactNameMatches(RuleAskedByName);
String msg = null;

if (RuleAskedBy.Count == 0) {
	WeaponsMethod(player.Name);
	msg = "No match for: " + RuleAskedByName;
	plugin.ServerCommand("admin.say", msg, "player", player.Name);
	return false;
}

if (RuleAskedBy.Count > 1) {
	msg = @"Try again, '" + RuleAskedByName + @"' matches multiple names: ";
	bool first = true;
	foreach (String b in RuleAskedBy) {
		if (first) {
			msg = msg + b;
			first = false;
		} else {
			msg = msg + ", " + b;
		}
	}
	plugin.ServerCommand("admin.say", msg, "player", player.Name);
	return false;	
}

// Otherwise just one exact match

/* Extract the player name */
PlayerInfoInterface target = plugin.GetPlayer(RuleAskedBy[0], true);

if (target == null) return false;

WeaponsMethod(target.Name);
return false;
}
return false;