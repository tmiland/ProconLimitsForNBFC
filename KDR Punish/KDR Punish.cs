/* KDR Punish
First check, OnKill, Expression:  */
(server.PlayerCount <= 12 && player.KdrRound > 10 )
/* Second Check, Code: */
if ( limit.Activations(player.Name, TimeSpan.FromSeconds(2)) >  1) 
	return false;

String kPrefix = "KDRWhore_"; // plugin.Data int
 
	// How many times have we warned this guy?
	int warnings = 1;
	String key = kPrefix + player.Name;
	String msg = "none";
	if (plugin.Data.issetInt(key)) warnings = plugin.Data.getInt(key);

	if (warnings <= 3) {
		// First warning
		msg = "Your kill rate is too high, server is in populating mode! READ THE !RULES"; // CUSTOMIZE
		plugin.SendPlayerYell(player.Name, msg, 15);
		plugin.SendGlobalMessage(player.FullName + ": " + msg);
		plugin.PRoConChat("ADMIN (psay to "+player.FullName+") > " + msg);
	} else {
		// It's punishing time!
		msg = plugin.R("/@punish " + player.Name + " has been PUNISHED for kill rate is too high!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 15);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN PUNISH > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN PUNISH >^0^n " + player.FullName + " has been PUNISHED for kill rate is too high! ");
	}
	warnings = warnings + 1;
	plugin.Data.setInt(key, warnings);

return false;