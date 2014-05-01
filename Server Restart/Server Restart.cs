/* Server Restart - https://forum.myrcon.com/showthread.php?7817-Plugin-to-auto-reboot-server&p=102493&viewfull=1#post102493
Set limit to evaluate OnIntervalServer, EvaluationInterval: 3600, set action to None

Set first_check to this Expression: */
(DateTime.Now.Hour == 6) && server.PlayerCount == 0

/* Set second_check to this Code: */
plugin.ServerCommand("admin.shutDown");
return false;
/* Note: Please make sure that your server is starting again after it was shut down */