	/* 	
		Mute on Foreign Language - Google Translate API
		Set limit to evaluate OnAnyChat, set action to None

		Set first_check to this Code:
	*/
	if (player.LastChat.StartsWith("ID_CHAT_") || player.LastChat.StartsWith("/") || player.LastChat.StartsWith("!") || player.LastChat.StartsWith("@") || player.LastChat.StartsWith("?") || player.LastChat.StartsWith(":") || player.LastChat.StartsWith(";"))
    return false;
    String[] words = player.LastChat.Trim().Split(new Char[]{' ','\t',',','.',':','!','?',';','(',')',']','[','"'});
    StringBuilder edited = new StringBuilder();
    foreach (String w in words) {
		// Skip less than 5 characters
		if (w.Length < 5) {
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
	String key = "AIzaSyDQdJ6OoQvzV_JknvHMiJQB5Xfp3UuVHHM";
    bool language = false;
    try {
        WebClient client = new WebClient();
        String json = client.DownloadString("https://www.googleapis.com/language/translate/v2/detect?key=" + key + "&q=" + Result.ToString());
        language = json.Contains("\"language\": \"en\",") || json.Contains("\"language\": \"no\",") || json.Contains("\"language\": \"sv\",") || json.Contains("\"language\": \"da\",");
    } catch (Exception e) {
        plugin.ConsoleWrite("Language check failed! Error: " + e);
    }
	return !language;

	/* Second check code: */
	
	String fancy_time = DateTime.Now.ToString("HH:mm:ss");
	String fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
	double count = limit.Activations(player.Name);
	String msg = "none";
	if (count == 2) {
		msg = plugin.R("ATTENTION %k_n%! Write in ENGLISH/SCANDINAVIAN in chat!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + player.FullName + " tripped the Foreign Language filter! ");
		plugin.Log("Logs/InsaneLimits_foreign_language_api.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	if (count == 3) {
		msg = plugin.R("ATTENTION %k_n%! ENGLISH/SCANDINAVIAN ONLY IN CHAT!");
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + player.FullName + " tripped the Foreign Language filter! ");
		plugin.Log("Logs/InsaneLimits_foreign_language_api.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
	else if (count == 4) {
		msg = plugin.R("/@mute " + player.Name + " not writing in ENGLISH/SCANDINAVIAN in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " using Foreign Language in chat! ");
		plugin.Log("Logs/InsaneLimits_foreign_language_api.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;