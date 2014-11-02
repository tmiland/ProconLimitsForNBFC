/* Punish on racism
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

	List<string> racism = new List<string>();
	// ### "Nigger" written with spaces ###
	racism.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[1ilj!])+(\W*[gq&])+(\W*[e3])*(\W*r)+\b(?<!niger)# nigger");
	racism.Add(@"(\|\\\|)+(\W*[1ilj!])*(\W*[gq&])+(\W*[e3])*(\W*r)+\b# |\|igger");
	racism.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4yi])*\b# nigga / niggy");
	racism.Add(@"(\|\\\|)+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4yi])*\b# |\|igga");
	racism.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[1ilj!])+(\W*[gq&])+(\W*[li1!])+(\W*[3e])*(\W*[dt])+\b# Niglet");
	racism.Add(@"(\|\\\|)+(\W*[1ilj!])+(\W*[gq&])+(\W*[li1!])+(\W*[3e])*(\W*[dt])+\b# |\|iglet");
	racism.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[e3])+(\W*[gq&])+(\W*r)+(\W*[o0qc])+(\W*[1ilj])*(\W*[dt])*\b# negro");
	racism.Add(@"(\|\\\|)+(\W*[e3])*(\W*[gq&])+(\W*r)+(\W*[o0qc])+(\W*[1ilj!])*(\W*[dt])*\b# |\|egro");
	racism.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[e3])+(\W*[gq&])+(\W*[e3])*(\W*r)+\b# neger");
	racism.Add(@"(\|\\\|)+(\W*[e3])*(\W*[gq&])+(\W*[e3])*(\W*r)+\b# |\|eger");
	racism.Add(@"\b((n)|([il1]\\[il1!]))+(\W*[e3])+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4])+r+\b# neigar");
	racism.Add(@"(\|\\\|)+(\W*[e3])+(\W*[1ilj!])+(\W*[gq&])+(\W*[a4])+r+\b# |\|eigar");
	// ### "Jigger" written with spaces ###
	racism.Add(@"\bj+(\W*[1ilj])+(\W*[gq&])+(\W*[e3a4])*(\W*r)+((\W*b)+(\W*[ocq])+)*(\W*[sz])*\b# jigger / jiggaboo");
	// ### "Jigger" contained in a word ###
	racism.Add(@"(?!jig+(l|s|ery|ing))j+[1i]+[gq&]+[e3a4]*r*(b+[ocq]+)*[sz]*# jigger / jiggaboo");
    
	string[] chat_words = Regex.Split(player.LastChat, @"\s+");
    
	foreach(string chat_word in chat_words)
	{	foreach(string insult in racism)
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
		msg = plugin.R("/@punish " + player.Name + " You have been Punished for Racism in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN PUNISH > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN PUNISH >^0^n " + player.FullName + " has been Punished for Racism! ");
		plugin.Log("Logs/InsaneLimits_racism.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;