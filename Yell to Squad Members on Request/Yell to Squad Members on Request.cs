/* Yell to Squad Members on Request

Set limit to evaluate OnAnyChat, set action to None

Set first_check to this Code:*/

if (!player.LastChat.StartsWith("ID_CHAT_"))
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

// Add all your messages which should be shown on "ID_CHAT_ATTACK/DEFEND" request
MessageType1.Add(" Gave you an ORDER Soldier! PTFO!");
MessageType1.Add(" PTFO To win the round Soldier!");
MessageType1.Add(" Has marked the OBJECTIVE! PTFO!");
MessageType1.Add(" Attack/Defend the marked OBJECTIVE Soldier");

// Add all your messages which should be shown on "ID_CHAT_THANKS" request
MessageType2.Add(": Thank You!");
MessageType2.Add(": Thanks Man!");
MessageType2.Add(": Hey, Thanks A Lot!");
MessageType2.Add(": Thank You Soldier!");
MessageType2.Add(": Thanks Man, I Owe You One!");
MessageType2.Add(": Thanks Soldier!");
MessageType2.Add(": Alright, Thanks A lot Man!");
MessageType2.Add(": Thank You Man!");
MessageType2.Add(": That\'s Great, Thanks!");
MessageType2.Add(": Hey, Thanks A Lot!");
MessageType2.Add(": Thanks Dude!");
MessageType2.Add(": Sweet, Thanks Man!");
MessageType2.Add(": Thanks, I Appreciate It!");
MessageType2.Add(": Thanks A Lot!");
MessageType2.Add(": Alright, Thanks A lot!");
MessageType2.Add(": Thank You Bro!");
MessageType2.Add(": Hey, I Really Appreciate It!");
MessageType2.Add(": Damn, Thanks A Lot!");
MessageType2.Add(": Hey, I Owe You One!");
MessageType2.Add(": Thanks Man, Alright!");

// Add all your messages which should be shown on "ID_CHAT_SORRY" request
MessageType3.Add(": My Bad!");
MessageType3.Add(": Oh Fuck, My Bad Man!");
MessageType3.Add(": Sorry Man, I Fucked Up!");
MessageType3.Add(": Uh, My Mistake!");
MessageType3.Add(": Oh Fuck, Sorry About That!");
MessageType3.Add(": I Didn\'t Mean To Fuck That One Up!");
MessageType3.Add(": Sorry Man, My Bad!");
MessageType3.Add(": Aw, Fuck Me, That Was My Fault!");
MessageType3.Add(": Fuck Man, I Am Sorry!");
MessageType3.Add(": Aw Shit, Sorry!");
MessageType3.Add(": Shit, That Was My Bad!");
MessageType3.Add(": That Was My Fault!");
MessageType3.Add(": Yeah I Fucked That One Up, Sorry Man!");
MessageType3.Add(": Sorry My Bad, I Fucked Up!");
MessageType3.Add(": My Bad!");
MessageType3.Add(": That Was My Fault, Sorry!");
MessageType3.Add(": Sorry, Man That Was My Fault!");
MessageType3.Add(": That Was My Fault Man, Sorry!");
MessageType3.Add(": Thats On Me, I Am Sorry Man!");
MessageType3.Add(": Aw Fuck, I Am Sorry Man!");
MessageType3.Add(": Hey, I Am Sorry!");
MessageType3.Add(": My Fault!");
MessageType3.Add(": Sorry Bro!");
MessageType3.Add(": Wow, Sorry Dude!");
MessageType3.Add(": Sorry Man, I Fucked Up!");
MessageType3.Add(": Hey! Sorry About That!");
MessageType3.Add(": Jeez, Sorry About That!");
MessageType3.Add(": Aw Fuck, Sorry, My Fault!");
MessageType3.Add(": Sorry Dude!");
MessageType3.Add(": Sorry About That, Won\'t Happen Again!");
MessageType3.Add(": Sorry!");
MessageType3.Add(": I am so Sorry!");
MessageType3.Add(": Sorry About That!");
MessageType3.Add(": Oh Crap!");

