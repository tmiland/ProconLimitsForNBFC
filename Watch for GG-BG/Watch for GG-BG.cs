//watch for gg/bg - Hard/Easy
if (Regex.Match(player.LastChat, @"^\s*(gg)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = player.Name + " had a Good Game this round! ";
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + msg);
		plugin.PRoConEvent(msg, "Insane Limits");
		return true;
	}
	else if (Regex.Match(player.LastChat, @"^\s*(bg)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = player.Name + " had a Bad Game this round! ";
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + msg);
		plugin.PRoConEvent(msg, "Insane Limits");
		return true;
	}
	else if (Regex.Match(player.LastChat, @"^\s*(easy)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = player.Name + " had a Easy Game this round! ";
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + msg);
		plugin.PRoConEvent(msg, "Insane Limits");
		return true;
	}
	else if (Regex.Match(player.LastChat, @"^\s*(hard)$", RegexOptions.IgnoreCase).Success)
	{
		String msg = player.Name + " had a Hard Game this round! ";
		plugin.SendGlobalMessage(msg);
		plugin.PRoConChat("ADMIN > " + msg);
		plugin.ConsoleWrite("^b^1ADMIN >^0^n " + msg);
		plugin.PRoConEvent(msg, "Insane Limits");
		return true;
	}
	return false;