/* https://forum.myrcon.com/showthread.php?6894-Insane-Limits-V0-9-R0-!punish-!forgive-Team-Kills&p=99483&viewfull=1#post99483
Create a limit to evaluate OnTeamKill, call it "Last TK Name", leave Action set to None.

Set first_check to this Code: */
String key = "Last_TK";
victim.RoundData.setString(key, killer.Name);
victim.RoundData.setObject(key, (Object)DateTime.Now);
String weapon = plugin.FriendlyWeaponName(kill.Weapon).Name;
plugin.ConsoleWrite("TK log: ^7^b" + victim.Name + "^n was TK by ^b" + killer.Name + " with " + weapon);
plugin.PRoConChat("TK log > " + victim.Name + " was TK by " + killer.Name + " with " + weapon);
plugin.SendPlayerMessage(killer.Name, "You team-killed " + victim.Name + "!"); // CHANGE
plugin.SendPlayerMessage(victim.Name, "You were team-killed by " + killer.Name + ", type !punish to punish."); // CHANGE
return false;
/* Create a limit to evaluate OnAnyChat, call it "TK Punisher", leave Action set to None.

Set first_check to this Code: */
double timeToPunishSeconds = 60; // victim has 60 seconds to punish team killer
String key = "Last_TK";

if (Regex.Match(player.LastChat, @"^\s*[!@#](p$|p\s|punish)", RegexOptions.IgnoreCase).Success) {
    if (!player.RoundData.issetObject(key) || !player.RoundData.issetString(key)) {
        plugin.SendPlayerMessage(player.Name, "There is no team-killer to punish!");
        return false;
    }
    DateTime last = (DateTime)player.RoundData.getObject(key);
    String tker = player.RoundData.getString(key);
    if (DateTime.Now.Subtract(last).TotalSeconds > timeToPunishSeconds) {
        plugin.SendPlayerMessage(player.Name, "More than " + timeToPunishSeconds + " seconds have past since the team kill, it's too late to punish");
        player.RoundData.unsetString(key);
        player.RoundData.unsetObject(key);
        return false;
    }
    // Punishment
    plugin.SendPlayerMessage(player.Name, tker + " will be punished in 5 seconds!");
    plugin.ConsoleWrite("TK log: victim ^7^b" + player.Name + "^n punished ^b" + tker + " for team killing!");
    String msg = player.FullName + " punished you for team-killing and not apologizing!"; // CHANGE
    plugin.SendPlayerMessage(tker, msg);
    plugin.SendPlayerYell(tker, "You have been punished for team-killing " + player.FullName, 10); // CHANGE
    plugin.PRoConChat("ADMIN to " + tker + " > " + msg);
    plugin.KillPlayer(tker, 5);

    player.RoundData.unsetString(key);
    player.RoundData.unsetObject(key);
}
return false;