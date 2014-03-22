/*
INSTRUCTIONS

Step 1

Create a custom list called admins (see note below). Set it Enabled and set the comparison to CaseSensitive. Set its value to a comma separated list of player names.

NOTE: If you already have an admin list for another limit, you may use it. Ignore the instructions above for "admins" and make a slight modification to the second_check code of the OnAnyChat limit below. Find the line that contains String admins and change the value of "admins" to whatever the name of your list is. For example, if your custom admin name list is called "admin_list", make the line look like this:
Code:
String admins = "admins"; // Define the name of your admins list here
Step 2

Create a custom list called admin_tags. Set it Enabled and set the comparison to CaseSensitive. Set its value to a comma separated list of tags. If a player's tag matches any on the list, they are considered an admin.

Hint: If you only want to use the tag list and not the name list, just write micovery in the list of admin names.


Step 3

Create a new limit and set it Enabled. Set limit to evaluate OnAnyChat, 
set the name to Squad Recruiter Commands, set the action to None.

Set first_check to this Expression:
*/
//###############
( Regex.Match(server.Gamemode, "^(?:Conquest|Rush|Domination)", RegexOptions.IgnoreCase).Success && (Regex.Match(player.LastChat, @"^\s*[!](?:recruit|draft)", RegexOptions.IgnoreCase).Success
|| Regex.Match(player.LastChat, @"(?:squad|clan|friend|mate)", RegexOptions.IgnoreCase).Success))
//###############
/* Set second_check to this Code: */
//###############
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
        String[] squadName = new String[9]{"No Squad", "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"};
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
//###############
/*
Step 4

Create a new limit and set it Enabled. Set limit to evaluate OnDeath, set the name to Squad Recruiter, set the action to None.

Set first_check to this Code:
*/
//#################
/* Version: V0.8/R4 */

if ( Regex.Match(server.Gamemode, "^(?:Conquest|Rush|Domination)", RegexOptions.IgnoreCase).Success ) {
    bool initialDraftMode = true; // Change this to false to start with mode OFF
    
    int level = 2;

    try {
        level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
    } catch (Exception e) {}
    
    // Check if victim is in the whitelist
    if (plugin.isInPlayerWhitelist(victim.Name)) return false;

    String prefix = "SQR_";
    String kUSCadets = prefix + "US_cadets"; // server.RoundData
    String kRUCadets = prefix + "RU_cadets"; // server.RoundData
    String kCooldown = prefix + "cooldown"; // server.RoundData
    String kDraft = prefix + "draft"; // plugin.Data

    if (!plugin.Data.issetBool(kDraft)) {
        plugin.Data.setBool(kDraft, initialDraftMode);
    }
    bool isDraftEnabled = plugin.Data.getBool(kDraft);

    String kCadets = (victim.TeamId == 1) ? kUSCadets : kRUCadets;

    if (!server.RoundData.issetObject(kCadets)) {
        server.RoundData.setObject(kCadets, new Dictionary<String, int>());
    }
    Dictionary<String, int> cadets = (Dictionary<String, int>)server.RoundData.getObject(kCadets);
    bool isCadet = cadets.ContainsKey(victim.Name);
    
    if (!isDraftEnabled && !isCadet) return false; // Nothing to do

    if (!server.RoundData.issetObject(kCooldown)) {
        server.RoundData.setObject(kCooldown, new List<String>());
    }
    List<String> coolDown = (List<String>)server.RoundData.getObject(kCooldown);
    bool isCooldown = coolDown.Contains(victim.Name);

    // Check if victim is not a cadet and is in a squad
    if (!isCadet && victim.SquadId != 0) return false;
    
    // Check if victim is in the cooldown pool (draft attempted this round)
    if (!isCadet && isCooldown) {
        if (level >= 3) plugin.ConsoleWrite("^b[SQR]^n: move already tried: " + victim.FullName);
        return false;
    }
    
    // Otherwise, count this activation and proceed
    return true;
}
return false;
//###########
/*
Set second_check to this Code:
*/
//####################
/* Version: V0.8/R4 */

/* Find a squad to place this dead lone wolf in */

String prefix = "SQR_";
String kUSCadets = prefix + "US_cadets"; // server.RoundData
String kRUCadets = prefix + "RU_cadets"; // server.RoundData
String kDraft = prefix + "draft"; // plugin.Data
String kCooldown = prefix + "cooldown"; // server.RoundData

int level = 2;

try {
    level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
} catch (Exception e) {}

/* If draft mode is on or victim on cadet list, attempt move */

String msg = "none";

bool isDraftEnabled = plugin.Data.getBool(kDraft);

if (isDraftEnabled && victim.SquadId == 0 && limit.Activations(victim.Name) == 1) {
    msg = "*** " + victim.FullName + ": don't be a draft dodger, join a squad!";
    plugin.SendTeamMessage(victim.TeamId, msg);
    plugin.PRoConChat("ADMIN > " + msg);
    if (level >= 3) plugin.ConsoleWrite("^b[SQR]^n: announced to " + victim.FullName);
    return false; // First death just gets a message
}

