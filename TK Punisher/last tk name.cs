/* Create a limit to evaluate OnTeamKill, call it "Last TK Name", leave Action set to None.

Set first_check to this Code: */
String key = "Last_TK";
victim.RoundData.setString(key, killer.Name);
victim.RoundData.setObject(key, (Object)DateTime.Now);
String weapon = plugin.FriendlyWeaponName(kill.Weapon).Name;
plugin.ConsoleWrite("TeamKill log: ^7^b" + victim.Name + "^n was TeamKilled by ^b" + killer.Name + " with " + weapon);
plugin.PRoConChat("TeamKill log > " + victim.Name + " was TeamKilled by " + killer.Name + " with " + weapon);
plugin.SendPlayerMessage(killer.Name, "You team-killed " + victim.Name + "! Say sry, sorry or my bad to avoid being punished!"); // CHANGE
plugin.SendPlayerMessage(victim.Name, "You were team-killed by " + killer.Name + ", type !p or !pun to punish."); // CHANGE
plugin.SendPlayerYell(killer.Name, "You team-killed " + victim.Name + "! Say sry, sorry or my bad to avoid being punished!", 8); // CHANGE
plugin.SendPlayerYell(victim.Name, "You were team-killed by " + killer.Name + ", type !p or !pun to punish", 8); // CHANGE
return false;