/* Punish and Mute on Insults
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */

	List<string> insults = new List<string>();
	// Matching you/she/he/we/they/it suck/sucks
	insults.Add(@"((yo)?u|s?he|we|they|it) sucks?");
	insults.Add(@"((yo)?u) (suck|stink|mother?)(fuck(er))(s?)");
	// Matching fuck you/her/him/them/it
	insults.Add(@"fuck ((yo)?u|h(er|im)|them|it)");
	// Matching you fucking
	insults.Add(@"you f+[u|ck]*ing [a-zA-Z]+");
	/* Matching you **** fuck */
	insults.Add(@"you [a-zA-Z]+ f+[u]ck");
	// Matching fucking and any word after
	//insults.Add(@"f+[u|ck]*ing [a-zA-Z]+");
	// Matching you fucking with spaces
	insults.Add(@"you f\W*([uc]\W*)+\W*([k]\W*)+\W*([i]\W*)+\W*([n]\W*)+\W*([g]\W*)");
	// # fag contained in a word
	insults.Add(@"f[a4]+[gq&]");
	//# written with spaces
	insults.Add(@"\bf\W*([a4]\W*)+[gq&]\b");
	//# variations like "fgt" or "fegit"
	insults.Add(@"\bf[a4e3]*g+[a4o0ile3]*t(?<!fget)\b");
	// # gay contained in a word
	insults.Add(@"g[a4]+[y]");
	//# written with spaces
	insults.Add(@"\bg\W*([a4]\W*)+[y]\b");
	// cunt contained in a word
	insults.Add(@"c[u]+[n]+[t]");
	// cunt written with spaces
	insults.Add(@"\bc\W*([u]\W*)+([n]\W*)+[t]\b");
	// kuk contained in a word
	insults.Add(@"k[u]+[k]");
	// kuk written with spaces
	insults.Add(@"\bc\W*([u]\W*)+([n]\W*)+[t]\b");
	// dick contained in a word
	insults.Add(@"d[i]+[c]+[k]");
	// dick written with spaces
	insults.Add(@"\bd\W*([i]\W*)+([c]\W*)+[k]\b");
	// cock contained in a word
	insults.Add(@"c[o]+[c]+[k]");
	// cock written with spaces
	insults.Add(@"\bc\W*([o]\W*)+([c]\W*)+[k]\b");
        
	foreach(string insult in insults)
	{	
		if (Regex.Match(player.LastChat, insult, RegexOptions.IgnoreCase).Success)
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
		msg = plugin.R("/@punish " + player.Name + " insults in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN PUNISH > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN PUNISH >^0^n " + player.FullName + " has been Punished for insults! ");
		plugin.Log("Logs/InsaneLimits_insults.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}
		else if (count == 2) {
		msg = plugin.R("/@mute " + player.Name + " insults in chat!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN MUTE > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN MUTE >^0^n " + player.FullName + " has been Muted for insults! ");
		plugin.Log("Logs/InsaneLimits_insults.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% said: [" + player.LastChat + "]"));
	}

	return false;