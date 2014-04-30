/* Mute on Cyrillic Letters
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

	String[] chat_words = Regex.Split(player.LastChat, @"");

	foreach(string chat_word in chat_words)
	{
	if (String.IsNullOrEmpty(chat_word))
		{
			return true;
		}
	}
	return false;

	/* Set second_check to this Code: */

	String fancy_time = DateTime.Now.ToString("HH:mm:ss");
	String fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
	double count = limit.Activations(player.Name);
	String msg = "none";

	if (count == 1) {
		msg = plugin.R("ATTENTION %k_n%! Please avoid Cyrillic, you will be muted for the rest of the round!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " tripped the Cyrillic filter! ");
		plugin.Log("Logs/InsaneLimits_cyrillic.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	else if (count == 2) {
		msg = plugin.R("/@mute " + player.Name + " You have been muted for Cyrillic letters in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been muted for Cyrillic letters in chat! ");
		plugin.Log("Logs/InsaneLimits_cyrillic.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;