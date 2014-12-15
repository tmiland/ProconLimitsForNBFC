/* Create a new limit to evaluate OnRoundOver, call it "Set Factions Based on Map", leave Action set to None.

Set first_check to this Code: */
String msg = "none";

int Team1 = 0;
int Team2 = 0;
int i = 0;

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
		Team1 = 0;
		Team2 = 1;
		break;
	case "MP_Prison":
	case "MP_Flooded":
		Team1 = 1;
		Team2 = 2;
		break;
	case "MP_Abandoned":
	case "MP_Naval":
	case "MP_Resort":
		Team1 = 0;
		Team2 = 2;
		break;

	default:
	Random rnd = new Random();
	Team1 = rnd.Next(0, 3);
	Team2 = rnd.Next(0, 3);
	i = 0;

	while (Team1 == Team2) {
		Team2 = rnd.Next(0, 3);
		i++;
		if(i>10){
			if		(Team1 == 0)
			Team2 = 1;
			else if	(Team1 == 1)
			Team2 = 2;
			else
			Team2 = 0;
			break;
		}
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