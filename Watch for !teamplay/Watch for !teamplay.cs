/* Watch for !teamplay 
https://forum.myrcon.com/showthread.php?4489-Insane-Limits-Requests&p=114408&viewfull=1#post114408
*/
if (!Regex.Match(player.LastChat, @"^\s*!teamplay", RegexOptions.IgnoreCase).Success)
    return false;
plugin.SendPlayerMessage(player.Name, "Attempting to move you to squad Tango ...");
plugin.MovePlayer(player.Name, player.TeamId, 20 /* Tango */, true);
return false;