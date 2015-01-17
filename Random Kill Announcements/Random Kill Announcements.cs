/* Random Kill Announcements

Set limit to evaluate OnKill, set action to None

Set first_check to this Code:*/

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
MessageType1.Add(killer.Name + " just sliced " + victim.Name + "'s throat, what a shame, bro!");
MessageType1.Add(victim.Name + " was shanked by " + killer.Name + "!");
MessageType1.Add(killer.Name + " just took " + victim.Name + "'s tags and made him cry!");
MessageType1.Add(killer.Name + " slipped a shiv into " + victim.Name + "'s back!");
MessageType1.Add(killer.Name + " knifed " + victim.Name + " and Insane Limits approves!");
MessageType1.Add(victim.Name + ", you gonna let " + killer.Name + " get away with taking your tags?");
MessageType1.Add(killer.Name + " just Tweeted about knifing " + victim.Name + "!");
MessageType1.Add(killer.Name + " just posted " + victim.Name + "'s tags on Facebook!");
MessageType1.Add(killer.Name + ": 'Just die already, " + victim.Name + "'"); // From BC2
MessageType1.Add(killer.Name + ": 'Hey " + victim.Name + ", you want summa this?'"); // From BC2
MessageType1.Add(victim.Name + " took a gun to a knife fight with " + killer.Name + ", and LOST!");
MessageType1.Add("Did you see the YouTube video of " + killer.Name + " knifing " + victim.Name + "?");
MessageType1.Add(killer.Name + " just added +1 knife kills to his Battlelog stats, thanks to " + victim.Name);
MessageType1.Add(killer.Name + ": 'Hey " + victim.Name + ", check your six next time!'");
MessageType1.Add(victim.Name + ", go tell your momma you lost your tags to " + killer.Name + "!");
MessageType1.Add(victim.Name + " just wanted to see " + killer.Name + "'s Premium knife, not have it shoved in his eye!");

// Add all your messages which should be shown on "RoadKill"
MessageType2.Add("WROOM! " + killer.Name + " just roadkilled " + victim.Name + "!");
MessageType2.Add("WROOM! " + killer.Name + " just ran over " + victim.Name + "!");

// Add all your messages which should be shown on "Suicide"
MessageType3.Add("CALL THE MORGUE! " + victim.Name + " just committed Suicide!");

// Add all your messages which should be shown on "VehicleUpsideDown"
MessageType4.Add("UH OH! " + victim.Name + " just flipped his vehicle upside down and died!");

// Add all your messages which should be shown on "Defib"
MessageType5.Add("WOAH! " + killer.Name + " just ZAPPED " + victim.Name + " to death with his " + kill.Weapon + "!");
MessageType5.Add("ZAP! " + killer.Name + " just ELECTROCUTED " + victim.Name + " with his " + kill.Weapon + "!");

// Add all your messages which should be shown on "AA Mine"
MessageType6.Add(victim.Name + " just flew over " + killer.Name + "'s " + kill.Weapon + "!");

// Add all your messages which should be shown on "BallisticShield"
MessageType7.Add("SLAM! " + killer.Name + " has killed " + victim.Name + " with his " + kill.Weapon + "!");

// Add all your messages which should be shown on "C4"
MessageType8.Add("BOOM! " + killer.Name + " just BLEW UP " + victim.Name + " with " + kill.Weapon + "!");

// Add all your messages which should be shown on "Claymore"
MessageType9.Add("WOHAA! " + victim.Name + " just stepped into " + killer.Name + "'s " + kill.Weapon + "!");

// Add all your messages which should be shown on "Medkit"
MessageType10.Add("MEDKILL! " + killer.Name + " just dropped a " + kill.Weapon + " on " + victim.Name + "!");
MessageType10.Add("MEDKILL! " + killer.Name + " just KILLED " + victim.Name + " with his " + kill.Weapon + "!");

// Add all your messages which should be shown on "SLAM"
MessageType11.Add("BAM! " + killer.Name + " just killed " + victim.Name + " with a " + kill.Weapon + "!");
MessageType11.Add("SLAM! " + victim.Name + " just ran in to " + killer.Name + "'s " + kill.Weapon + "'s!");

// Add all your messages which should be shown on "Repairtool"
MessageType12.Add(killer.Name + " just BURNED " + victim.Name + "'s ass with a " + kill.Weapon + "!");
MessageType12.Add(killer.Name + " just TORCHED " + victim.Name + " with " + kill.Weapon + "!");
MessageType12.Add(victim.Name + " just got TORCHED with " + kill.Weapon + " by " + killer.Name + "!");

// Add all your messages which should be shown on "Tomahawk"
MessageType13.Add("BOMBS AWAY! " + killer.Name + " just dropped a " + kill.Weapon + " on " + victim.Name + "!");

	// Do the switch
	switch (kill.Weapon)
	{
		case "Melee":
		{
			msg = MessageType1[rnd.Next(MessageType1.Count)]; // Take a random message from Message Type 1
		}	break;
		case "RoadKill":
		{
			msg = MessageType2[rnd.Next(MessageType2.Count)]; // Take a random message from Message Type 2
		}	break;
		case "Suicide":
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
		case "Repairtool":
		{
			msg = MessageType12[rnd.Next(MessageType12.Count)]; // Take a random message from Message Type 12
		}	break;
		case "Tomahawk":
		{
			msg = MessageType13[rnd.Next(MessageType13.Count)]; // Take a random message from Message Type 13
		}	break;
        default:
        return false;
	}

	if (!player.Data.issetBool("NoYell"))
	{
		player.Data.setBool("NoYell", true);
	}

	if (player.Data.getBool("NoYell"))
	{
		// Send msg to chat if @noyell is off
		plugin.SendGlobalMessage(msg);
	}
	plugin.SendGlobalYell(msg, 5);
	// For writing to console
	plugin.PRoConChat("Kill Announcements > " + msg);
	plugin.ConsoleWrite("^b^1Kill Announcements >^0^n " + msg);
	plugin.PRoConEvent(msg, "Insane Limits");

	return false;