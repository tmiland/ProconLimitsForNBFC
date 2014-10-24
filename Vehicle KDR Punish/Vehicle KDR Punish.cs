/* Vehicle KDR Punish */
/* Version 0.8/R1 */
/* CUSTOMIZE: */

if ( limit.Activations(player.Name, TimeSpan.FromSeconds(2)) >  1) return false;

String kPrefix = "VWhore_"; // plugin.Data int
 
	// How many times have we warned this guy?
	int warnings = 1;
	String key = kPrefix + player.Name;
	String msg = "none";
	if (plugin.Data.issetInt(key)) warnings = plugin.Data.getInt(key);
	if (warnings <= 3) {
		// First warning
		msg = "Your vehicle kill rate is too high, READ THE !RULES!"; // CUSTOMIZE
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(player.FullName + ": " + msg);
		plugin.PRoConChat("ADMIN (psay to "+player.FullName+") > " + msg);
	} else {
		// It's punishing time!
		msg = plugin.R("/@punish " + player.Name + " has been PUNISHED for vehicle whoring!");
		plugin.ServerCommand("admin.say", msg);
		plugin.SendPlayerYell(player.Name, msg, 20);
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN PUNISH > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN PUNISH >^0^n " + player.FullName + " has been PUNISHED for vehicle whoring! ");
	}
	warnings = warnings + 1;
	plugin.Data.setInt(key, warnings);

return false;