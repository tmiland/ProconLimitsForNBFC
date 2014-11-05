/* Punish on racism
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

	List<string> racisms = new List<string>();
	// ### "Nigger" written with spaces ###
	racisms.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[1ilj!])+(\W*[gq&])+(\W*[e3])*(\W*r)+\b(?<!niger)");
	racisms.Add(@"(\|\\\|)+(\W*[1ilj!])*(\W*[gq&])+(\W*[e3])*(\W*r)+\b# |\|igger");
	racisms.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4yi])*\b");
	racisms.Add(@"(\|\\\|)+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4yi])*\b# |\|igga");
	racisms.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[1ilj!])+(\W*[gq&])+(\W*[li1!])+(\W*[3e])*(\W*[dt])+\b");
	racisms.Add(@"(\|\\\|)+(\W*[1ilj!])+(\W*[gq&])+(\W*[li1!])+(\W*[3e])*(\W*[dt])+\b");
	racisms.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[e3])+(\W*[gq&])+(\W*r)+(\W*[o0qc])+(\W*[1ilj])*(\W*[dt])*\b");
	racisms.Add(@"(\|\\\|)+(\W*[e3])*(\W*[gq&])+(\W*r)+(\W*[o0qc])+(\W*[1ilj!])*(\W*[dt])*\b");
	racisms.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[e3])+(\W*[gq&])+(\W*[e3])*(\W*r)+\b");
	racisms.Add(@"(\|\\\|)+(\W*[e3])*(\W*[gq&])+(\W*[e3])*(\W*r)+\b");
	racisms.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[e3])+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4])+r+\b");
	racisms.Add(@"(\|\\\|)+(\W*[e3])+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4])+r+\b");

	// ### "Nigger" contained in a word ###
	racisms.Add(@"((n)|([il1!\|]\\[il1!\|])|(\|\\\|))+[1ilj!]*[gq&]{2,}[e3]*r(?<!pingger)");
	racisms.Add(@"((n)|([il1!\|]\\[il1!\|])|(\|\\\|))+[1ilj!]+[gq&]{2,}[ayi]");
	racisms.Add(@"((n)|([il1!\|]\\[il1!\|])|(\|\\\|))+[1ilj!]+[gq&]+[li1!]+[e3]*[dt]");
	racisms.Add(@"((n)|([il1!\|]\\[il1!\|])|(\|\\\|))+[e3]+[gq&]+r+[o0qc]+");

	// ### "Jigger" written with spaces ###
	racisms.Add(@"\bj+(\W*[1ilj])+(\W*[gq&])+(\W*[e3a4])*(\W*r)+((\W*b)+(\W*[ocq])+)*(\W*[sz])*\b");
	// ### "Jigger" contained in a word ###
	racisms.Add(@"(?!jig+(l|s|ery|ing))j+[1i]+[gq&]+[e3a4]*r*(b+[ocq]+)*[sz]*");
    
	foreach(string racism in racisms)
	{	
		if (Regex.Match(player.LastChat, racism, RegexOptions.IgnoreCase).Success)
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

	if (count == 1) {
		msg = plugin.R("/@punish " + player.Name + " You have been Punished for racism in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN PUNISH > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN PUNISH >^0^n " + player.FullName + " has been Punished for racism! ");
		plugin.Log("Logs/InsaneLimits_racism.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
		else if (count == 2) {
		msg = plugin.R("/@mute " + player.Name + " You have been Muted for using racism in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been Muted for racism! ");
		plugin.Log("Logs/InsaneLimits_racism.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;