/* https://forum.myrcon.com/showthread.php?3877-Insane-Limits-V0-8-R4-Squad-Recruiter
Create a new limit and set it Enabled. Set limit to evaluate OnDeath, set the name to Squad Recruiter, set the action to None.

Set first_check to this Code: */
/* Version: V9.16/R5 */

if ( Regex.Match(server.Gamemode, "^(?:Conquest|Rush|Domination)", RegexOptions.IgnoreCase).Success ) {
    bool initialDraftMode = true; // Change this to false to start with mode OFF
    
    int level = 2;

    try {
        level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
    } catch (Exception e) {}
    
    // Check if victim is in the whitelist
    if (plugin.isInPlayerWhitelist(victim.Name)) return false;

    String prefix = "SQR_";
    String kUSCadets = prefix + "T1_cadets"; // server.RoundData
    String kRUCadets = prefix + "T2_cadets"; // server.RoundData
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

/* Set second_check to this Code: */

/* Version: V9.16/R5 */

/* Find a squad to place this dead lone wolf in */

String prefix = "SQR_";
String kT1Cadets = prefix + "T1_cadets"; // server.RoundData
String kT2Cadets = prefix + "T2_cadets"; // server.RoundData
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

String kCadets = (victim.TeamId == 1) ? kT1Cadets : kT2Cadets;

Dictionary<String,int> cadets = (Dictionary<String,int>)server.RoundData.getObject(kCadets);
bool isCadet = cadets.ContainsKey(victim.Name);

List<String> coolDown = (List<String>)server.RoundData.getObject(kCooldown);
bool isCooldown = coolDown.Contains(victim.Name);

if (!isDraftEnabled && !isCadet) return false; // Nothing to do

/* We are clear to attempt to place the victim in a squad! */

// Find a squad, most full to least

int[] squads = new int[33]; // R20/Alpha - Hotel - 32 + the "no squad" (0)

TeamInfoInterface team = (victim.TeamId == 1) ? team1 : team2;
String[] factionNames = new String[3]{"US", "RU", "CN"};
String teamName = "??";

foreach (PlayerInfoInterface p in team.players) {
    int ii = p.SquadId;
    squads[p.SquadId] += 1;
}

int targetTeam = victim.TeamId;
int targetSquad = 0;

if (isCadet) {
    int teamSquad = cadets[victim.Name];
    targetTeam = (teamSquad / 1000);
    targetSquad = (teamSquad % 1000);
    if (squads[targetSquad] == 5) {
        if (level >= 2) plugin.ConsoleWarn("^b[SQR]^n: squad " + targetSquad.ToString() + " is full: " + victim.FullName);
        msg = "*** Can't move " + victim.FullName + "to your squad, it's full!";
        plugin.SendSquadMessage(targetTeam, targetSquad, msg);
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

int faction = server.GetFaction(targetTeam);
if (faction >= 0 && faction <= 2) teamName = factionNames[faction];

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

String[] squadName = new String[] { "None",
      "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel",
      "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa",
      "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray",
      "Yankee", "Zulu", "Haggard", "Sweetwater", "Preston", "Redford", "Faith", "Celeste"
    };
String sqn = (targetSquad >= 0) ? squadName[targetSquad] : "?";

if (level >= 2) plugin.ConsoleWrite("^b[SQR]^n: ^b^4MOVE^0^n " + victim.FullName + " to " + sqn + " on " + teamName);
msg = "*** Recruiting " + victim.FullName + " to " + sqn + " on " + teamName;
plugin.SendTeamMessage(targetTeam, msg);
if (victim.TeamId != targetTeam) plugin.SendPlayerMessage(victim.Name, msg);
plugin.PRoConChat("ADMIN > " + msg);
plugin.PRoConEvent(msg, "Insane Limits");
plugin.ServerCommand("admin.movePlayer", victim.Name, targetTeam.ToString(), targetSquad.ToString(), "false");

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
return false;