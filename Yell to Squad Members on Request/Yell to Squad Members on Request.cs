/* Yell to Squad Members on Request

Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code:*/

string msg = "none";

if (!player.LastChat.StartsWith("ID_CHAT_"))
    return false;

switch (player.LastChat)
	{
		case "ID_CHAT_ATTACK/DEFEND":
		{
			msg = player.Name + " Gave you an ORDER Soldier! PTFO!";
		}	break;
		case "ID_CHAT_THANKS":
		{
			msg = player.Name + " says: THANK YOU!";
		}	break;
		case "ID_CHAT_SORRY":
		{
			msg = player.Name + " says: SORRY!";
		}	break;
		case "ID_CHAT_GOGOGO":
		{
			msg = player.Name + " said: Let's Go!";
		}	break;
		case "ID_CHAT_REQUEST_ORDER":
		{
			msg = player.Name + ": What are the objective Squad Leader?";
		}	break;
		case "ID_CHAT_REQUEST_MEDIC":
		{
			msg = player.Name + " requested a MEDIC! Throw out a bag!";
		}	break;
		case "ID_CHAT_REQUEST_AMMO":
		{
			msg = player.Name + " requested AMMO! Throw out a bag!";
		}	break;
		case "ID_CHAT_REQUEST_RIDE":
		{
			msg = player.Name + " requested a RIDE! Go pick him up!";
		}	break;
		case "ID_CHAT_GET_OUT":
		{
			msg = player.Name + " requested a SPOT! Get out of the vehicle!";
		}	break;
		case "ID_CHAT_GET_IN":
		{
			msg = player.Name + " has a free SPOT! Get in the vehicle!";
		}	break;
		case "ID_CHAT_REQUEST_REPAIRS":
		{
			msg = player.Name + " requested REPAIRS! Now! Go! Go! Go!";
		}	break;
		case "ID_CHAT_AFFIRMATIVE":
		{
			msg = player.Name + ": Affirmative!";
		}	break;
		case "ID_CHAT_NEGATIVE":
		{
			msg = player.Name + " NO! I cannot do that!";
		}	break;
        default:
        plugin.ConsoleWrite("Unknown commo rose chat code: " + player.LastChat);
        return false;
	}
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
	// For writing to console
	plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + msg);
	// For writing to chat
	//plugin.PRoConChat("^b^1ADMIN ORDERS >^0^n " + msg);
	plugin.PRoConEvent(msg, "Insane Limits");

	return false;