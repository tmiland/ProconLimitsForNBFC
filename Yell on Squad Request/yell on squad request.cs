/* Yell to Squad on Request

Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code:*/

	string msg = "none";

	if (Regex.Match(player.LastChat, @"ID_CHAT_ATTACK/DEFEND", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " Gave you an ORDER! Follow up!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " Gave an ORDER! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_THANKS", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " says: THANK YOU!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " said THANK YOU! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_SORRY", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " says: SORRY!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " said SORRY! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_GOGOGO", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + ": Let's Go!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " said Let's Go! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_REQUEST_ORDER", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " requested ORDERS! Give it to him!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " requested ORDERS! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_REQUEST_MEDIC", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " needs MEDICAL ATTENTION!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " requested a MEDIC! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_REQUEST_AMMO", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " needs AMMO! Someone Give it to him!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " requested AMMO! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_REQUEST_RIDE", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " needs a RIDE! Someone Go pick him up!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " requested a RIDE! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_GET_OUT", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " needs a SPOT in the VEHICLE! GTFO!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " requested a SPOT! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_GET_IN", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " has a free SPOT in the VEHICLE! Get in!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " has a free SPOT! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_REQUEST_REPAIRS", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " needs REPAIRS! Someone Go fix him!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " requested REPAIRS! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_AFFIRMATIVE", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " accepted your REQUEST!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " accepted REQUEST! ");
	}
	else if (Regex.Match(player.LastChat, @"ID_CHAT_NEGATIVE", RegexOptions.IgnoreCase).Success)
	{
		msg = player.Name + " denied your REQUEST!";
		plugin.SendSquadMessage(player.TeamId, player.SquadId, msg);
		plugin.SendPlayerYell(player.Name, msg, 5);
		plugin.PRoConEvent(msg, "Insane Limits");
		plugin.ConsoleWrite("^b^1ADMIN ORDERS >^0^n " + player.FullName + " denied REQUEST! ");
	}
	
	return false;