/* Mute on Hackusations - https://forum.myrcon.com/showthread.php?7737-Insane-Limits-Mute-on-Hackusations
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

List<String> hackusations = new List<String>();
	// Matching hack, hacker, haxor, hacking, hacks, wallhack
	hackusations.Add(@"\b(wall)?ha(ck|xo)(er|ing|ed|s|r)?\b");
	// Matching cheat, cheater, cheating, cheats
	hackusations.Add(@"\bche(at)(er|ing|ed|s)(s)?\b");
	// Matching exploit, exploiter, exploiting, exploits
	hackusations.Add(@"\bexploit(er|ing|ed|s)?\b");
	// Matching glitch, glitcher, glitching
	hackusations.Add(@"\bglitch(er|ing|ed)?\b");
	// Matching aimbot
	hackusations.Add(@"\baimbot?\b");
	// Matching reported
	hackusations.Add(@"\breported?\b");

	Match report = Regex.Match(player.LastChat, @"^\s*!report\s+(.*)$", RegexOptions.IgnoreCase);
	
	foreach(String hackusation in hackusations)
	{	
		if (report.Success)
		{
			return false;
		}
		else if (Regex.Match(player.LastChat, "^"+hackusation+"$", RegexOptions.IgnoreCase).Success)
		{	
			return true;
		}
	}
	return false;

	/* Set second_check to this Code: */

	string fancy_time = DateTime.Now.ToString("HH:mm:ss");
	string fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
	double count = limit.Activations(player.Name);
	String msg = "none";
	Action<String> ChatPlayer = delegate(String who) {
	plugin.ServerCommand("admin.yell", msg, "15", "player", who);
	plugin.ServerCommand("admin.say", msg, "player", who);
	};
	if (count == 1) {
		msg = plugin.R("ATTENTION %k_n%! If you suspect a hacker, report him with !report playername reason ");
		ChatPlayer(player.Name);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
	}
	else if (count == 2) {
		msg = plugin.R("ATTENTION %k_n%! Please avoid Hackusations, you will be muted for the rest of the round!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " tripped the Hackusations filter! ");
		plugin.Log("Logs/InsaneLimits_hackusations.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	else if (count == 3) {
		msg = plugin.R("/@mute " + player.Name + " You have been muted for Hackusations in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been muted for Hackusations! ");
		plugin.Log("Logs/InsaneLimits_hackusations.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;