/* https://forum.myrcon.com/showthread.php?9209-Insane-Limits-!msquad-!fmsquad-move-player-to-another-squad-in-same-team-command&p=113895
Create a limit to evaluate OnAnyChat, call it "Squad Move Command".

Set first_check to this Code: */
/* Revision 1 */
Match cmd = Regex.Match(player.LastChat, @"[!@#]msquad\s+(Alpha|Bravo|Charlie|Delta|Echo|Foxtrot|Golf|Hotel|India|Juliet|Kilo|Lima|Mike|November|Oscar|Papa|Quebec|Romeo|Sierra|Tango|Uniform|Victor|Whiskey|Xray|Yankee|Zulu|Haggard|Sweetwater|Preston|Redford|Faith|Celeste)\s+([^\s]+)", RegexOptions.IgnoreCase);
Match fcmd = Regex.Match(player.LastChat, @"[!@#]fmsquad\s+(Alpha|Bravo|Charlie|Delta|Echo|Foxtrot|Golf|Hotel|India|Juliet|Kilo|Lima|Mike|November|Oscar|Papa|Quebec|Romeo|Sierra|Tango|Uniform|Victor|Whiskey|Xray|Yankee|Zulu|Haggard|Sweetwater|Preston|Redford|Faith|Celeste)\s+([^\s]+)", RegexOptions.IgnoreCase);

if (!cmd.Success && !fcmd.Success)
    return false;
Match m = null;
if (cmd.Success)
    m = cmd;
else
    m = fcmd;

String sl = plugin.GetSquadLeaderName(player.TeamId, player.SquadId);
if (player.Name != sl) {
    plugin.SendPlayerMessage(player.Name, "Only a squad leader may use the !msquad or !fmsquad command");
    return false;
}

// Check target player's name
List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
all.AddRange(team1.players);
all.AddRange(team2.players);
if (team3.players.Count > 0)
    all.AddRange(team3.players);
if (team4.players.Count > 0)
    all.AddRange(team4.players);
int found = 0;
String name = m.Groups[2].Value;
PlayerInfoInterface target = null;
foreach (PlayerInfoInterface p in all) {
    if (Regex.Match(p.Name, name, RegexOptions.IgnoreCase).Success) {
        ++found;
        target = p;
    }
}
if (found == 0) {
    plugin.SendPlayerMessage(player.Name, "No such player name matches (" + name + ")");
    return false;
}
if (found > 1) {
    plugin.SendPlayerMessage(player.Name, "Multiple players match the target name (" + name +"), try again!");
    return false;
}
if (target.TeamId != player.TeamId || target.SquadId != player.SquadId) {
    plugin.SendPlayerMessage(player.Name, "(" + target.Name + ") is not in the same squad as you");
    return false;
}

String squad = m.Groups[1].Value;
bool force = (fcmd.Success);
String[] squadName = new String[] { "None",
      "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel",
      "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa",
      "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray",
      "Yankee", "Zulu", "Haggard", "Sweetwater", "Preston", "Redford", "Faith", "Celeste"
    };
int squadId = 1;
for (int i = 0; i < squadName.Length; ++i) {
    if (squadName[i] == squad) {
        squadId = i;
        break;
    }
}

if (plugin.IsSquadLocked(player.TeamId, squadId)) {
    plugin.SendPlayerMessage(player.Name, "Cannot move to " + squad + ", that squad is locked!");
    return false;
}

String msg = "Moving " + target.Name  + " to squad " + squad;
plugin.SendPlayerMessage(player.Name, msg);
plugin.PRoConEvent(msg, player.Name);
plugin.MovePlayer(target.Name, target.TeamId, squadId, force);
return false;