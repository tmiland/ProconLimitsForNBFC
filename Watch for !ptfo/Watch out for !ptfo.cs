//watch for !ptfo
if (Regex.Match(player.LastChat, @"^\s*[!/](ptfo)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = "" + player.Name + " wants you to Play The Fucking Objective! ";
		plugin.SendGlobalMessage(msg);
		plugin.SendPlayerYell(player.Name, msg, 10);
		return true;
	}
	return false;