/* Mute on Profanity - https://forum.myrcon.com/showthread.php?7737-Insane-Limits-Mute-on-Profanity
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

	string[] chat_words = Regex.Split(player.LastChat, @"\s+");

	foreach(string chat_word in chat_words)
	{
		if (plugin.isInList(chat_word, "bad_words_list")) {
			return true;
		} else
		if (plugin.isInList(chat_word, "bad_words_list_no")) {
			return true;
		}
	}
	return false;

	/* Set second_check to this Code: */

	string fancy_time = DateTime.Now.ToString("HH:mm:ss");
	string fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
	double count = limit.Activations(player.Name);
	String msg = "none";
	if (count == 1) {
		msg = plugin.R("ATTENTION %k_n%! Please avoid using profanity, you will be muted for the rest of the round!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " tripped the profanity filter! ");
		plugin.Log("Logs/InsaneLimits_bad_words_"+ player.Name +".log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	else if (count == 2) {
		msg = plugin.R("/@mute " + player.Name + " You have been muted for using profanity in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been muted for profanity! ");
		plugin.Log("Logs/InsaneLimits_bad_words_"+ player.Name +".log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;