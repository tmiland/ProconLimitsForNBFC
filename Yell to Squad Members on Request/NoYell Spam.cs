/* NoYell Notices
OnSpawn
first check code: */
String msg = "Turn On/Off the squad spam & Service stars on killcam with @noyell in chat! Default is on!";
// Times
int yellTime = 5; // seconds
int secondNoticeSpawnCount = 5;

/* CODE */

String key = "WelcomeNoYell_" + player.Name;

int count = 0;
if (player.Data.issetInt(key)) count = player.Data.getInt(key);

count = count + 1;

if (count == secondNoticeSpawnCount) { // Second notice

    plugin.SendPlayerMessage(player.Name, msg);
	plugin.SendPlayerYell(player.Name, msg, yellTime);
}

player.Data.setInt(key, count);

return false;