// Add all your messages which should be shown on "ID_CHAT_GOGOGO" request
MessageType4.Add(": Hey Grab Your Shit, Let\'s Go!");
MessageType4.Add(": Playtime Is Over, Get Out!");
MessageType4.Add(": Grab Your Shit, We\'re Steppin!");
MessageType4.Add(": We\'re Steppin!");
MessageType4.Add(": GO!");
MessageType4.Add(": Go Now!");
MessageType4.Add(": Lets Go! GO!!");
MessageType4.Add(": Come On, Lets Go");
MessageType4.Add(": Let\'s Go!");
MessageType4.Add(": Let\'s Move! Move! Move!");
MessageType4.Add(": Come On, GO!!");
MessageType4.Add(": Go, NOW!!");
MessageType4.Add(": We are Steppin!");
MessageType4.Add(": Lets Move, GO!!");
MessageType4.Add(": Go, Lets Move Out!");
MessageType4.Add(": Mooooooove!!!");
MessageType4.Add(": Knees High, Step It UP, Go! Go!");
MessageType4.Add(": Move Your Asses, Lets Go!");
MessageType4.Add(": Alright, Lets Get Fucking Moving!");
MessageType4.Add(": Move Out, You Motherfukkas!");
MessageType4.Add(": It\'s Time To Haul Ass!");
MessageType4.Add(": Alright, We gotta Move Now!");
MessageType4.Add(": We Are Moving Out!");
MessageType4.Add(": Go! Go! Go!");
MessageType4.Add(": Let\'s get out of here!");
MessageType4.Add(": Let\'s get down to business!");

// Add all your messages which should be shown on "ID_CHAT_REQUEST_ORDER" request
MessageType5.Add(": What\'s the objective Squad Leader?");
MessageType5.Add(": Objective Squad Leader?");
MessageType5.Add(": What\'s the ORDERS?");

// Add all your messages which should be shown on "ID_CHAT_REQUEST_MEDIC" request
MessageType6.Add(" requested a MEDIC! Throw out a bag!");
MessageType6.Add(" Is DYING! Give him some MEDICINE");
MessageType6.Add(": I Need a MEDIC!");
MessageType6.Add(": Where Is The Got Damn MEDIC?");
MessageType6.Add(": Get Me A Medic Over Here!");
MessageType6.Add(": Get Me A Medic!!");
MessageType6.Add(": I Need A Fucking Medic NOW!");
MessageType6.Add(": Get Me A Fucking Medic!");
MessageType6.Add(": MEDIIIIIC!!");
MessageType6.Add(": Aw, Hell, I Need A Medic Over Here!");
MessageType6.Add(": I Am Dying Out Here, I Need A Medic!");
MessageType6.Add(": Oh Jesus, I Need A Medic!");
MessageType6.Add(": Oh God, I Need A Medic!");
MessageType6.Add(": I Need A Medic!");
MessageType6.Add(": Bleeding All Over The Place, MEEDIC!");
MessageType6.Add(": Give Me A Fucking Medic!");
MessageType6.Add(": I Need A Medic Here, NOW!");
MessageType6.Add(": I NEED A MEDIC!!");

// Add all your messages which should be shown on "ID_CHAT_REQUEST_AMMO" request
MessageType7.Add(" Anybody Got Any ROUNDS?");
MessageType7.Add(" I Need Some Bullets Over Here!");
MessageType7.Add(" AMMO!! AMMO!!");
MessageType7.Add(" Somebody Hook Me Up With Some AMMO!");
MessageType7.Add(" Hey, Give Me Some AMMO!");
MessageType7.Add(" Toss Me Over Some ROUNDS!");
MessageType7.Add(" Hook Me Up With Some ROUNDS!");
MessageType7.Add(" I Need Some AMMO!");
MessageType7.Add(" I Need Some AMMO Over Here!");
MessageType7.Add(" I Need Some AMMO Over Here, Someone Help Me Out!");
MessageType7.Add(" requested AMMO! Throw out a bag!");
MessageType7.Add(" Is out of AMMO! Give it to him!");

