//watch for !nextmap
if (Regex.Match(player.LastChat, @"^\s*[!/](nextmap|nm)$", RegexOptions.IgnoreCase).Success)
{
plugin.ConsoleWrite("" + player.Name + " wants to know the next map");

String map_msg = "Next map is " + plugin.FriendlyMapName(server.NextMapFileName);
String rnd_msg = "(It may be different if the server population changes or due to a recent map vote!) ";
plugin.ServerCommand("admin.say", map_msg, "player", player.Name);
plugin.ServerCommand("admin.say", rnd_msg, "player", player.Name);

return true;
}

return false;