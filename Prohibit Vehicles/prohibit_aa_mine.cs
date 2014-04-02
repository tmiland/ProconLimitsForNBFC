/* https://forum.myrcon.com/showthread.php?7617-Insane-Limits-Mobile-Anti-Air-and-AA-Mine-Limit
Prohibit AA Mine
OnKill
First check code: */
/* Version 9.16/R4 */
/* SETUP */

// Message templates
// {0} will be replaced with PlayerName
// {1} will be replaced by prohibited weapon/vehicle name, see below
String autoKilled = "{0} AUTO-KILLED for using the PROHIBITED {1}";
String autoKicked = "{0} AUTO-KICKED for using the PROHIBITED {1}";
String tempBan = "{0} TEMP BAN 1 HOUR for using the PROHIBITED {1}";
String yellKilled = "The {1} is prohibited. You will be AUTO-KICKED if you use it again.";

// Times
int yellTime = 20; // seconds
int banTime = 60; // minutes
double multiKillTime = 5; // seconds


// Weapon/vehicle codes

bool isAAMine = (kill.Weapon == "AA Mine");

/* CODE */

if (!isAAMine) return false;

String prohibited = "AA-Mine";

String key = "PersistAA_" + prohibited + "_" + killer.Name;

DateTime last = DateTime.MinValue;
if (server.Data.issetObject(key)) last = (DateTime)server.Data.getObject(key);
if (DateTime.Now.Subtract(last).TotalSeconds <= multiKillTime) return false;
server.Data.setObject(key, (Object)DateTime.Now);

int count = 0;
if (plugin.Data.issetInt(key)) count = plugin.Data.getInt(key);

count = count + 1;

String msg = null;

if (count == 1) { // First Violation: Kill

    msg = String.Format(autoKilled, killer.Name, prohibited);
    plugin.SendGlobalMessage(msg);
    plugin.SendPlayerYell(killer.Name, String.Format(yellKilled, killer.Name, prohibited), yellTime);
    plugin.PRoConChat("ADMIN > " + msg);
    plugin.PRoConEvent(msg, "Insane Limits");
    plugin.KillPlayer(killer.Name, 6);

} else if (count == 2) { // Second Violation: Kick

    msg = String.Format(autoKicked, killer.Name, prohibited);
    plugin.SendGlobalMessage(msg);
    plugin.PRoConChat("ADMIN > " + msg);
    plugin.PRoConEvent(msg, "Insane Limits");
    plugin.KickPlayerWithMessage(killer.Name, msg);

} else { // Third or subsequent Violation: TBan

    msg = String.Format(tempBan, killer.Name, prohibited);
    plugin.SendGlobalMessage(msg);
    plugin.PRoConChat("ADMIN > " + msg);
    plugin.PRoConEvent(msg, "Insane Limits");
    plugin.EABanPlayerWithMessage(EABanType.Name, EABanDuration.Temporary, killer.Name, banTime, msg);

}

plugin.Data.setInt(key, count);
return false;