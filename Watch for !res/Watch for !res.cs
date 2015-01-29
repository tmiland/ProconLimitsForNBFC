/* Watch for !res
First check expression: */

(Regex.Match(player.LastChat, @"^\s*[!/](res)$", RegexOptions.IgnoreCase).Success)

/*  Second check code: */
	String msg = player.Name + " is DEAD and needs to be resurrected! ";

	// We need a list for notification
	List<PlayerInfoInterface> callersTeam = new List<PlayerInfoInterface>();

	// Get a list of players on caller's team
	switch (player.TeamId)
	{
		case 1:
		{
			callersTeam.AddRange(team1.players);
		}	break;
		case 2:
		{
			callersTeam.AddRange(team2.players);
		}	break;
		case 3:
		{
			callersTeam.AddRange(team3.players);
		}	break;
		case 4:
		{	callersTeam.AddRange(team4.players);
			break;
		}
	}
	if (!player.Data.issetBool("NoYell"))
	{
		player.Data.setBool("NoYell", true);
	}
	// Send the message only to the players in the same squad
	foreach (PlayerInfoInterface p in callersTeam)
	{
		if ((p.Name != player.Name) && (p.SquadId == player.SquadId))
		{
			plugin.SendPlayerYell(p.Name, msg, 5);
		}
	}
	if (player.Data.getBool("NoYell"))
	{
		// Send msg to squad chat if @noyell is off
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
	}
	
	return false;