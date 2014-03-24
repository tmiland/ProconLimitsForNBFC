/* https://forum.myrcon.com/showthread.php?3877-Insane-Limits-V0-8-R4-Squad-Recruiter
Create a new limit and set it Enabled. Set limit to evaluate OnAnyChat, set the name to Squad Recruiter Commands, set the action to None.

Set first_check to this Expression: */
( Regex.Match(server.Gamemode, "^(?:Conquest|Rush|Domination)", RegexOptions.IgnoreCase).Success && (Regex.Match(player.LastChat, @"^\s*[!](?:recruit|draft)", RegexOptions.IgnoreCase).Success
|| Regex.Match(player.LastChat, @"(?:squad|clan|friend|mate)", RegexOptions.IgnoreCase).Success))
/* Set second_check to this Code: */
/* Version: V0.8/R4
!recruit name
- Attempts to move the soldier matching "name" to the player's squad.

!draft (on|off)
- With arguments, enable/disable the automatic squad draft.
- Without arguments, sends the current state to chat.
*/

String admins = "admins"; // Define the name of your admins list here
String admin_tags = "admin_tags"; // Define the name of your admin tag list here

String prefix = "SQR_";
String kUSCadets = prefix + "US_cadets"; // server.RoundData
String kRUCadets = prefix + "RU_cadets"; // server.RoundData
String kDraft = prefix + "draft"; // plugin.Data
String kCooldown = prefix + "cooldown"; // server.RoundData

String msg = "none";

int level = 2;

try {
    level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
} catch (Exception e) {}

Match rMatch = Regex.Match(player.LastChat, @"recruit\s+([^\s]+)", RegexOptions.IgnoreCase);
Match d0Match = Regex.Match(player.LastChat, @"draft\s*$", RegexOptions.IgnoreCase);
Match d1Match = Regex.Match(player.LastChat, @"draft\s+(on|off|1|0|true|false)", RegexOptions.IgnoreCase);

if (level >= 3) plugin.ConsoleWrite("^b[SQR]^n: Orig command by " + player.FullName + ": " + player.LastChat);

/* Somebody mentioned "squad" in their chat */
if (Regex.Match(player.LastChat, @"\s(?:squad|clan|friend|mate)", RegexOptions.IgnoreCase).Success || Regex.Match(player.LastChat, @"^squad\s", RegexOptions.IgnoreCase).Success) {
    if (level >= 3) plugin.ConsoleWrite("^b[SQR]^n: keyword detected.");
    plugin.SendSquadMessage(player.TeamId, player.SquadId, "Hey " + player.Name + ", type '!recruit ?' for info on moving friends to your squad");
    return false;
}

Action<String> ChatPlayer = delegate(String who) {
    plugin.ServerCommand("admin.say", msg, "player", who);
    plugin.PRoConChat( "ADMIN to " + who +"> " + msg);
};

/* !recruit command? */

