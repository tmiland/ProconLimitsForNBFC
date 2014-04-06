/* Mute on Profanity - https://forum.myrcon.com/showthread.php?7737-Insane-Limits-Mute-on-Profanity
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */
	List<String> bad_words = new List<String>();
	//bad_words.Add("BadWord"); // Change This
	// this matches nigger, n1g3r, nig3, etc 
	bad_words.Add(@"n+[1i]+g+[3ea]+r*"); // Change This
	// this matches faggot, f4g0t, fagat, etc
	bad_words.Add(@"f+[a4]+g+[ao0]+t*"); // Change This
	// this matches fag, f4g, fagggg, fa4g, etc.
	bad_words.Add(@"f+[a4]+g+"); // Change This
	// this matches gay, g4y, gayyyy, ga4y, etc.
	bad_words.Add(@"g+[a4]+y+"); // Change This
	// this matches jew, j3w, jeeeew, je3w, etc.
	bad_words.Add(@"j+[e3]+w+"); // Change This
	bad_words.Add(@"dick*"); // Change This
	bad_words.Add(@"cock*"); // Change This

	String[] chat_words = Regex.Split(player.LastChat, @"\s+");

	foreach(String chat_word in chat_words)
		foreach(String bad_word in bad_words)
			if (Regex.Match(chat_word, "^"+bad_word+"$", RegexOptions.IgnoreCase).Success)
				return true;

	return false;

	/* Set second_check to this Code: */

	double count = limit.Activations(player.Name);
	String msg = "none";
	if (count == 1) {
		msg = plugin.R("Please avoid using profanity, you will be muted for the rest of the round!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.PRoConChat("ADMIN MUTE > " + player.FullName + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " tripped the profanity filter! ");
	}
	else if (count == 2) {
		msg = plugin.R("/@mute " + player.Name + " You have been muted for using profanity in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.PRoConChat("ADMIN MUTE > " + player.FullName + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been muted for profanity! ");
	}

	return false;