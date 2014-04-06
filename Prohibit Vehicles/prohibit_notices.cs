/* https://forum.myrcon.com/showthread.php?7617-Insane-Limits-Mobile-Anti-Air-and-AA-Mine-Limit
AA Notices
OnSpawn
first check code: */
/* Version 9.16/R1a */
/* SETUP */

// Messages
String msg = "WARNING: You will be AUTO-KICKED if you use the PROHIBITED Mobile-AA or GUNSHIP!";
String yellMsg = @"WARNING: You will be AUTO-KICKED if you use the PROHIBITED Mobile-AA, AA-Mine or GUNSHIP!";
String msg1 = "WARNING: You will be AUTO-KICKED if you use the PROHIBITED Mobile-AA or GUNSHIP!";
String msg2 = "Please type @rules in chat to see the Server Rules.";

// Times
int yellTime = 25; // seconds
int secondNoticeSpawnCount = 3;

/* CODE */

String key = "WelcomeRulesAA_" + player.Name;

int count = 0;
if (player.Data.issetInt(key)) count = player.Data.getInt(key);

count = count + 1;

if (count == 1) { // First notice

    plugin.SendPlayerMessage(player.Name, msg);
    plugin.SendPlayerYell(player.Name, yellMsg, yellTime);

} else if (count == secondNoticeSpawnCount) { // Second notice

    plugin.SendPlayerMessage(player.Name, msg1);
    plugin.SendPlayerMessage(player.Name, msg2);
}

player.Data.setInt(key, count);

return false;