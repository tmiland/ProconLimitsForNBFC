/* Create a new limit to evaluate OnIntervalServer, 300, call it "Next Map In Server Name".

Set first_check to this Code: */
string mapName = plugin.FriendlyMapName(server.NextMapFileName);
string modeName = plugin.FriendlyModeName(server.NextGamemode);
string ServerName = "[NBFC #1] 24/7 " + modeName + " | " + mapName;

	// Set it
	plugin.SendGlobalMessage("Setting Next Map Server Name: " + ServerName);
	plugin.ServerCommand("vars.serverName", ServerName);
	plugin.ConsoleWrite("^b^1SERVER NAME >^0^n " + ServerName);
	plugin.PRoConChat("^b^1SERVER NAME >^0^n " + ServerName);
	plugin.PRoConEvent(ServerName, "Insane Limits");
	return false;