/* Create a new limit to evaluate OnAnyChat, call it "TK Forgive", leave Action set to None.

Set first_check to this Code: */
String apologies = @"(sorry|sry|my\s*bad)"; // CHANGE

if (player.TeamId != 1 && player.TeamId != 2) return false;
if (!Regex.Match(player.LastChat, apologies, RegexOptions.IgnoreCase).Success) return false;

String key = "Last_TK";
TeamInfoInterface team = (player.TeamId == 1) ? team1 : team2;
foreach (PlayerInfoInterface v in team.players) {
    if (v.RoundData.issetString(key) && player.Name == v.RoundData.getString(key)) {
        // Apology recognized
        plugin.SendPlayerMessage(v.Name, player.Name + " has apologized for team-killing you!"); // CHANGE
        plugin.SendPlayerMessage(player.Name, "You apologized for team-killing " + v.Name); // CHANGE
        plugin.PRoConChat("ADMIN > " + player.Name + " apologized for team-killing " + v.Name);
        v.RoundData.setObject(key, (Object)DateTime.MaxValue);
    }
}
return false;