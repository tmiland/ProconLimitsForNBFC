/* Set limit to evaluate OnIntervalServer, EvaluationInterval: 60, set action to None

Set first_check to this Code: */
int threshold = 32;
int idleTimeout = 300;

if (server.PlayerCount < threshold ) {
plugin.ServerCommand("vars.idleTimeout", "86400");
plugin.ConsoleWrite("^2idleTimeout: ^b86400^n");
}
else {
plugin.ServerCommand("vars.idleTimeout", "idleTimeout.ToString()");
plugin.ConsoleWrite("^2idleTimeout: ^b" + idleTimeout + "^n");
}
return false;