// Add all your messages which should be shown on "ID_CHAT_REQUEST_RIDE" request
MessageType8.Add(" requested a RIDE! Go pick him up!");
MessageType8.Add(" needs a RIDE, be a teammate and pick him up!");
MessageType8.Add(": I Need A Ride!");
MessageType8.Add(": Hey, Come Over Here And Pick Me Up!");
MessageType8.Add(": Hey, I Need A Ride Here!");
MessageType8.Add(": Hey, Someone Come Get Me!");
MessageType8.Add(": I Need A Ride!");
MessageType8.Add(": Someone Come Pick Me Up!");
MessageType8.Add(": I Really Need A Ride Over Here!");
MessageType8.Add(": Someone Come Get Me, I Need A Ride!");
MessageType8.Add(": Someone Come Get Me!");
MessageType8.Add(": Someone Send Me A Fucking Vehicle!");
MessageType8.Add(": Hey, Come And Get Me!");
MessageType8.Add(": Come Get My Ass Outta Here!");
MessageType8.Add(": I Need A Fucking Lift!");
MessageType8.Add(": Hey, I Need A Ride!");
MessageType8.Add(": Need A Pickup Over Here!");
MessageType8.Add(": I Need A Lift Here!");
MessageType8.Add(": Come Get My Ass!");
MessageType8.Add(": Come And Get Me, I Need To Get The Fuck Outta Here!");
MessageType8.Add(": Can i get a RIDE?");

// Add all your messages which should be shown on "ID_CHAT_GET_OUT" request
MessageType9.Add(": Hop Out, Lets Move!");
MessageType9.Add(": Lets Move People!");
MessageType9.Add(": Lets Move Out Guys, Go! Go!");
MessageType9.Add(": Alright, Lets Bail!");
MessageType9.Add(": Alright, Lets Move On Out!");
MessageType9.Add(": Alright, Out Of The Vehicle!");
MessageType9.Add(": Alright Boys, Lets Move!");
MessageType9.Add(": Get Out Of The Vehicle Now!");
MessageType9.Add(": Alright, Lets Get Outta Here!");
MessageType9.Add(": We are Steppin, Move! Move!");
MessageType9.Add(": Move! Move! Move!");
MessageType9.Add(": Bail Out, Lets Move!");
MessageType9.Add(": Get Out, We are Moving!");
MessageType9.Add(" requested a SPOT! Get out of the vehicle!");
MessageType9.Add(" Is probably playing with a teammate, GTFO!");

// Add all your messages which should be shown on "ID_CHAT_GET_IN" request
MessageType10.Add(": Hey, Hop In!");
MessageType10.Add(": Come On, Get In!");
MessageType10.Add(": Get In, Lets Go!");
MessageType10.Add(": Steppin out, Come On, Lets Go!");
MessageType10.Add(": Alright, We Are Moving Out, Get In!");
MessageType10.Add(": Get In, Lets Move Out!");
MessageType10.Add(": Get You Ass In, Lets Roll!");
MessageType10.Add(": Get In Here Soldier, We Gotta Go!");
MessageType10.Add(": Come On, Lets Go!");
MessageType10.Add(": We Aint Got Time To Fuck Around, Lets Go!");
MessageType10.Add(": Come On We Gotta Move!");
MessageType10.Add(": Get Your Ass In Here, We Gotta Go!");
MessageType10.Add(": Get In, We Gotta Go!");
MessageType10.Add(": Alright, Get The Fuck In Here, We Gotta Move!");
MessageType10.Add(": Alright, Hop In, Lets Get Outta Here!");
MessageType10.Add(": Hey Man, Get In, Lets Go!");
MessageType10.Add(" has a free SPOT! Get in the vehicle!");
MessageType10.Add(" said: Do you need a lift?");

// Add all your messages which should be shown on "ID_CHAT_REQUEST_REPAIRS" request
MessageType11.Add(" This Shit Aint Running No More, I Need To Get It Fixed!");
MessageType11.Add(" I Need A Mechanic!");
MessageType11.Add(" Someone Get Over Here And Get This Shit Running!");
MessageType11.Add(" I Need Somebody To Fix My Vehicle!");
MessageType11.Add(" My Vehicle Is Fucked, I Need Some Help!");
MessageType11.Add(" Come On, I Need A Mechanic Over Here!");
MessageType11.Add(" Oh Fuck, My Ride Is Out, I Need Somebody To Fix It!");
MessageType11.Add(" Oh, I Fucked Up My Ride, I Need Some Repairs!");
MessageType11.Add(" This Ride Needs Repairs!");
MessageType11.Add(" This Peace Of Shit Cant Move, I Need A Got Damn Mechanic!");
MessageType11.Add(" Hey, Come Fix Up My Ride!");
MessageType11.Add(" In Desperate Need Of A Mechanic Over Here!");
MessageType11.Add(" My Vehicle Is Fucked Up, Can Anybody Help Me?");
MessageType11.Add(" I Need Mechanical Assistance!");
MessageType11.Add(" Fuck, I Need A Mechanic here!");
MessageType11.Add(" I Need Some Help To Fix This Shit!");
MessageType11.Add(" I Need A GreaseMonkey To Take A Look At This Shit!");
MessageType11.Add(" requested REPAIRS! Now! Go! Go! Go!");
MessageType11.Add(" Has a damaged vehicle, go REPAIR your teammate!");

