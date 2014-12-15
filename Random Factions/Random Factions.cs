/* Random Factions - https://forum.myrcon.com/showthread.php?7236-Insane-Limits-Random-Factions
 Add a new limit with the following settings:
OnRoundOver

Code: */
if(((server.CurrentRound +1) >= server.TotalRounds) && (server.NextMapFileName == "XP3_UrbanGdn" || server.NextMapFileName == "X0_Oman")) {
	return false;
	}

Random rnd = new Random();
int Team1 = rnd.Next(0, 3);
int Team2 = rnd.Next(0, 3);
int i = 0;

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
    
Dictionary<int,String> Teams = new Dictionary<int,String>();
Teams.Add(0, "US");
Teams.Add(1, "RU");
Teams.Add(2, "CN");

plugin.ServerCommand("vars.teamFactionOverride", "1", Convert.ToString(Team1));
plugin.ServerCommand("vars.teamFactionOverride", "2",  Convert.ToString(Team2));

plugin.SendGlobalMessage("Scrambling next round team factions...", 12);

return false;