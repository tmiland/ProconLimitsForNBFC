/* Yell to Squad on Request

Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code:*/

if (!player.LastChat.StartsWith("ID_CHAT_"))
    return false;

	switch (player.LastChat)
	{
		case "ID_CHAT_ATTACK/DEFEND":
		{
			msg = player.Name + " Gave you an ORDER! Follow up!";
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
			msg = player.Name + " said Let's Go!";
		}	break;
		case "ID_CHAT_REQUEST_ORDER":
		{
			msg = player.Name + " requested ORDERS!";
		}	break;
		case "ID_CHAT_REQUEST_MEDIC":
		{
			msg = player.Name + " requested a MEDIC!";
		}	break;
		case "ID_CHAT_REQUEST_AMMO":
		{
			msg = player.Name + " requested AMMO!";
		}	break;
		case "ID_CHAT_REQUEST_RIDE":
		{
			msg = player.Name + " requested a RIDE!";
		}	break;
		case "ID_CHAT_GET_OUT":
		{
			msg = player.Name + " requested a SPOT!";
		}	break;
		case "ID_CHAT_GET_IN":
		{
			msg = player.Name + " has a free SPOT!";
		}	break;
		case "ID_CHAT_REQUEST_REPAIRS":
		{
			msg = player.Name + " requested REPAIRS!";
		}	break;
		case "ID_CHAT_AFFIRMATIVE":
		{
			msg = player.Name + " accepted REQUEST!";
		}	break;
		case "ID_CHAT_NEGATIVE":
		{
			msg = player.Name + " denied REQUEST!";
			break;
		}

        plugin.ConsoleWrite("Unknown commo rose chat code: " + player.LastChat);
        return false;
	}
		
	plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
	plugin.SendServerCommand("admin.yell", msg, 5, "squad", player.TeamId.ToString(), player.SquadId.ToString());
	plugin.PRoConEvent(msg, "Insane Limits");
	plugin.PRoConChat("^b^1ADMIN ORDERS >^0^n " + msg);

	return false;