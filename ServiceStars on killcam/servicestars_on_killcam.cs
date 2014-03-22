/* https://forum.myrcon.com/showthread.php?7383-Insane-Limits-Service-Stars-V0-2
First limit

OnAnyChat
Expression */
player.LastChat.StartsWith("@ss")
/* Code: */
if (!player.Data.issetBool("NoYell")) {
	player.Data.setBool("NoYell", false);
	plugin.SendPlayerYell(player.Name, plugin.R ("\nMessages on"),5);
	return false;
	}

if(player.Data.getBool("NoYell")) {
player.Data.setBool("NoYell", false);
plugin.SendPlayerYell(player.Name, plugin.R ("\nMessages on"),5);
}
else {
player.Data.setBool("NoYell", true);
plugin.SendPlayerYell(player.Name, plugin.R ("\nMessages off"),5);
}
return false;
/* Second Limit

OnDeath
Code: */
if (!player.Data.issetBool("NoYell"))
	player.Data.setBool("NoYell", true);

if (player.Data.getBool("NoYell")) 
	return false;
else {
	if (killer.Name != null) {
		BattlelogWeaponStatsInterface WeaponStats = killer.GetBattlelog(kill.Weapon);			
		if (WeaponStats != null) {
		double WeaponTotalKills = WeaponStats.Kills; 
			if (WeaponTotalKills > 0) {
			double WeaponUsagePercentage = Convert.ToInt32(WeaponTotalKills/killer.Kills*100);
			double ServiceStars = Convert.ToInt32(WeaponTotalKills/100);
			plugin.SendPlayerYell(player.Name, plugin.R ("\nKiller: " +killer.Name+ " with " + (plugin.FriendlyWeaponName(kill.Weapon).Name) + "\nService Stars: " + ServiceStars + "\n" + WeaponUsagePercentage + "% of all "  + killer.Kills + " kills."), 6);
			}
		}
	}				
}

return false;