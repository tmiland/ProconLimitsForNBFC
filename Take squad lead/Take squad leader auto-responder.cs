/* Take squad leader auto-responder

First expression: */
(Regex.Match(player.LastChat, @"ID_CHAT_REQUEST_ORDER", RegexOptions.IgnoreCase).Success)
/* Second Code: */
/* Version: V0.8/R2 */
String kCounter = player.Name + "_chat_spams";
String msg1 = player.Name + " Take squad leader position with !lead if the current SL is failing! "; // CUSTOMIZE: 1st msg
String msg2 = player.Name + " !lead is Only for reserved slot players! "; // CUSTOMIZE: 2nd msg
String msg3 = player.Name + " Get your slot @ nbfc.no! Only 30 NOK per month! "; // CUSTOMIZE: 3rd msg

int warnings = 0;
if (server.Data.issetInt(kCounter)) warnings = server.Data.getInt(kCounter);

String msg = "none";
Action<String> ChatPlayer = delegate(String who) {
	plugin.ServerCommand("admin.say", msg, "player", who);
	plugin.ServerCommand("admin.yell", msg, "10", "player", who);
	plugin.PRoConChat( "ADMIN to " + who +"> " + msg);
};

if (warnings == 2) { // Private warning
	msg = msg1;
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
} else if (warnings == 3) {
        msg = msg2;
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
} else if (warnings == 4) {
		msg = msg3;
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
}

server.Data.setInt(kCounter, warnings+1);
return false;