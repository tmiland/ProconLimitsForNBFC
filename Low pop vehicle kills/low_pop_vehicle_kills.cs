/* Create a new limit OnKill, call it "Low Pop Vehicle Kills", leave Action set to None.

In the code that follows, these variables define how the limit works -- think of these like settings, but you have to change the code to change them:

minimumPlayers: if there are the same or fewer than this number of players in the server, the punishment is enabled. If there are more players than this number, the punishment is disabled.

warningCount: first through this number of vehicle kills get a warning (player-only chat).

adminKillCount: first through this number of vehicle kills result in the player being admin killed.

kickCount: this number of vehicle kills will result in the player being kicked. If they come back and vehicle kill again and the population is still too low, they will get kicked again.

Note that adminKillCount and warningCount are forced to be less than kickCount. So if you make a mistake and set warningCount to 5 and kickCount to 4, warningCount will be changed to (kickCount - 1), which would be 3 in this example.

Set first_check to this Code: */
// Variables
int minimumPlayers = 16; // you can change this number
int warningCount = 2; // you can change this number
int adminKillCount = 2; // you can change this number
int kickCount = 3; // you can change this number
// You can change these messages also:
String warningMessage = "Vehicle kills will be punished until there are more than " + minimumPlayers + " players!"; 
String kickMessage = "ignored warnings about no vehicle kills";

// Code
if (!Regex.Match(kill.Weapon, @"(Death|RoadKill)", RegexOptions.IgnoreCase).Success) return false;
if (killer.Name == victim.Name) return false;
if (killer.Name == "Server") return false;
if (server.PlayerCount > minimumPlayers) return false;

String prefix = "LowPopVehiclePunisher_";
String key = prefix + killer.Name;

int count = 0;
if (plugin.RoundData.issetInt(key)) count = plugin.RoundData.getInt(key);

count = count + 1;

plugin.RoundData.setInt(key, count);

if (kickCount < 1) kickCount = 1;
if (warningCount >= kickCount) warningCount = kickCount - 1;
if (adminKillCount >= kickCount) adminKillCount = kickCount - 1;

if (count >= kickCount) {
    String tmp = "Kicking " + killer.Name + " killed " + victim.Name + " with " + kill.Weapon + ", reason: " + kickMessage;
    plugin.ConsoleWrite(tmp);
    plugin.PRoConChat("Insane Limits > " + tmp);
    plugin.KickPlayerWithMessage(killer.Name, kickMessage);
    return false;
}

bool warned = false;
if (count <= warningCount) {
    plugin.SendPlayerMessage(killer.Name, warningMessage);
    plugin.PRoConChat("Insane Limits > " + killer.Name + ": " + warningMessage);
    warned = true;
}


if (count <= adminKillCount) {
    if (!warned) {
        plugin.SendPlayerMessage(killer.Name, warningMessage);
        plugin.PRoConChat("Insane Limits > " + killer.Name + ": " + warningMessage);
    }
    plugin.KillPlayer(killer.Name, 5);
}

return false;
/*
OPTIONAL

For each players first noticeCount spawns, this limit will announce that vehicle kills will be punished if the population is less than minimumPlayers.

The value of minimumPlayers MUST be the same in both limits. If you change the code above, you must also change this code.

Create a new limit OnSpawn, call it "Spawn Notice", leave Action set to None.

In the code that follows, these variables define how the limit works -- think of these like settings, but you have to change the code to change them:

minimumPlayers: if there are the same or fewer than this number of players in the server, the punishment is enabled. If there are more players than this number, the punishment is disabled.

noticeCount: first through this number of spawns will send a chat message to the player.
*/
// Variables
int minimumPlayers = 16; // you can change this number - MUST BE THE SAME AS THE OnKill LIMIT!
int noticeCount = 2; // you can change this number
// You can change this message also:
String noticeMessage  = "Vehicle kills will be punished until there are more than " + minimumPlayers + " players!"; 

// Code
if (server.PlayerCount > minimumPlayers) return false;

String prefix = "SpawnNotice_";
String key = prefix + player.Name;

int count = 0;
if (plugin.RoundData.issetInt(key)) count = plugin.RoundData.getInt(key);

if (count > noticeCount) return false;

count = count + 1;

plugin.RoundData.setInt(key, count);

plugin.SendPlayerMessage(player.Name, noticeMessage);
return false;