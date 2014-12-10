/* Create a new limit to evaluate OnRoundOver, call it "Set Factions Based on Map", leave Action set to None.

Set first_check to this Code: */
/*
plugin.ServerCommand("mapList.getMapIndices");
int nextRound = server.CurrentRound + 2;
if (nextRound > server.TotalRounds) nextRound = 1;

if ((server.CurrentRound +1) < server.TotalRounds) {
	plugin.ConsoleWrite("^2Next round is ^b" + plugin.FriendlyMapName(server.MapFileName) + "^n, round " + (server.CurrentRound+1) + " of " + server.TotalRounds + ", with " + server.PlayerCount + " players remaining");
	return false;
}

plugin.ConsoleWrite("^2Next map is ^b" + plugin.FriendlyMapName(server.NextMapFileName) + "^n, round " + nextRound + ", with " + server.PlayerCount + " players remaining");
*/
String msg = "none";

int Team1 = 0;
int Team2 = 0;

Dictionary<int,String> Teams = new Dictionary<int,String>();
Teams.Add(0, "US");
Teams.Add(1, "RU");
Teams.Add(2, "CN");

switch (server.NextMapFileName)
{
	case "MP_Tremors":
	case "MP_Siege":
	case "MP_Damage":
	case "MP_Journey":
	case "MP_TheDish":	
		Team1 = 1;
		Team2 = 0;
		break;
	case "MP_Prison":
	case "MP_Flooded":
		Team1 = 2;
		Team2 = 1;
		break;
	case "MP_Abandoned":
	case "MP_Naval":
	case "MP_Resort":
		Team1 = 2;
		Team2 = 0;
		break;
	default:
		Random rnd = new Random();
		Team1 = rnd.Next(0, 3);
		Team2 = rnd.Next(0, 3);

		while (Team1 == Team2)
		{
			Team2 = rnd.Next(0, 3);
		}
		break;
}

plugin.ServerCommand("vars.teamFactionOverride", "1", Convert.ToString(Team1));
plugin.ServerCommand("vars.teamFactionOverride", "2", Convert.ToString(Team2));
msg = "Setting factions to " + Teams[Team1] + " vs " + Teams[Team2] + " on next round...";

plugin.SendGlobalMessage(msg, 12);
plugin.ConsoleWrite("^b^1ADMIN FACTIONS >^0^n " + msg);
plugin.PRoConChat("^b^1ADMIN FACTIONS >^0^n " + msg);
plugin.PRoConEvent(msg, "Insane Limits");

return false;