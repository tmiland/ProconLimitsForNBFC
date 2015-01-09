/* Create a limit to evaluate OnSpawn, call it "Vote IO Reminder".

Set first_check to this Code: */
String msg = "Type !voteio to run the next round as INFANTRY ONLY!";
// Times
int yellTime = 10; // seconds
int secondNoticeSpawnCount = 4;

/* CODE */

String key = "WelcomeVoteIO_" + player.Name;

int count = 0;
if (player.Data.issetInt(key)) count = player.Data.getInt(key);

count = count + 1;

if (count == secondNoticeSpawnCount) { // Second notice

    plugin.SendPlayerMessage(player.Name, msg);
	plugin.SendPlayerYell(player.Name, msg, yellTime);
}

player.Data.setInt(key, count);

return false;