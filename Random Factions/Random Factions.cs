/* Random Factions
Set limit to evaluate OnRoundOver, set action to None

Set first_check to this Code:*/

string msg = "none";

Random rnd = new Random();
int Team1 = rnd.Next(0, 3);
int Team2 = rnd.Next(0, 3);

while (Team1 == Team2) {
Team2 = rnd.Next(0, 3);
}
    
Dictionary<int,String> Teams = new Dictionary<int,String>();
Teams.Add(0, "US");
Teams.Add(1, "Russia");
Teams.Add(2, "China");


plugin.ServerCommand("vars.teamFactionOverride", "1", Convert.ToString(Team1));
plugin.ServerCommand("vars.teamFactionOverride", "2",  Convert.ToString(Team2));

msg = "Scrambling next round team factions...";
plugin.SendGlobalMessage(msg, 12);
plugin.ConsoleWrite("^b^1ADMIN SCRAMBLE >^0^n " + msg);
plugin.PRoConChat("^b^1ADMIN SCRAMBLE >^0^n " + msg);
plugin.PRoConEvent(msg, "Insane Limits");

return false;