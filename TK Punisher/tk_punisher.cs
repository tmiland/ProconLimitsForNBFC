/* https://forum.myrcon.com/showthread.php?6894-Insane-Limits-V0-9-R0-!punish-!forgive-Team-Kills&p=99483&viewfull=1#post99483
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
    if (last == DateTime.MaxValue) {
        plugin.SendPlayerMessage(player.Name, tker + " apologized, punishment is not allowed"); // CHANGE
        player.RoundData.unsetString(key);
        player.RoundData.unsetObject(key);
        return false;
    }
    if (DateTime.Now.Subtract(last).TotalSeconds > timeToPunishSeconds) {
        plugin.SendPlayerMessage(player.Name, "More than " + timeToPunishSeconds + " seconds have past since the team kill, it's too late to punish");
        player.RoundData.unsetString(key);
        player.RoundData.unsetObject(key);
        return false;
    }
    // Punishment
    plugin.SendPlayerMessage(player.Name, tker + " will be punished in 5 seconds!");
    plugin.ConsoleWrite("TeamKill log: victim ^7^b" + player.Name + "^n punished ^b" + tker + " for team killing!");
    String msg = player.FullName + " punished you for team-killing and not apologizing!"; // CHANGE
    plugin.SendPlayerMessage(tker, msg);
    plugin.SendPlayerYell(tker, "You have been punished for team-killing " + player.FullName, 10); // CHANGE
    plugin.PRoConChat("ADMIN to " + tker + " > " + msg);
    plugin.KillPlayer(tker, 5);

    player.RoundData.unsetString(key);
    player.RoundData.unsetObject(key);
}
return false;