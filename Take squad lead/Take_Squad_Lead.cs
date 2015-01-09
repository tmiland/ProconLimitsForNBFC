/* For Procon Accounts
Create a limit to evaluate OnAnyChat, call it "Take Squad Lead", leave Action set to None.

Set first_check to this Code:
*/
//#######################################################################
bool canKill = false;
bool canKick = false;
bool canBan = false;
bool canMove = false;
bool canChangeLevel = false;
bool isSelf = true;
Match selfie = Regex.Match(player.LastChat, @"^\s*[@!]lead");
Match other = Regex.Match(player.LastChat, @"^\s*[@!]lead\s+([^\s]+)");
PlayerInfoInterface target = null;
if (other.Success) {
    List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
    all.AddRange(team1.players);
    all.AddRange(team2.players);
    String pattern = other.Groups[1].Value;
    if (String.IsNullOrEmpty(pattern)) return false;
    List<PlayerInfoInterface> matches = new List<PlayerInfoInterface>();
    foreach (PlayerInfoInterface p in all) {
        if (Regex.Match(p.Name, pattern, RegexOptions.IgnoreCase).Success) {
            matches.Add(p);
        }
    }
    if (matches.Count == 0) {
        plugin.SendPlayerMessage(player.Name, "No player name matches: " + pattern);
        return false;
    } else if (matches.Count > 1) {
        List<String> names = new List<String>();
        foreach (PlayerInfoInterface m in matches) {
            names.Add(m.Name);
        }
        String tmp = String.Join(", ", names.ToArray());
        if (tmp.Length > 90) tmp = tmp.Substring(0,90);
        plugin.SendPlayerMessage(player.Name, "Multiple matches, try again: " + tmp + "...");
        return false;
    }
    target = matches[0];
    isSelf = false;
} else if (selfie.Success) {
    target = player;
} else {
    return false;
}

if (plugin.CheckAccount(player.Name, out canKill, out canKick, out canBan, out canMove, out canChangeLevel)) {
    if (!canMove) return false;

    plugin.ServerCommand("squad.leader", target.TeamId.ToString(), target.SquadId.ToString(), target.Name);

    String[] SQUAD_NAMES = new String[] { "None",
      "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel",
      "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa",
      "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray",
      "Yankee", "Zulu", "Haggard", "Sweetwater", "Preston", "Redford", "Faith", "Celeste"
    };
    String msg = target.Name + " is now squad leader of " + SQUAD_NAMES[target.SquadId] + " squad";
    plugin.ConsoleWrite(msg);
    plugin.SendPlayerMessage(player.Name, msg);
    if (!isSelf) plugin.SendPlayerMessage(target.Name, msg);
} else {
    plugin.SendPlayerMessage(player.Name, "You don't have permission to use the " + player.LastChat + " command");
}
return false;
//###########
/* For admins list
Create a new list, Enabled, name it admins, set CaseSensitive, fill in with a comma separated list of player names OR clan tags.

Create a limit to evaluate OnAnyChat, name it "Admin List Take Squad Lead", leave Action set to None.

Set first_check to this Code: 
*/
//###########################
/* R4 */
bool isSelf = true;
Match selfie = Regex.Match(player.LastChat, @"^\s*[@!]lead");
Match other = Regex.Match(player.LastChat, @"^\s*[@!]lead\s+([^\s]+)");
PlayerInfoInterface target = null;
if (other.Success) {
    List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
    all.AddRange(team1.players);
    all.AddRange(team2.players);
    String pattern = other.Groups[1].Value;
    if (String.IsNullOrEmpty(pattern)) return false;
    List<PlayerInfoInterface> matches = new List<PlayerInfoInterface>();
    foreach (PlayerInfoInterface p in all) {
        if (Regex.Match(p.Name, pattern, RegexOptions.IgnoreCase).Success) {
            matches.Add(p);
        }
    }
    if (matches.Count == 0) {
        plugin.SendPlayerMessage(player.Name, "No player name matches: " + pattern);
        return false;
    } else if (matches.Count > 1) {
        List<String> names = new List<String>();
        foreach (PlayerInfoInterface m in matches) {
            names.Add(m.Name);
        }
        String tmp = String.Join(", ", names.ToArray());
        if (tmp.Length > 90) tmp = tmp.Substring(0,90);
        plugin.SendPlayerMessage(player.Name, "Multiple matches, try again: " + tmp + "...");
        return false;
    }
    target = matches[0];
    isSelf = false;
} else if (selfie.Success) {
    target = player;
} else {
    return false;
}


