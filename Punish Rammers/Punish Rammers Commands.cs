/* Second Limit
First Check
 
Expression */
player.LastChat.Contains("rammer")
/* Second Check

Code: */
	string fancy_time = DateTime.Now.ToString("HH:mm:ss");
	string fancy_date = DateTime.Now.ToString("dd-MM-yyyy");

if(!server.RoundData.issetDouble("#" + player.Name))		// Player has not been killed by "SoldierCollision"
	return false;

double Dinterval = player.TimeRound - server.RoundData.getDouble("#" + player.Name);

if(Dinterval > 30.0)
	return false;
else {
    plugin.KillPlayer(server.RoundData.getString("#" + player.Name));
	plugin.SendPlayerMessage(player.Name, server.RoundData.getString(player.Name) + " has been punished for ramming!");
	plugin.SendPlayerMessage(server.RoundData.getString(player.Name), player.Name + " has punished you for ramming!");
	server.RoundData.unsetDouble("#" + player.Name);
	plugin.Log("Logs/InsaneLimits_rammers.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% has been punished for ramming by [" + player.Name + "]"));
	}

return false;