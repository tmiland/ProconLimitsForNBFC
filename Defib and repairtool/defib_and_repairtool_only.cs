/* On kill

First check, Expression: */
!Regex.Match(kill.Weapon, @"(U_Defib|U_Repairtool|Suicide|SoldierCollision|DamageArea|Death)", RegexOptions.IgnoreCase).Success
/*
Second check, code:
*/
String kCounter = killer.Name + "_TreatAsOne_Count_MaxKills";
TimeSpan time = TimeSpan.FromSeconds(3); // Activations within 3 seconds count as 1
String msg = null;
 
int warnings = 1;
if (server.Data.issetInt(kCounter)) warnings = server.Data.getInt(kCounter);

/*
Check to make sure we are not
getting multiple activations in a short period of time. Ignore if
less than the time span required.
*/
 
if (limit.Activations(killer.Name, time) > 1) return false;
 
/*
We get here only if there was exactly one activation in the time span
*/
 
if (warnings == 0) {
        msg = "You have " + (warnings) + " kill this round, the maximum allowed on this server is 0";
        plugin.ServerCommand("admin.say", msg, "15", "player", killer.Name);
        plugin.PRoConChat("ADMIN to " + killer.Name + ">" + msg);
} if (warnings >= 1) {
            msg ="not using Defib or Repairtool to kill ";
            plugin.EABanPlayerWithMessage(EABanType.EA_GUID, EABanDuration.Temporary, player.Name, 0, " not using Defib or Repairtool to kill!");
			plugin.ConsoleWrite("^b^1ILLEGAL WEAPON!^0^n " + killer.FullName + " used " + kill.Weapon + " against " + victim.FullName);
			msg = "Kicking " + killer.FullName + " for " + msg + "player used " + kill.Weapon + " against " + victim.FullName;
            plugin.SendGlobalMessage(msg);
            plugin.PRoConChat("ADMIN > " + msg);
            plugin.PRoConEvent(msg, "Insane Limits");
        }
server.Data.setInt(kCounter, warnings+1);

    return false;