/*
Create a limit to evaluate OnIntervalPlayers, set interval to 30, leave Action set to None.

Set first_check to this Code:
*/
/* Code */
int minimumPlayers = 16; // ping is ignored if fewer than these players
int maximumPing = 200; // highest ping allowed
int maximumCount = 5; // number of times max ping is allowed before kick

String msg = "none";
String key = "PingCounter";

if (server.PlayerCount < minimumPlayers) return false;
if (plugin.isInList(player.Name, "admins")) return false;

if (player.Ping > maximumPing) plugin.ServerCommand("player.ping", player.Name);

if (player.MedianPing > maximumPing) {
	int count = 0;
	if (player.Data.issetInt(key)) count = player.Data.getInt(key);
	count = count + 1;
	player.Data.setInt(key, count);
	if (count > maximumCount) {
		msg = "[Ping Kicker] > %p_n% has been KICKED for high ping.";
		plugin.KickPlayerWithMessage(player.Name, plugin.R("Sorry, but your ping was too high."));
		plugin.PRoConChat(plugin.R(msg));
		plugin.ServerCommand("admin.say", msg);
    }
}
return false;