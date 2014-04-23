/* Mute on Insults
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

	List<string> insults = new List<string>();
	// Matching you/she/he/we/they/it suck/sucks
	insults.Add(@"\b((yo)?u|s?he|we|they|it) sucks?\b");
	// Matching fuck you/her/him/them/it
	insults.Add(@"\bfuck ((yo)?u|h(er|im)|them|it)\b");
	
	insults.Add(@"\byou ((f)?uck(ing)) [a-zA-Z]+\b");
    
	string[] chat_words = Regex.Split(player.LastChat, @"\s+");
    
	foreach(string chat_word in chat_words)
	{	foreach(string insult in insults)
		{	if (Regex.Match(chat_word, "^"+insult+"$", RegexOptions.IgnoreCase).Success)
			{	
				return true;
			}
		}
	}
	return false;

	/* Set second_check to this Code: */

	string fancy_time = DateTime.Now.ToString("HH:mm:ss");
	string fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
	double count = limit.Activations(player.Name);
	string msg = "none";

	if (count == 1) {
		msg = plugin.R("ATTENTION %k_n%! Please avoid insults, you will be muted for the rest of the round!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " tripped the insults filter! ");
		plugin.Log("Logs/InsaneLimits_insults.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	else if (count == 2) {
		msg = plugin.R("/@mute " + player.Name + " You have been muted for Insults in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been muted for Insults! ");
		plugin.Log("Logs/InsaneLimits_insults.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;