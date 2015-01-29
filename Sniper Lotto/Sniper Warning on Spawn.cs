/* Evaluation: OnSpawn
First check 
code: */
/* SETUP */

// Messages
String msg = "WARNING: ONLY 5 SNIPERS/DMR PER TEAM! Type !sniper to join the lottery, or buy a reserved slot!";
String yellMsg = @"WARNING: ONLY 5 SNIPERS/DMR PER TEAM! Type !sniper to join the lottery, or buy a reserved slot!";
String msg1 = "WARNING: ONLY 5 SNIPERS/DMR PER TEAM! Type !sniper to join the lottery, or buy a reserved slot!";
String msg2 = "Please register @ nbfc.no to buy your reserved slot! Only 30,- NOK per month!";

// Times
int yellTime = 20; // seconds
int secondNoticeSpawnCount = 3;

/* CODE */
// Disable for Reserverved Slots
List<String> ReservervedSlots = plugin.GetReservedSlotsList();
if (ReservervedSlots.Contains(player.Name)) {
	return false;	
}

String key = "WelcomeRulesSniperLotto_" + player.Name;

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