String kCadets = (victim.TeamId == 1) ? kUSCadets : kRUCadets;

Dictionary<String,int> cadets = (Dictionary<String,int>)server.RoundData.getObject(kCadets);
bool isCadet = cadets.ContainsKey(victim.Name);

List<String> coolDown = (List<String>)server.RoundData.getObject(kCooldown);
bool isCooldown = coolDown.Contains(victim.Name);

if (!isDraftEnabled && !isCadet) return false; // Nothing to do

/* We are clear to attempt to place the victim in a squad! */

// Find a squad, most full to least

int[] squads = new int[33]; // R20/Alpha - Hotel - 32 + the "no squad" (0)

TeamInfoInterface team = (victim.TeamId == 1) ? team1 : team2;
String teamName = (victim.TeamId == 1) ? "US" : "RU";

foreach (PlayerInfoInterface p in team.players) {
    int ii = p.SquadId;
    squads[p.SquadId] += 1;
}

int targetSquad = 0;

if (isCadet) {
    targetSquad = cadets[victim.Name];
    if (squads[targetSquad] == 4) {
        if (level >= 2) plugin.ConsoleWarn("^b[SQR]^n: squad " + targetSquad.ToString() + " on " + teamName + " is full: " + victim.FullName);
        msg = "*** Can't move " + victim.FullName + "to your squad, it's full!";
        plugin.SendSquadMessage(victim.TeamId, targetSquad, msg);
        plugin.PRoConChat("ADMIN > " + msg);
        return false; // No squad available
    }
} else {
    for (int n = 3; n >= 0; --n) {
        for (int s = 1; s <= 32; s++) {
            if (squads[s] == n) {
                targetSquad = s;
                break;
            }
        }
        if (targetSquad != 0) break;
    }
}

if (targetSquad == 0) {
    if (level >= 2) plugin.ConsoleWarn("^b[SQR]^n: no squad found for: " + victim.FullName + " on " + teamName);
    return false; // No squad available
}

/* 
Attempt the move - this may fail for multiple reasons. 
- Squad may be locked.
- Someone else may move ahead of this command.
- etc.
We'll give it two tries and then give up by putting player in the cooldown pool
*/

String[] squadName = new String[9]{"No Squad", "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel"};
String sqn = (targetSquad > 8) ? ("Squad-" + targetSquad) : squadName[targetSquad];

if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: ^b^4MOVE^0^n " + victim.FullName + " to " + sqn + " on " + teamName);
msg = "*** Insane Limits attempting to place " + victim.FullName + " in squad " + sqn + " on team " + teamName;
plugin.SendTeamMessage(victim.TeamId, msg);
plugin.PRoConChat("ADMIN > " + msg);
plugin.PRoConEvent(msg, "Insane Limits");
plugin.ServerCommand("admin.movePlayer", victim.Name, victim.TeamId.ToString(), targetSquad.ToString(), "false");

/* Clean-up */

// Remove cadet from cadet list
if (isCadet) {
    cadets.Remove(victim.Name);
}

// Add to cooldown pool if second try
if (!isCooldown && limit.Activations(victim.Name) > 1) {
    if (level >= 3) plugin.ConsoleWrite("^b[SQR]^n: cool down " + victim.FullName);
    coolDown.Add(victim.Name);
}
return true;
//##########
/*
NOTES

The Insane Limits Draft starts ON when the limit is Enabled. To start it as OFF, go to the OnDeath limit and look in the first_check code for a line containing initialDraftMode. Change the value from true to false.
The commands are case insensitive, so you may type !Draft or !DRAFT or !Recruit, etc.
The cadet name in the !recruit command uses the best match function, so abbreviations and misspelling are tolerated. However, this does mean that sometimes the limit may attempt to move the wrong player into a squad.
Restarting PRoCon or disabling/re-enabling the Insane Limits plugin will cause all the pending moves and the cool down pool to be forgotten. It will be as if the round just started.


At debug level 2 or higher, the plugin console log is updated every time a player is moved by Squad Recruiter. The full name of the player, the destination squad and destination team (US or RU) are included in the update. For example:

[20:42:52] [Insane Limits] [SQR]: MOVE [UN]XxiLikEtAcoSxX to Alpha on RU

"SQR" is an acronym for "SQuad Recruiter".

Also at debug level 2, any command that is rejected because of failure to meet required policy conditions, such as trying to use the !draft on command when not an admin or trying to use the !recruit command when not in a squad, will result in a chat message to the player and an update to the console log, for example:

[18:15:31] [Insane Limits] [SQR]: LtGeneralChesty is already in a squad and tags don't match

At debug level 3 or higher, additional debugging messages are logged.
*/
//###########
// https://forum.myrcon.com/showthread.php?3877-Insane-Limits-V0-8-R4-Squad-Recruiter
//###########