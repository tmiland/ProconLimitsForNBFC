/* Announce Top Scoring Clan
This limit will wait until there are less than 20 tickets remaining in either team. It will announce three times the name of the top scoring clan.

Set limit to evaluate OnSpawn, set action to None

Set first_check to this Expression: */

(team1.RemainTicketsPercent < 20 || team2.RemainTicketsPercent < 20)

/* Set second_check to this Code: */

    double count = limit.Activations();
    
    if (count > 3)
        return false;

    List<PlayerInfoInterface> players = new List<PlayerInfoInterface>();
    players.AddRange(team1.players);
    players.AddRange(team2.players);
    Dictionary<String, double> clan_stats = new Dictionary<String, double>();
    
    /* Collect clan statistics */
    foreach(PlayerInfoInterface player_info in players)
    {
        if(player_info.Tag.Length == 0)
            continue;
        
        if (!clan_stats.ContainsKey(player_info.Tag))
            clan_stats.Add(player_info.Tag, 0);
        
        clan_stats[player_info.Tag] += player_info.ScoreRound;
    }

    /* Find the best scoring clan */
    String best_clan = String.Empty;
    double best_score = 0;
    
    foreach(KeyValuePair<String, double> pair in clan_stats)
        if (pair.Value > best_score)
        {
             best_clan = pair.Key;
             best_score = pair.Value;
        }
    
    if (best_clan.Length > 0)
	{
		String message = "Top scoring clan this round is "+ best_clan + " with " + best_score + " points!";
                plugin.SendGlobalMessage(message); 
		plugin.ConsoleWrite(message);
        }   
    return false;