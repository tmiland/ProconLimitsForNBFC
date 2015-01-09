/* Random Kill Announcements

Set limit to evaluate OnKill, set action to None

Set first_check to this Code:*/

if (!kill.Weapon.StartsWith("U_"))
    return false;

string msg;
List<string> MessageType1 = new List<string>();
List<string> MessageType2 = new List<string>();
List<string> MessageType3 = new List<string>();
List<string> MessageType4 = new List<string>();
List<string> MessageType5 = new List<string>();
List<string> MessageType6 = new List<string>();
List<string> MessageType7 = new List<string>();
List<string> MessageType8 = new List<string>();
List<string> MessageType9 = new List<string>();
List<string> MessageType10 = new List<string>();
List<string> MessageType11 = new List<string>();
List<string> MessageType12 = new List<string>();
List<string> MessageType13 = new List<string>();
Random rnd = new Random();

// Add all your messages which should be shown on "Melee"
MessageType1.Add(@"%k_fn% just sliced %v_n%'s throat, what a shame, bro!");
MessageType1.Add(@"%v_n% was shanked by %k_fn%!");
MessageType1.Add(@"%k_fn% just took %v_n%'s tags and made him cry!");
MessageType1.Add(@"%k_fn% slipped a shiv into %v_n%'s back!");
MessageType1.Add(@"%k_fn% knifed %v_n% and Insane Limits approves!");
MessageType1.Add(@"%v_n%, you gonna let %k_fn% get away with taking your tags?");
MessageType1.Add(@"%k_fn% just Tweeted about knifing %v_n%!");
MessageType1.Add(@"%k_fn% just posted %v_n%'s tags on Facebook!");
MessageType1.Add(@"%k_fn%: 'Just die already, %v_n%'"); // From BC2
MessageType1.Add(@"%k_fn%: 'Hey %v_n%, you want summa this?'"); // From BC2
MessageType1.Add(@"%v_n% took a gun to a knife fight with %k_fn%, and LOST!");
MessageType1.Add(@"Did you see the YouTube of %k_fn% knifing %v_n%?");
MessageType1.Add(@"%k_fn% just added +1 knife kills to his Battlelog stats, thanks to %v_n%");
MessageType1.Add(@"%k_fn%: 'Hey %v_n%, check your six next time!'");
MessageType1.Add(@"%v_n%, go tell your momma you lost your tags to %k_fn%!");
MessageType1.Add(@"%v_n% just wanted to see %k_fn%'s Premium knife, not have it shoved in his eye!");

// Add all your messages which should be shown on "RoadKill"
MessageType2.Add(@"WROOM! %k_fn% just roadkilled %v_n%!");
MessageType2.Add(@"WROOM! %k_fn% just ran over %v_n%!");

// Add all your messages which should be shown on "Suicide|DamageArea|SoldierCollision"
MessageType3.Add(@"CALL THE MORGUE! %v_n% just committed Suicide!");

// Add all your messages which should be shown on "VehicleUpsideDown"
MessageType4.Add(@"UH OH! %v_n% just flipped his vehicle upside down and died!");

// Add all your messages which should be shown on "Defib"
MessageType5.Add(@"WOAH! %k_fn% just ZAPPED %v_n% to death with his %w_n%!");
MessageType5.Add(@"ZAP! %k_fn% just ELECTROCUTED %v_n% with his %w_n%!");

// Add all your messages which should be shown on "AA Mine"
MessageType6.Add(@"%v_n% just flew over %k_fn%'s %w_n%!");

// Add all your messages which should be shown on "BallisticShield"
MessageType7.Add(@"SLAM! %k_fn% has killed %v_n% with his %w_n%!");

// Add all your messages which should be shown on "C4"
MessageType8.Add(@"BOOM! %k_fn% just BLEW UP %v_n% with %w_n%!");

// Add all your messages which should be shown on "Claymore"
MessageType9.Add(@"WOHAA! %v_n% just stepped into %k_fn%'s %w_n%!");

// Add all your messages which should be shown on "Medkit"
MessageType10.Add(@"MEDKILL! %k_fn% just dropped a %w_n% on %v_n%!");
MessageType10.Add(@"MEDKILL! %k_fn% just KILLED %v_n% with his %w_n%!");

// Add all your messages which should be shown on ""
MessageType11.Add(@"BAM! %k_fn% just killed %v_n% with a %w_n%!");
MessageType11.Add(@"SLAM! %v_fn% just ran in to %k_n%'s %w_n%'s!");

// Add all your messages which should be shown on "EODBot|Repairtool"
MessageType12.Add(@"%k_fn% just BURNED %v_n%'s ass with a %w_n%!");
MessageType12.Add(@"%k_fn% just TORCHED %v_n% with %w_n%!");

// Add all your messages which should be shown on ""
MessageType13.Add(@"BOMBS AWAY! %k_fn% just dropped a %w_n% on %v_n%!");

	// Do the switch
	switch (kill.Weapon)
	{
		case "Melee|Knife":
		{
			msg = MessageType1[rnd.Next(MessageType1.Count)]; // Take a random message from Message Type 1
		}	break;
		case "RoadKill":
		{
			msg = MessageType2[rnd.Next(MessageType2.Count)]; // Take a random message from Message Type 2
		}	break;
		case "Suicide|DamageArea|SoldierCollision":
		{
			msg = MessageType3[rnd.Next(MessageType3.Count)]; // Take a random message from Message Type 3
		}	break;
		case "VehicleUpsideDown":
		{
			msg = MessageType4[rnd.Next(MessageType4.Count)]; // Take a random message from Message Type 4
		}	break;
		case "Defib":
		{
			msg = MessageType5[rnd.Next(MessageType5.Count)]; // Take a random message from Message Type 5
		}	break;
		case "AA Mine":
		{
			msg = MessageType6[rnd.Next(MessageType6.Count)]; // Take a random message from Message Type 6
		}	break;
		case "BallisticShield":
		{
			msg = MessageType7[rnd.Next(MessageType7.Count)]; // Take a random message from Message Type 7
		}	break;
		case "C4":
		{
			msg = MessageType8[rnd.Next(MessageType8.Count)]; // Take a random message from Message Type 8
		}	break;
		case "Claymore":
		{
			msg = MessageType9[rnd.Next(MessageType9.Count)]; // Take a random message from Message Type 9
		}	break;
		case "Medkit":
		{
			msg = MessageType10[rnd.Next(MessageType10.Count)]; // Take a random message from Message Type 10
		}	break;
		case "SLAM":
		{
			msg = MessageType11[rnd.Next(MessageType11.Count)]; // Take a random message from Message Type 11
		}	break;
		case "EODBot|Repairtool":
		{
			msg = MessageType12[rnd.Next(MessageType12.Count)]; // Take a random message from Message Type 12
		}	break;
		case "Tomahawk":
		{
			msg = MessageType13[rnd.Next(MessageType13.Count)]; // Take a random message from Message Type 13
		}	break;
        default:
        //plugin.ConsoleWrite("Unknown weapon code: " + kill.Weapon);
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