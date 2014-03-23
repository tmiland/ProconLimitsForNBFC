/* Announce VIP 
Evaluation: OnJoin
First check: 
Code: */
List<String> ReservervedSlots = plugin.GetReservedSlotsList();
if (ReservervedSlots.Contains(player.Name))
return true;
else
return false;
/* Second check 
Code: */
plugin.SendGlobalMessage(plugin.R("Reserved Player: %k_fn% joined the server."));
plugin.PRoConChat(plugin.R("[Reserved Player Announcer] > Reserved Player: %k_fn% joined the server."));
return false;