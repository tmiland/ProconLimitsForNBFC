/* https://forum.myrcon.com/showthread.php?9277-Insane-Limits-!join-player-(move-to-team-squad-of-player)-command
Create a limit to evaluate OnAnyChat, call it "!join command".

Set first_check to this Code: */
Match m = Regex.Match(player.LastChat, @"^\s*[!@#]join\s+(^\s)+", RegexOptions.IgnoreCase);
if (!m.Success)
    return false;
List<String> vips = plugin.GetReservedSlotsList();
if (!vips.Contains(player.Name)) {
    plugin.SendPlayerMessage(player.Name, "You are not a VIP, you can't use the !join command!");
    return false;
}
// Match target player name
String name = m.Groups[1].Value;
List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
all.AddRange(team1.players);
all.AddRange(team2.players);
all.AddRange(team3.players);
all.AddRange(team4.players);
PlayerInfoInterface target = null;
int count = 0;
foreach (PlayerInfoInterface p in all) {
    if (Regex.Match(p.Name, name, RegexOptions.IgnoreCase).Success) {
        target = p;
        ++count;
    }
}
if (count == 0 || target == null) {
    plugin.SendPlayerMessage(player.Name, "No player name matches '" + name + "'");
    return false;
} else if (count > 1) {
    plugin.SendPlayerMessage(player.Name, "Too many names match '" + name + "', try again");
    return false;
}

// Force the squad to be unlocked
plugin.ServerCommand("squad.private", target.TeamId.ToString(), target.SquadId.ToString(), "false");

// Work the magic
plugin.SendPlayerMessage(player.Name, "Killing you and sending you to " + target.Name + "'s squad!");
plugin.MovePlayer(player.Name, target.TeamId, target.SquadId, true);
return false;