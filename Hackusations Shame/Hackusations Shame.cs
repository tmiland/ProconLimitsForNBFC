/* Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

List<String> hackusations = new List<String>();
	// Matching hack, hacker, hacking, hacks, wallhack
	hackusations.Add(@"\b(wall)?hack(er|ing|ed|s)?\b");
	// Matching cheat, cheater, cheating, cheats
	hackusations.Add(@"\bcheat(er|ing|ed|s)?\b");
	// Matching exploit, exploiter, exploiting, exploits
	hackusations.Add(@"\bexploit(er|ing|ed|s)?\b");
	// Matching glitch, glitcher, glitching
	hackusations.Add(@"\bglitch(er|ing|ed)?\b");
    
	String[] chat_words = Regex.Split(player.LastChat, @"[\s!\?\.]+");
    
	foreach(String chat_word in chat_words)
		foreach(String bad_word in hackusations)
			if (Regex.Match(chat_word, "^"+bad_word+"$", RegexOptions.IgnoreCase).Success)
				return true;

	return false;
	
/* Set second check to this code: */

List<String> shame = new List<String>();
shame.Add("%p_n%, hackusations are not welcome on this server!");
shame.Add("%p_n% must have been killed, go cry hackusations to mommy!");
shame.Add("%p_n% go report the hacker on battlelog, dont spam our chat with hackusations.");
shame.Add("Would you like some cheese with your whine about hacking, %p_n%?");
shame.Add("%p_n%, maybe if you played better everyone wouldn't seem like a hack?");
shame.Add("%p_n%, surrounded by hacks who can't possibly play better than you ... really?");
shame.Add("%p_n%, don't go away mad, just go away.");
shame.Add("%p_n%, u mad, bro?");
// Add additional messages here with shame.Add("...");

int level = 2;

try {
	level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
} catch (Exception e) {}

int next = Convert.ToInt32(limit.ActivationsTotal());

next = next % shame.Count; // Ensure rotation of messages
String msg = plugin.R(shame[next]);

/*
To keep a lid on spam, only the first activation per player per
round is sent to all players. Subsequent shames are only sent
to the killer's squad.
*/
bool squadOnly = (limit.Activations(player.Name) > 1);

if (level >= 2) plugin.ConsoleWrite("^b[Hackusation]^n " + ((squadOnly)?"^8private^0: ":"^4public^0: ") + msg + " to " + player.Name + " about: " + player.LastChat);
if (squadOnly) {
	plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
	plugin.ServerCommand("admin.yell", msg, "10", "player", player.Name);
} else {
	plugin.SendGlobalMessage(msg);
	plugin.ServerCommand("admin.yell", msg);
}
plugin.PRoConChat("ADMIN > " + msg);
return false;