// Add all your messages which should be shown on "ID_CHAT_AFFIRMATIVE" request
MessageType12.Add(": Affirmative!");
MessageType12.Add(": Copy That!");
MessageType12.Add(": Okay Chief!");
MessageType12.Add(": Roger That!");
MessageType12.Add(": Sure Thing!");
MessageType12.Add(": No Problem!");
MessageType12.Add(": Yeah, You betcha!");
MessageType12.Add(": Yes!");
MessageType12.Add(": Consider It Done!");
MessageType12.Add(": Yeah, No Sweat!");
MessageType12.Add(": Alright, No Problem!");
MessageType12.Add(": Ill Get Right On That!");
MessageType12.Add(": Thats Affirmative!");

// Add all your messages which should be shown on "ID_CHAT_NEGATIVE" request
MessageType13.Add(" NO! I cannot do that!");
MessageType13.Add(" Maybe later!");
MessageType13.Add(" I\'ll Get Back To It!");
MessageType13.Add(" No can do!");
MessageType13.Add(" Thats a NEGATIVE!");
MessageType13.Add(" NO!");
MessageType13.Add(" Uh, thats not possible at this time!");
MessageType13.Add(" No way man!");
MessageType13.Add(" I am all tied up, i cannot do that right now!");
MessageType13.Add(" Sorry, No!");
MessageType13.Add(" No, Sorry!");
MessageType13.Add(" Negative! Cannot do that right now!");
MessageType13.Add(" Yeah, i dont think so!");
/* 

All other messages should also be defined here

*/
	
	switch (player.LastChat)
	{
		case "ID_CHAT_ATTACK/DEFEND":
		{
			msg = player.Name + MessageType1[rnd.Next(MessageType1.Count)]; // Take a random message from Message Type 1
		}	break;
		case "ID_CHAT_THANKS":
		{
			msg = player.Name + MessageType2[rnd.Next(MessageType2.Count)]; // Take a random message from Message Type 2
		}	break;
		case "ID_CHAT_SORRY":
		{
			msg = player.Name + MessageType3[rnd.Next(MessageType3.Count)]; // Take a random message from Message Type 3
		}	break;
		case "ID_CHAT_GOGOGO":
		{
			msg = player.Name + MessageType4[rnd.Next(MessageType4.Count)]; // Take a random message from Message Type 4
		}	break;
		case "ID_CHAT_REQUEST_ORDER":
		{
			msg = player.Name + MessageType5[rnd.Next(MessageType5.Count)]; // Take a random message from Message Type 5
		}	break;
		case "ID_CHAT_REQUEST_MEDIC":
		{
			msg = player.Name + MessageType6[rnd.Next(MessageType6.Count)]; // Take a random message from Message Type 6
		}	break;
		case "ID_CHAT_REQUEST_AMMO":
		{
			msg = player.Name + MessageType7[rnd.Next(MessageType7.Count)]; // Take a random message from Message Type 7
		}	break;
		case "ID_CHAT_REQUEST_RIDE":
		{
			msg = player.Name + MessageType8[rnd.Next(MessageType8.Count)]; // Take a random message from Message Type 8
		}	break;
		case "ID_CHAT_GET_OUT":
		{
			msg = player.Name + MessageType9[rnd.Next(MessageType9.Count)]; // Take a random message from Message Type 9
		}	break;
		case "ID_CHAT_GET_IN":
		{
			msg = player.Name + MessageType10[rnd.Next(MessageType10.Count)]; // Take a random message from Message Type 10
		}	break;
		case "ID_CHAT_REQUEST_REPAIRS":
		{
			msg = player.Name + MessageType11[rnd.Next(MessageType11.Count)]; // Take a random message from Message Type 11
		}	break;
		case "ID_CHAT_AFFIRMATIVE":
		{
			msg = player.Name + MessageType12[rnd.Next(MessageType12.Count)]; // Take a random message from Message Type 12
		}	break;
		case "ID_CHAT_NEGATIVE":
		{
			msg = player.Name + MessageType13[rnd.Next(MessageType13.Count)]; // Take a random message from Message Type 13
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