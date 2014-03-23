/* Evaluation: OnSpawn
First check 
code: */
/* SETUP */

// Messages
String msg = "WARNING: THIS IS KNIFES & PISTOLS ONLY! Type !wp for weapon rules!";
String yellMsg = @"WARNING: This is Knifes & Pistols only! Type !wp for weapon rules!";
String msg1 = "WARNING: THIS IS KNIFES & PISTOLS ONLY! Type !wp for weapon rules!";
String msg2 = "Please type @rules in chat to see the Server Rules.";

// Times
int yellTime = 20; // seconds
int secondNoticeSpawnCount = 3;

/* CODE */

String key = "WelcomeRulesKnifesAndPistols_" + player.Name;

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