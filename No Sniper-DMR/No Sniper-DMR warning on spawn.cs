/* No SniperDMR warning on spawn
Set first_check to this Code: */
/* Version 9.16/R1a */
/* SETUP */

// Messages
String msg = "WARNING: You will be AUTO-KICKED if you use SNIPER/DMR!";
String yellMsg = @"WARNING: You will be AUTO-KICKED if you use SNIPER/DMR!";
String msg1 = "WARNING: You will be AUTO-KICKED if you use SNIPER/DMR!";
String msg2 = "Please type @rules in chat to see the Server Rules.";

// Times
int yellTime = 15; // seconds
int secondNoticeSpawnCount = 3;

/* CODE */

String key = "WelcomeRulesExplosives_" + player.Name;

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