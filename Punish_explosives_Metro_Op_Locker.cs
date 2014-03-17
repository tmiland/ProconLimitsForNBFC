/* BF4 for BOTH Locker and Metro 2014
Set the limit to evaluate OnKill and set the Action to None.

Set first_check to this Expression: */
(Regex.Match(server.MapFileName, @"(?:MP_Prison|XP0_Metro)", RegexOptions.IgnoreCase).Success && Regex.Match(kill.Weapon, @"(M320|RPG|SMAW|C4|M67|Claymore|FGM-148|FIM92|ROADKILL|Death|_LVG|_HE|_Frag|_XM25|_FLASH|_V40|_M34|_SMK|_Smoke|_FGM148|_Grenade|_SLAM|_NLAW|_RPG7|_C4|_Claymore|_FIM92|_M67|_SMAW|_SRAW|_Sa18IGLA|_Tomahawk)", RegexOptions.IgnoreCase).Success)
/*---------- With exeptions------------*/
(Regex.Match(server.MapFileName, @"(?:MP_Prison|XP0_Metro)", RegexOptions.IgnoreCase).Success && (Regex.Match(kill.Weapon, @"(AA Mine|M320|LVG|HE|3GL|AT4|C4|Claymore|FGM148|FIM92|FLASH|Flashbang|GrenadeRGO|M15|M224|M34|M67|MGL|NLAW|RPG7|Sa18IGLA|SLAM|SMAW|SRAW|Starstreak|Tomahawk|UCAV|V40|XM25)", RegexOptions.IgnoreCase).Success && !Regex.Match(kill.Weapon, @"(SMK|Smoke|Flechette|Slug)", RegexOptions.IgnoreCase).Success))
/* Set second_check to this Code: */
/* Version: V0.9.15/R1 */
String kCounter = killer.Name + "_TreatAsOne_Count";
TimeSpan time = TimeSpan.FromSeconds(3); // Activations within 3 seconds count as 1

int warnings = 0;
if (server.RoundData.issetInt(kCounter)) warnings = server.RoundData.getInt(kCounter);
    
/*
The first time through, warnings is zero. Whether this is an isolated
activation or the first of a sequence of activations in a short period
of time, do something on this first time through.
*/
String msg = "none";
if (warnings == 0) {
        msg = plugin.R("ATTENTION %k_n%! Do not use %w_n%! THIS IS NO EXPLOSIVES!"); // First warning message
        plugin.ServerCommand("admin.say", msg, "player", killer.Name);
        plugin.SendPlayerYell(killer.Name, msg, 10);
        plugin.PRoConChat("ADMIN > " + msg);
		plugin.SendGlobalMessage(msg);
        plugin.KillPlayer(killer.Name, 3);
        server.RoundData.setInt(kCounter, warnings+1);
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
        plugin.SendPlayerYell(killer.Name, msg, 10);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.KillPlayer(killer.Name, 3);
} else if (warnings == 2) {
        msg = plugin.R("Kicking %k_n% for ignoring warnings and killing with %w_n%!");
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
        plugin.KickPlayerWithMessage(killer.Name, msg);
} else if (warnings > 2) {
        msg = plugin.R("TBANNING %k_n% for 30mins. Still using EXPLOSIVES after being kicked!");
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
        plugin.EABanPlayerWithMessage(EABanType.Name, EABanDuration.Temporary, killer.Name, 30 /* minutes */, msg);
}
server.RoundData.setInt(kCounter, warnings+1);
return false;