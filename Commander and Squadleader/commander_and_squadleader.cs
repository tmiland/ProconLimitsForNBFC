/* Limit to put the Commander and Squad Leader on the same team
Create a limit to evaluate OnRoundStart, leave Action set to None: */

/* Version 0.9.15/R1 */
String SquadLeader = "VeloViking"; // change this to the squad leader
String Commander = "TheRubberDuck"; // change this to commander

// If already on the same team, do nothing:
PlayerInfoInterface sl = plugin.GetPlayer(SquadLeader, false);
PlayerInfoInterface cm = plugin.GetPlayer(Commander, false);
if (sl == null || cm == null) return false;
if (sl.TeamId == 0 || cm.TeamId == 0) return false;
if (sl.TeamId == cm.TeamId) return false;
String m = "Commander " + Commander + " and Squad Leader " + SquadLeader + " are not on the same team!";
plugin.ConsoleWrite(m);
plugin.SendPlayerMessage(Commander, m);
plugin.SendPlayerMessage(SquadLeader, m);

// Determine if Commander is a commander or not
if (cm.Role != 2) {
    plugin.ConsoleWrite(Commander + " is not a commander");
    return false;
}

// Determine if SquadLeader is a squad leader or not
if (plugin.GetSquadLeaderName(sl.TeamId, sl.SquadId) == SquadLeader) {
    // Move Commander to SquadLeader's team
    String msg = "Moving Commander " + Commander + " to Squad Leader " + SquadLeader + " team " + sl.TeamId;
    plugin.ConsoleWrite(msg);
    plugin.SendGlobalMessage(msg);
    plugin.MovePlayer(Commander, sl.TeamId, 0, true);
} else {
    plugin.ConsoleWrite(SquadLeader + " is not a squad leader");
}
return false;