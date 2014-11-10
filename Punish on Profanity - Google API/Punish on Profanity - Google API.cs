	/* 	
		Punish on Profanity - Google API
		Set limit to evaluate OnAnyChat, set action to None

		Set first_check to this Code:
	*/

    // Exception list is "good_words"
    // Split chat line into words
    String[] words = player.LastChat.Trim().Split(new Char[]{' ','\t',',','.',':','!','?',';','(',')',']','[','"'});
    StringBuilder edited = new StringBuilder();
    // Remove exception list words from line
    foreach (String w in words) {
        if (plugin.isInList(w, "good_words")) {
            continue;
        }
        edited.Append(w);
        edited.Append(" ");
    }

    // URL Encode edited string
    String UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
    StringBuilder Result = new StringBuilder();
    String Input = edited.ToString();

    for (int x = 0; x < Input.Length; ++x)
    {
        if (UnreservedChars.IndexOf(Input[x]) != -1)
            Result.Append(Input[x]);
        else
            Result.Append("%").Append(String.Format("{0:X2}", (int)Input[x]));
    }

    // Test for badness
    bool bad = false;
    try {
        WebClient client = new WebClient();
        String json = client.DownloadString("http://www.wdyl.com/profanity?q=" + Result.ToString());
        bad = json.Contains("true");
    } catch (Exception e) {
        plugin.ConsoleWrite("Profanity check failed! Error: " + e);
    }

    if (bad) {
        return true;
    }

	return bad; // for Actions
	
	/* Second check code: */
	
	string fancy_time = DateTime.Now.ToString("HH:mm:ss");
	string fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
	double count = limit.Activations(player.Name);
	String msg = "none";
	if (count == 1) {
		msg = plugin.R("ATTENTION %k_n%! Watch your language!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + player.FullName + " tripped the Bad Language filter! ");
		plugin.Log("Logs/InsaneLimits_bad_words_api.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	else if (count == 2) {
		msg = plugin.R("/@punish " + player.Name + " using Bad Language in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN PUNISH > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN PUNISH >^0^n " + player.FullName + " using Bad Language in chat! ");
		plugin.Log("Logs/InsaneLimits_bad_words_api.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;