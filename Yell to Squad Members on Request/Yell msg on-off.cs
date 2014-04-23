/* Yell msg on/off
Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Expression */
player.LastChat.StartsWith("@noyell")

/* Set second_check to this Code: */

if (!player.Data.issetBool("NoYell")) {
	player.Data.setBool("NoYell", false);
	plugin.SendPlayerYell(player.Name, plugin.R ("\nMessages off"),5);
	return false;
	}

if(player.Data.getBool("NoYell")) {
player.Data.setBool("NoYell", false);
plugin.SendPlayerYell(player.Name, plugin.R ("\nMessages off"),5);
}
else {
player.Data.setBool("NoYell", true);
plugin.SendPlayerYell(player.Name, plugin.R ("\nMessages on"),5);
}
return false;