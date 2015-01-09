/* Set the limit to evaluate OnIntervalServer, with interval set to 30 seconds and name it "Change IO". Set the Action to None.

Set first_check to this Code: */

/* VERSION 0.8/R1 - vote io */
String kVoteResult = "voteinfantry_result"; // plugin.Data bool
String kState = "voteinfantry_state"; // plugin.Data int
String kNextMap = "voteinfantry_map"; // plugin.Data String
String kNextMode = "voteinfantry_mode"; // plugin.Data String
String kCurrRound = "voteinfantry_round"; // plugin.Data int

double minTicketPercent = 5.0;

int level = 2;

try {
	level = Convert.ToInt32(plugin.getPluginVarValue("debug_level"));
} catch (Exception e) {}

if (!plugin.Data.issetInt(kState)) {
	plugin.Data.setInt(kState, 0);
	if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n initialized to State 0");
}

int state = plugin.Data.getInt(kState);
int nextState = state;

switch (state) {
	case 0: { // Idle
		if (plugin.Data.issetBool(kVoteResult)) {
			if (plugin.Data.getBool(kVoteResult)) {
				nextState = 1;
				if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n successful vote detected!");
				// Remember the next map/mode and current round number
				plugin.Data.setString(kNextMap, server.NextMapFileName);
				plugin.Data.setString(kNextMode, server.NextMapFileName);
				plugin.Data.setInt(kCurrRound, server.CurrentRound);
			}
		plugin.Data.unsetBool(kVoteResult);
		}
		break;
	}
	case 1: { // Detect round change and change config
		bool headStart = (server.RemainTicketsPercent(1) < minTicketPercent || server.RemainTicketsPercent(2) < minTicketPercent || (team3.players.Count > 0 && server.RemainTicketsPercent(3) < minTicketPercent) || (team4.players.Count > 0 && server.RemainTicketsPercent(4) < minTicketPercent));
		bool roundChanged = false;
		if ((plugin.Data.getString(kNextMap) == server.MapFileName && plugin.Data.getString(kNextMode) == server.Gamemode) || plugin.Data.getInt(kCurrRound) != server.CurrentRound) {
			roundChanged = true;
			if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n round change detected!");
			nextState = 2;
			// Remember the next map/mode and current round number
			plugin.Data.setString(kNextMap, server.NextMapFileName);
			plugin.Data.setString(kNextMode, server.NextMapFileName);
			plugin.Data.setInt(kCurrRound, server.CurrentRound);
		}
		if (headStart || roundChanged) {
			// CUSTOMIZE
			plugin.ServerCommand("vars.preset", "INFANTRY", "true");
			server.Data.setString("RememberPreset", "INFANTRY"); // where 'preset' is the string value of the preset, i.e., "Normal" or "Infantry".
			if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n setting preset to IO!");
		}
		break;
	}
	case 2: { // Detect round change and revert to default config
		bool headStartRevert = (server.RemainTicketsPercent(1) < minTicketPercent || server.RemainTicketsPercent(2) < minTicketPercent || (team3.players.Count > 0 && server.RemainTicketsPercent(3) < minTicketPercent) || (team4.players.Count > 0 && server.RemainTicketsPercent(4) < minTicketPercent));
		bool roundRevert = false;
		bool skip = false;
		if (plugin.Data.issetBool(kVoteResult)) {
			if (plugin.Data.getBool(kVoteResult)) {
				nextState = 1;
				if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n another successful vote detected!");
				// Remember the next map/mode and current round number
				plugin.Data.setString(kNextMap, server.NextMapFileName);
				plugin.Data.setString(kNextMode, server.NextMapFileName);
				plugin.Data.setInt(kCurrRound, server.CurrentRound);
				headStartRevert = false;
				skip = true;
			}
			plugin.Data.unsetBool(kVoteResult);
		}
		if (!skip) {
			if ((plugin.Data.getString(kNextMap) == server.MapFileName && plugin.Data.getString(kNextMode) == server.Gamemode) || plugin.Data.getInt(kCurrRound) != server.CurrentRound) {
				roundRevert = true;
				if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n round revert detected!");
				nextState = 0;
				// Forget
				plugin.Data.unsetString(kNextMap);
				plugin.Data.unsetString(kNextMode);
				plugin.Data.unsetInt(kCurrRound);
				plugin.Data.unsetBool(kVoteResult);		
			}
			if (headStartRevert || roundRevert) {
				// CUSTOMIZE
				plugin.ServerCommand("vars.preset", "NORMAL", "true");
				server.Data.setString("RememberPreset", "NORMAL"); // where 'preset' is the string value of the preset, i.e., "Normal" or "Infantry".
				if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n setting preset to NORMAL!");
			}
		}
		break;	
	}
	default: nextState = 0; break;
}

if (state != nextState) {
	if (level >= 3) plugin.ConsoleWrite("^b[Change IO]^n changing to State " + nextState);
	plugin.Data.setInt(kState, nextState);
}

return false;