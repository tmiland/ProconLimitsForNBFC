//watch for !help/backup
if (Regex.Match(player.LastChat, @"^\s*[@!/](help)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = player.Name + " NEEDS HELP! ";
		plugin.SendGlobalMessage(msg);
		plugin.SendGlobalYell(msg, 5);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + msg);
		plugin.PRoConEvent(msg, "Insane Limits");
		return true;
	}
	else if (Regex.Match(player.LastChat, @"^\s*[@!/](backup)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = player.Name + " REQUESTED BACKUP! ";
		plugin.SendGlobalMessage(msg);
		plugin.SendGlobalYell(msg, 5);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + msg);
		plugin.PRoConEvent(msg, "Insane Limits");
		return true;
	}
	return false;