if (plugin.isInList(player.Name, "admins") || plugin.isInList(player.Tag, "admins")) {

    plugin.ServerCommand("squad.leader", target.TeamId.ToString(), target.SquadId.ToString(), target.Name);

    String[] SQUAD_NAMES = new String[] { "None",
      "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel",
      "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa",
      "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray",
      "Yankee", "Zulu", "Haggard", "Sweetwater", "Preston", "Redford", "Faith", "Celeste"
    };
    String msg = target.Name + " is now squad leader of " + SQUAD_NAMES[target.SquadId] + " squad";
    plugin.ConsoleWrite(msg);
    plugin.SendPlayerMessage(player.Name, msg);
    if (!isSelf) plugin.SendPlayerMessage(target.Name, msg);
} else {
    plugin.SendPlayerMessage(player.Name, "You don't have permission to use the " + player.LastChat + " command");
}
return false;
//###########
/* For Reserved slots

Create a limit to evaluate OnAnyChat, name it "Take Squad Lead For Reserved Slots", leave Action set to None.

Set first_check to this Code: 
*/
//###########################
/* R4 */
bool isSelf = true;
Match selfie = Regex.Match(player.LastChat, @"^\s*[@!]lead");
Match other = Regex.Match(player.LastChat, @"^\s*[@!]lead\s+([^\s]+)");
PlayerInfoInterface target = null;
if (other.Success) {
    List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
    all.AddRange(team1.players);
    all.AddRange(team2.players);
    String pattern = other.Groups[1].Value;
    if (String.IsNullOrEmpty(pattern)) return false;
    List<PlayerInfoInterface> matches = new List<PlayerInfoInterface>();
    foreach (PlayerInfoInterface p in all) {
        if (Regex.Match(p.Name, pattern, RegexOptions.IgnoreCase).Success) {
            matches.Add(p);
        }
    }
    if (matches.Count == 0) {
        plugin.SendPlayerMessage(player.Name, "No player name matches: " + pattern);
        return false;
    } else if (matches.Count > 1) {
        List<String> names = new List<String>();
        foreach (PlayerInfoInterface m in matches) {
            names.Add(m.Name);
        }
        String tmp = String.Join(", ", names.ToArray());
        if (tmp.Length > 90) tmp = tmp.Substring(0,90);
        plugin.SendPlayerMessage(player.Name, "Multiple matches, try again: " + tmp + "...");
        return false;
    }
    target = matches[0];
    isSelf = false;
} else if (selfie.Success) {
    target = player;
} else {
    return false;
}

List<String> ReservervedSlots = plugin.GetReservedSlotsList();
if (ReservervedSlots.Contains(player.Name)) {

    plugin.ServerCommand("squad.leader", target.TeamId.ToString(), target.SquadId.ToString(), target.Name);

    String[] SQUAD_NAMES = new String[] { "None",
      "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel",
      "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa",
      "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray",
      "Yankee", "Zulu", "Haggard", "Sweetwater", "Preston", "Redford", "Faith", "Celeste"
    };
    String msg = target.Name + " is now squad leader of " + SQUAD_NAMES[target.SquadId] + " squad";
    plugin.ConsoleWrite(msg);
    plugin.SendPlayerMessage(player.Name, msg);
    if (!isSelf) plugin.SendPlayerMessage(target.Name, msg);
} else {
    plugin.SendPlayerMessage(player.Name, "You don't have permission to use the " + player.LastChat + " command");
}
return false;