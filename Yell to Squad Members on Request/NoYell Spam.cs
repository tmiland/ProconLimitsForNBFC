/* NoYell Notices
OnSpawn
first check code: */
String msg = "Turn On/Off the squad spam & Random Kill Announcements with @noyell in chat! Default is on!";
// Times
int yellTime = 10; // seconds
int secondNoticeSpawnCount = 3;

/* CODE */

String key = "WelcomeNoYell_" + player.Name;

int count = 0;
if (player.Data.issetInt(key)) count = player.Data.getInt(key);

count = count + 1;

if (!player.Data.issetBool("NoYell"))
{
	player.Data.setBool("NoYell", true);
}

if (count == secondNoticeSpawnCount) { // Second notice
	if (player.Data.getBool("NoYell"))
	{
		plugin.SendPlayerMessage(player.Name, msg);
	}
	plugin.SendPlayerYell(player.Name, msg, yellTime);
}

player.Data.setInt(key, count);

return false;