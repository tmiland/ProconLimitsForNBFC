/* Create a custom list called admins, set it to CaseInsensitive, add all of your admin's names to it.

Create a limit that evaluates OnAnyChat, call it "Admin Away Commands", leave Action set to None. 

Set first_check to this Code: */

/* Version 0.9/R1 */
bool isAdmin = plugin.isInList(player.Name, "admins");
Match away = Regex.Match(player.LastChat, @"^\s*!away\s+(.*)$", RegexOptions.IgnoreCase);
Match back = Regex.Match(player.LastChat, @"^\s*!back", RegexOptions.IgnoreCase);
String prefix =  "AdminAwayName_";
String key = prefix  + player.Name;

if (isAdmin && away.Success) {
    String msg = away.Groups[1].Value;
    plugin.SendPlayerMessage(player.Name, "Your away message has been recorded: " + msg);
    plugin.ConsoleWrite("^b[Away]^n " + player.Name + " recorded away message: " + msg);
    plugin.Data.setString(key, msg);
    return false;
}

if (isAdmin && back.Success) {
    plugin.SendPlayerMessage(player.Name, "You are back, your message has been deleted!");
    plugin.ConsoleWrite("^b[Away]^n " + player.Name + " is back!");
    plugin.Data.unsetString(key);
    return false;
}

// Check if chat contains an admin's name
String[] words = Regex.Split(player.LastChat, @"[^\w]");
if (words == null || words.Length == 0) return false;
foreach (String word in words) {
    PlayerInfoInterface admin = plugin.GetPlayer(word, true);
    if (admin != null && plugin.isInList(admin.Name, "admins")) {
        key = prefix + admin.Name;
        if (plugin.Data.issetString(key))  {
            // Send admin's away message to player
            plugin.SendPlayerMessage(player.Name, plugin.Data.getString(key));
            plugin.ConsoleWrite("^b[Away]^n " + admin.Name + " away message sent to " + player.Name);
            break;
        }
    }
}

return false;