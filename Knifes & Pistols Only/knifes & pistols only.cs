/* Set the limit to evaluate OnKill and set the Action to None.

Set first_check to this Expression: */
kill.Weapon == "U_M98B" || kill.Weapon == "U_M93R" || kill.Weapon == "U_GOL" || !Regex.Match(kill.Weapon, @"(U_Taurus44|U_HK45C|U_CZ75|U_FN57|U_M1911|U_M9|U_MP412Rex|U_MP443|U_P226|U_QSZ92|U_SW40|U_Flashbang|U_Defib|Melee|Suicide|SoldierCollision|DamageArea|Death)", RegexOptions.IgnoreCase).Success
/* 
Set second_check to this Code: 
*/
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
        msg = plugin.R("ATTENTION %k_n%! Do not use %w_n%! THIS IS KNIFES & PISTOLS ONLY!"); // First warning message
        plugin.ServerCommand("admin.say", msg, "player", killer.Name);
        plugin.SendPlayerYell(killer.Name, msg, 10);
        plugin.PRoConChat("ADMIN > " + msg);
		plugin.SendGlobalMessage(msg);
        plugin.KillPlayer(killer.Name, 3);
		plugin.ConsoleWrite("^b^1ILLEGAL WEAPON!^0^n " + killer.FullName + " used " + kill.Weapon + " against " + victim.FullName);

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
		plugin.ConsoleWrite("^b^1ILLEGAL WEAPON!^0^n " + killer.FullName + " used " + kill.Weapon + " against " + victim.FullName);

} else if (warnings == 2) {
        msg = plugin.R("Kicking %k_n% for ignoring warnings and killing with %w_n%!");
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
        plugin.KickPlayerWithMessage(killer.Name, msg);
} else if (warnings > 2) {
        msg = plugin.R("TBANNING %k_n% for 30mins. Still using PROHIBITED WEAPONS after being kicked!");
        plugin.SendGlobalMessage(msg);
        plugin.PRoConChat("ADMIN > " + msg);
        plugin.PRoConEvent(msg, "Insane Limits");
        plugin.EABanPlayerWithMessage(EABanType.Name, EABanDuration.Temporary, killer.Name, 30 /* minutes */, msg);
}
server.RoundData.setInt(kCounter, warnings+1);
return false;