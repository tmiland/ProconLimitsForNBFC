/* Create a new limit to evaluate OnIntervalServer, 300, call it "Random Server Name".

Set first_check to this Code: */
string modeName = plugin.FriendlyModeName(server.NextGamemode);
string prefix = "[NBFC #1] 24/7 " + modeName + " | All Maps | ";
List<String> name = new List<String>();
	name.Add(prefix + "Noob Friendly");
	name.Add(prefix + "DICE Friends");
	name.Add(prefix + "Free Beer");
	name.Add(prefix + "Free Cake");
	name.Add(prefix + "Fast XP");
	name.Add(prefix + "Free Cookies");
	name.Add(prefix + "Noobs Welcome");
	name.Add(prefix + "NO LAG");
	name.Add(prefix + "Vote Ban");
	name.Add(prefix + "Vote Kick");
	name.Add(prefix + "MERRY Xmas");
	
	// Pick the next tip
	String key = "index for name";
	int i = 0;
	if (!plugin.Data.issetInt(key)) {
		Random r = new Random();
		i = r.Next(name.Count);
	} else {
		i = plugin.Data.getInt(key);
	}
	i = (i + 1) % name.Count;
	plugin.Data.setInt(key, i);

	// Set it
	plugin.SendGlobalMessage("Setting random server name: " + name[i]);
	plugin.ServerCommand("vars.serverName", name[i]);
	plugin.ConsoleWrite("^b^1RANDOM SERVER NAME >^0^n " + name[i]);
	//plugin.PRoConChat("^b^1RANDOM SERVER NAME >^0^n " + name[i]);
	plugin.PRoConEvent(name[i], "Insane Limits");
	return false;