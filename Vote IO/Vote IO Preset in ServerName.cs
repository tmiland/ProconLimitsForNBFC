/* Set the limit to evaluate OnRoundOver and name it "Vote IO Preset in ServerName". Set the Action to None.

Set first_check to this Code: */
if (!server.Data.issetString("RememberPreset"))
    return false; // skip if not set yet
String preset = server.Data.getString("RememberPreset");
if (preset == "NORMAL") {
		plugin.ServerCommand("vars.serverName", "[NBFC #1] 24/7 CQ | Normal | All Maps");
} else if (preset == "INFANTRY") {
		plugin.ServerCommand("vars.serverName", "[NBFC #1] 24/7 CQ | Infantry Only | All Maps");
}