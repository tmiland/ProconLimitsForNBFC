/* OPTIONAL

If you want to prevent players from entering the lottery between rounds and possibly getting double entries/cheating, add this third limit:

Create a new limit OnRoundStart, called "Plug Lotto Hole", leave Action set to None.

Set first_check to this Code: */
String kState = "Lotto_State"; // plugin.RoundData int
String kEntries1 = "Lotto_Entries1"; // Team 1: plugin.RoundData Object (List<String>)
String kEntries2 = "Lotto_Entries2"; // Team 2: plugin.RoundData Object (List<String>)

if (plugin.RoundData.issetInt(kState)) plugin.RoundData.unsetInt(kState);
if (plugin.RoundData.issetObject(kEntries1)) plugin.RoundData.unsetObject(kEntries1);
if (plugin.RoundData.issetObject(kEntries2)) plugin.RoundData.unsetObject(kEntries2);
String msg = "All Lotto entries have been cleared!";
plugin.ConsoleWrite(msg);
plugin.SendGlobalMessage(msg);
return false;