if (rMatch.Success) {
    // Add to list of cadets that need a squad
    String rName = rMatch.Groups[1].Value;
    PlayerInfoInterface recruit = plugin.GetPlayer(rName, true);
    // Check if requesting info
    if (rName == "?") {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: " + player.FullName + " requested info");
        msg = "*** Move a friend to your squad with:\n!recruit friends_name";
        ChatPlayer(player.Name);
        msg = "*** Your squad must have room and your tags must match";
        ChatPlayer(player.Name);
        return false;
    }
    // Check if dummy is dumb
    if (rName == "friends_name") {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: dummy is dumb");
        msg = "*** Replace 'friends_name' with the soldier name of your friend";
        ChatPlayer(player.Name);
        return false;
    }
    // Check if there is a player by that name
    if (recruit == null) {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: no such player: " + rName);
        msg = "*** No such player: " + rName;
        ChatPlayer(player.Name);
        return false;
    }
    
    // Tell player what name we are trying to use
    msg = "*** Closest match to '" + rName + "' is " + recruit.FullName;
    ChatPlayer(player.Name);
    msg = "*** " + player.FullName + " is trying to recruit you to his squad";
    ChatPlayer(recruit.Name);
    
    // Check if requestor is not in a squad
    if (player.SquadId == 0) {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: player not in a squad: " + player.FullName);
        msg = "*** You are not in a squad: " + player.FullName;
        ChatPlayer(player.Name);
        return false;
    }
    // Check if recruit is on whitelist
    if (plugin.isInPlayerWhitelist(recruit.Name)) {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: on whitelist: " + recruit.FullName);
        msg = "*** Player is exempt: " + recruit.FullName;
        ChatPlayer(player.Name);
        return false;
    }

    // Extract tags for further testing
    String ptag = player.Tag;
    if (String.IsNullOrEmpty(ptag)) {
        // Maybe they are using [_-=]XXX[=-_]PlayerName format
        Match tm = Regex.Match(player.Name, @"^[=_\-]?([^=_\-]{2,4})[=_\-]");
        if (tm.Success) {
            ptag = tm.Groups[1].Value;
            if (level >= 4) plugin.ConsoleWrite("^b[SQR]^n extracted [" + ptag + "] from " + player.Name);
        }
    }
    String rtag = recruit.Tag;
    if (String.IsNullOrEmpty(rtag)) {
        // Maybe they are using [_-=]XXX[=-_]PlayerName format
        Match tm = Regex.Match(recruit.Name, @"^[=_\-]?([^=_\-]{2,4})[=_\-]");
        if (tm.Success) {
            rtag = tm.Groups[1].Value;
            if (level >= 4) plugin.ConsoleWrite("^b[SQR]^n extracted [" + rtag + "] from " + recruit.Name);
        }
    }

    // Check if recruit is on the other team (only matching tags may recruit)
    if (recruit.TeamId != player.TeamId) {
        // Check if tags don't match
        if (String.IsNullOrEmpty(ptag) 
          || (!String.IsNullOrEmpty(ptag) 
            && (String.IsNullOrEmpty(rtag) || ptag != rtag)
          )
        ) {
        
            if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: not on same team: " + recruit.FullName);
            msg = "*** Not on same team and tags don't match: " + recruit.FullName;
            ChatPlayer(player.Name);
            return false;
        }
    }
    // Check if recruit is already in a squad (only matching tags may recruit)
    if (recruit.SquadId != 0) {
        // Check if tags don't match
        if (String.IsNullOrEmpty(ptag) 
          || (!String.IsNullOrEmpty(ptag) 
            && (String.IsNullOrEmpty(rtag) || ptag != rtag)
          )
        ) {
            if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: " + recruit.FullName + "is already in a squad and tags don't match");
            msg = "*** Already in a squad and tags don't match: " + recruit.FullName;
            ChatPlayer(player.Name);
            return false;
        }
    }
/*
    // Check if squad id is above 8 as of R20 patch
    if (player.SquadId > 8) {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: " + player.FullName + " is in a custom squad #" + recruit.SquadId);
        msg = "*** You are in custom squad #" + player.SquadId + " and cannot recruit players";
        plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
        plugin.PRoConChat( "ADMIN > " + msg);
        return false;
    }
*/
    // Check if recruit is in the cooldown pool and has different tags
    if (!server.RoundData.issetObject(kCooldown)) {
        server.RoundData.setObject(kCooldown, new List<String>());
    }
    List<String> coolDown = (List<String>)server.RoundData.getObject(kCooldown);
    if (coolDown.Contains(recruit.Name)) {
        if (!String.IsNullOrEmpty(rtag) && !String.IsNullOrEmpty(ptag) && ptag != rtag) {
            if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: in cooldown pool: " + recruit.FullName);
            msg = "*** Already been moved this round: " + recruit.FullName;
            ChatPlayer(player.Name);
            return false;
        } else {
            // Recruit with matching tag is allowed to be moved again
            coolDown.Remove(recruit.Name);
            server.RoundData.setObject(kCooldown, coolDown);
        }
    }
    // Add to list
    String kCadets = (player.TeamId == 1) ? kUSCadets : kRUCadets;
    if (!server.RoundData.issetObject(kCadets)) {
        server.RoundData.setObject(kCadets, new Dictionary<String,int>());
    }
    Dictionary<String,int> teamCadets = (Dictionary<String,int>)server.RoundData.getObject(kCadets);
    teamCadets.Add(recruit.Name, player.SquadId);
    if (level >= 2) {
        String[] squadName = new String[9]{"None",
      "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel",
      "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa",
      "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray",
      "Yankee", "Zulu", "Haggard", "Sweetwater", "Preston", "Redford", "Faith", "Celeste"};
        String sqn = (player.SquadId > 8) ? ("Squad-" + player.SquadId) : squadName[player.SquadId];
        String teamName = (player.TeamId == 1) ? "US" : "RU";
        if (level >= 2 ) plugin.ConsoleWrite("^b[SQR]^n: new cadet for squad " + sqn + " on team " + teamName + ": " + recruit.FullName);
    }
    msg = "*** Insane Limits will attempt to move " + recruit.FullName + " to your squad on next death.";
    plugin.SendTeamMessage(player.TeamId, msg);
    plugin.PRoConChat( "ADMIN > " + msg);
    return false;
}

/* !draft command with no arguments */

if (d0Match.Success) {
    // Report enabled/disabled
    if (!plugin.Data.issetBool(kDraft)) {
        plugin.Data.setBool(kDraft, false);
    }
    bool isDraftEnabled = plugin.Data.getBool(kDraft);
    if (isDraftEnabled) {
        msg = "*** Insane Limits squad draft mode is:\n*** ON!";
    } else {
        msg = "*** Insane Limits squad draft mode is:\n*** OFF!";
    }
    plugin.SendGlobalMessage(msg);
    plugin.PRoConChat( "ADMIN > " + msg);
    return false;
}

/* !draft command with arguments */

if (d1Match.Success) {
    String tag = player.Tag;
    if (tag.Length == 0) {
        // Maybe they are using [_-=]XXX[=-_]PlayerName format
        Match tm = Regex.Match(player.Name, @"^[=_\-]?([^=_\-]{2,4})[=_\-]");
        if (tm.Success) {
            tag = tm.Groups[1].Value;
            if (level >= 4) plugin.ConsoleWrite("^b[SQR]^n extracted [" + tag + "] from " + player.Name);
        } else {
            tag = "no tag";
        }
    }
    if (!plugin.isInList(player.Name, admins) && !plugin.isInList(tag, admin_tags)) {
        if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: " + "[" + tag + "]" + player.Name + " not authorized to use !draft command.");
        msg = "*** You are not authorized to change the draft!";
        plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
        plugin.PRoConChat( "ADMIN > " + msg);
        return false;
    }
    // Enable/Disable
    String state = "OFF!";
    if (!plugin.Data.issetBool(kDraft)) {
        plugin.Data.setBool(kDraft, false);
    } else if (Regex.Match(d1Match.Groups[1].Value, "(on|1|true)", RegexOptions.IgnoreCase).Success) {
        plugin.Data.setBool(kDraft, true);
        state = "ON!";
    } else {
        plugin.Data.setBool(kDraft, false);
    }
    msg = "*** Insane Limits squad draft mode is:\n***" + state;
    plugin.SendGlobalMessage(msg);
    plugin.PRoConChat( "ADMIN > " + msg);
    return false;
}

return false;