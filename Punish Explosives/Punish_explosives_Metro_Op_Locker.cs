/* https://forum.myrcon.com/showthread.php?4014-Insane-Limits-Punish-explosives-M320-RPG-USAS-Metro-Op-Locker
BF4 for BOTH Locker and Metro 2014
Set the limit to evaluate OnKill and set the Action to None.

Set first_check to this Expression: */
/*---------- With exeptions ------------*/
(Regex.Match(server.MapFileName, @"(?:MP_Prison|XP0_Metro)", RegexOptions.IgnoreCase).Success && (Regex.Match(kill.Weapon, @"(AA Mine|M320|_LVG|_HE|_3GL|_AT4|_C4|_Claymore|_FGM148|_FIM92|_FLASH|_Flashbang|_GrenadeRGO|_M15|_M224|_M34|_M67|_MGL|_NLAW|_RPG7|_Sa18IGLA|_SLAM|_SMAW|_SRAW|_Starstreak|_Tomahawk|_UCAV|_V40|_XM25|ROADKILL|Death)", RegexOptions.IgnoreCase).Success && !Regex.Match(kill.Weapon, @"(_SMK|_Smoke|_Suicide|_SoldierCollision|_DamageArea)", RegexOptions.IgnoreCase).Success))
/*---------- With Weapon Categories ----------*/
(
Regex.Match(server.MapFileName, @"(?:MP_Prison|XP0_Metro)", RegexOptions.IgnoreCase).Success 
&&  Regex.Match(kill.Category, @"(Explosive|Impact)").Success
)
/* Set second_check to this Code: */
/* Version: V0.9.15/R1 */
String kCounter = killer.Name + "_TreatAsOne_Count";
TimeSpan time = TimeSpan.FromSeconds(3); // Activations within 3 seconds count as 1

int warnings = 0;
if (server.Data.issetInt(kCounter)) warnings = server.Data.getInt(kCounter);
    
/*
The first time through, warnings is zero. Whether this is an isolated
activation or the first of a sequence of activations in a short period
of time, do something on this first time through.
*/
String msg = "none";
if (warnings == 0) {
        msg = plugin.R("ATTENTION %k_n%! Do not use %w_n%! THIS IS NO EXPLOSIVES!"); // First warning message
        plugin.ServerCommand("admin.say", msg, "player", killer.Name);
        plugin.SendPlayerYell(killer.Name, msg, 20);
        plugin.PRoConChat("ADMIN > " + msg);
		plugin.SendGlobalMessage(msg);
		plugin.ConsoleWrite("^b^1ILLEGAL WEAPON!^0^n " + killer.FullName + " used " + kill.Weapon + " against " + victim.FullName);
        plugin.KillPlayer(killer.Name, 3);
        server.Data.setInt(kCounter, warnings+1);
        return false;
}

/*
The second and subsequent times through, check to make sure we are not
getting multiple activations in a short period of time. Ignore if
less than the time span required.
*/

if (limit.Activations(killer.Name, time) > 1) return false;

/*
We get here only if there was exactly one activation in the time span
*/

if (warnings == 1) {
        msg = plugin.R("FINAL WARNING %k_n%! Do not use %w_n%! Next Violation is a KICK!"); // Second warning message
        plugin.ServerCommand("admin.say", msg, "player", killer.Name);
        plugin.SendPlayerYell(killer.Name, msg, 20);
        plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ILLEGAL WEAPON!^0^n " + killer.FullName + " used " + kill.Weapon + " against " + victim.FullName);
        plugin.KillPlayer(killer.Name, 3);
} else if (warnings == 2) {
        msg = plugin.R("Kicking %k_n% for ignoring warnings and killing with %w_n%!");
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
        plugin.KickPlayerWithMessage(killer.Name, msg);
} else if (warnings > 3) {
        msg = plugin.R("TBANNING %k_n% for 30mins. Still using EXPLOSIVES after being kicked!");
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
        plugin.EABanPlayerWithMessage(EABanType.Name, EABanDuration.Temporary, killer.Name, 30 /* minutes */, msg);
}
server.Data.setInt(kCounter, warnings+1);
return false;