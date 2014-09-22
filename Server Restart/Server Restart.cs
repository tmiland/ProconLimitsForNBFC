/* https://forum.myrcon.com/showthread.php?8117-Insane-Limits-Automatic-server-restart
Limit to restart the server a specified number of seconds after the first RoundOver in a specified hour, even if the server is not empty.

Create a limit to evaluate OnRoundOver, call it "Server Restart With Warnings".

Set first_check to this Code: */
int Hour24Clock = 5;  // CHANGE: 0 to 23 o'clock, 3 is 3AM, 15 is 3PM
int DelaySeconds = 45; // CHANGE: Number of seconds after RoundOver to do the restart
String ChatMessage = "Server RESTARTING NOW to BOOST Gameplay Performance! Please REJOIN IN 2 MINUTES to Play on a Fresh Server"; // CHANGE
String YellMessage = "Server RESTARTING NOW to BOOST Gameplay Performance!\nPlease REJOIN IN 2 MINUTES to Play on a Fresh Server"; // CHANGE
int YellDuration = 15; // CHANGE

// If it's the appointed hour and a restart has not happened in the last 24 hours ...
if (DateTime.Now.Hour == Hour24Clock && (server.TimeUp/(60*60)) > 24) {
    Thread messenger = new Thread(new ThreadStart(delegate {
        try {
            plugin.PRoConChat("ADMIN > " + ChatMessage);
            plugin.SendGlobalMessage(ChatMessage);
            plugin.SendGlobalYell(YellMessage, YellDuration);
            Thread.Sleep(15 * 1000);
            plugin.PRoConChat("ADMIN > " + ChatMessage);
            plugin.SendGlobalMessage(ChatMessage);
            plugin.SendGlobalYell(YellMessage, YellDuration);
            Thread.Sleep(15 * 1000);
            plugin.PRoConChat("ADMIN > " + ChatMessage);
            plugin.SendGlobalMessage(ChatMessage);
            plugin.SendGlobalYell(YellMessage, YellDuration);
        } catch (Exception) {}
    }));
    messenger.Name = "RestartMessenger";
    messenger.Start();

    Thread timer = new Thread(new ThreadStart(delegate {
        try {
            Thread.Sleep(DelaySeconds * 1000);
            plugin.ConsoleWrite("^1AUTOMATIC RESTART OF ACTIVE SERVER");
            plugin.PRoConEvent("AUTOMATIC RESTART OF ACTIVE SERVER", "Insane Limits");
            plugin.ServerCommand("admin.shutDown");
        } catch (Exception) {}
    }));
    timer.Name = "AutoRestarter";
    timer.Start();
}

return false;