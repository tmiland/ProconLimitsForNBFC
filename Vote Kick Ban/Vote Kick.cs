/* This limit allows players to use In-Game command "!votekick <player_name>". Players can vote as many times they want, and against as many players they want. The are no time or concurrency restrictions. However, votes are only counted once (you can only vote once against a certain player). If any player accumulates votes from more than 50% of all players in the server, that player is kicked. All votes are reset at the end of a round. The player name does not have to be a full-name. It can be a sub-string or misspelled name.

Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code: */
double percent = 50;

string fancy_time = DateTime.Now.ToString("HH:mm:ss");
string fancy_date = DateTime.Now.ToString("dd-MM-yyyy");
/* Verify that it is a command */
if (!plugin.IsInGameCommand(player.LastChat))
    return false;

/* Extract the command */
String command = plugin.ExtractInGameCommand(player.LastChat);

/* Sanity check the command */
if (command.Length == 0)
    return false;
    
/* Parse the command */
Match kickMatch = Regex.Match(command, @"^votekick\s+([^ ]+)", RegexOptions.IgnoreCase);

/* Bail out if not a vote-kick */
if (!kickMatch.Success)
    return false;
    
/* Extract the player name */
PlayerInfoInterface target = plugin.GetPlayer(kickMatch.Groups[1].Value.Trim(), true);

if (target == null)
    return false;
    
if (target.Name.Equals(player.Name))
{
    plugin.SendSquadMessage(player.TeamId, player.SquadId, plugin.R("player.Name, you cannot vote-kick yourself!"));
    return false;
}
// Whitelist Reserved players
List<String> ReservervedSlots = plugin.GetReservedSlotsList();
{
	if (ReservervedSlots.Contains(target.Name))
    plugin.SendSquadMessage(player.TeamId, player.SquadId, plugin.R("player.Name, you cannot vote-kick reserved players!"));
	return false;
}   
/* Account the vote in the voter's dictionary */
/* Votes are kept with the voter, not the votee */
/* If the voter leaves, his votes are not counted */

if (!player.DataRound.issetObject("votekick"))
    player.DataRound.setObject("votekick", new Dictionary<String, bool>());

Dictionary<String, bool> vdict =  (Dictionary<String, bool>) player.DataRound.getObject("votekick");

if (!vdict.ContainsKey(target.Name))
    vdict.Add(target.Name, true);


/* Tally the votes against the target player */
double votes = 0;
List<PlayerInfoInterface> all = new List<PlayerInfoInterface>();
all.AddRange(team1.players);
all.AddRange(team2.players);
all.AddRange(team3.players);
all.AddRange(team4.players);

foreach(PlayerInfoInterface p in all)
    if (p.DataRound.issetObject("votekick"))
    {
       Dictionary<String, bool> pvotes = (Dictionary<String, bool>) p.DataRound.getObject("votekick");
       if (pvotes.ContainsKey(target.Name) && pvotes[target.Name])
           votes++;
    }

if (all.Count == 0)
    return false;
    
int needed = (int) Math.Ceiling((double) all.Count * (percent/100.0));
int remain = (int) ( needed - votes);

if (remain == 1)
   plugin.SendGlobalMessage(target.Name + " is about to get vote-kicked, 1 more vote needed");
else if (remain > 0)
   plugin.SendSquadMessage(player.TeamId, player.SquadId, plugin.R("player.Name, your vote against " + target.Name + " was counted, " + remain + " more needed to kick"));

if (remain > 0)
    plugin.ConsoleWrite(player.Name + ", is trying to vote-kick " + target.Name + ", " + remain + " more votes needed");

if (votes >= needed)
{
    String count = "with " + votes + " vote" + ((votes > 1)?"s":"");
    String message = target.Name + " was vote-kicked " + count ;
    plugin.SendGlobalMessage(message);
    plugin.ConsoleWrite(message);
    plugin.KickPlayerWithMessage(target.Name, target.Name + ", you were vote-kicked from the server " + count);
	plugin.Log("Logs/InsaneLimits_vote-kick.log", plugin.R("[" + fancy_date + "][" + fancy_time + "] %p_n% has been vote kicked"));
    return true;
}

return false;