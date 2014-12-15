/* Create a new limit to evaluate OnDeath, call it "Tips on Killcam".

Set first_check to this Code: */
if (!player.Data.issetBool("NoYell"))
	player.Data.setBool("NoYell", true);

if (player.Data.getBool("NoYell")) {
List<String> tips = new List<String>();
	tips.Add("Hitting a Tank straight on with a flat 90 degree angle rocket will do more damage than a regular hit to the side!");
	tips.Add("Hold down <key> to charge the defibrillator, you will earn more points and revive allies with more health!");
	tips.Add("When suppressed it becomes harder to aim as firing your weapon is less accurate!");
	tips.Add("Mortars are great for taking out people who are sitting still, use them to takeout gunners and snipers!");
	tips.Add("The PLD and SOFLAM allow your entire team to preform a powerful Top-Down Lock-on attack with launchers!");
	tips.Add("Flags on the battlefield provide your team with powerful war assets like Tanks and Cruise Missiles, Capture them!");
	tips.Add("Be careful with playing solo. You’ll usually do better if you stay close to your team or squad mates!");
	tips.Add("Suppressing your weapon will prevent you from appearing on the map when you shoot, but reduces your hip-fire accuracy!");
	tips.Add("Laser sights will shine through the smoke and reveal where you are if you point it at someone, Turn it off with <Key>!");
	tips.Add("After firing the SRAW, quickly let go of ADS to prevent it from aiming off target if you happen to die!");
 	tips.Add("Are you the last member of your squad who’s still alive? Make sure you’re a safe spawn point for your squad!");
	tips.Add("Be careful with playing solo. You’ll usually do better if you stay close to your team or squad mates!");
	tips.Add("Extra points are another benefit of sticking with your squad. You’ll get extra points for healing, reviving, and supplying your squad mates!");
	tips.Add("Try spotting your target before shooting, if you can! In case you miss.");
	tips.Add("Are you getting shot at? Zigzagging can save your life!");
	tips.Add("Equip your soldier with smoke grenades! A smokescreen might be just the thing you need to capture a flag, and still live to tell the tale.");
	tips.Add("Be careful when approaching a flag. Expect that one or more opponents are waiting for you.");
	tips.Add("If you’re driving a car or a motorcycle en route to the target, ditch your ride a while before you get there so that the engine sound doesn’t give you away.");
	tips.Add("Don’t pick the obvious routes to your target. Use a detour, just to surprise the welcome committee.");
	tips.Add("Don’t run straight to the flag immediately. Try getting an overview of the area surrounding the target first. Take out any bad guys before moving towards the target.");
	tips.Add("No matter what the cover is, try playing so that you’ve always got cover between you and your opponents. Plan your movements.");
	tips.Add("Planning to move over a bigger part of a map? Pick a route that gives you as much cover as possible between you and the hostiles.");
	tips.Add("Avoid open areas. Letting your character run around in the middle of street or in the wide open, you’ll quickly become the opposing team’s favorite.");
	tips.Add("Make sure you get to safety before reloading, A reload in the open is like placing a sign on your character that says “Attention! Easy Target Here – Reload in Progress!”");
	tips.Add("Make sure the character you just took out doesn’t have any friends lurking about, before you reload.");
	tips.Add("When you walk more than you run, you’ll pick up on more of what’s going on around your character.");
	tips.Add("The time it takes from you stopping moving your character until it can start firing its weapon is a little bit shorter when you’re walking instead of running.");
	tips.Add("The sound of footsteps, incoming grenades, engines, shots, and so on, are often easy to hear using headphones with surround sound.");
	tips.Add("A lot of times, the smartest thing you can do is hide. As such, you should know as many good hideouts as possible. These can be anything from bushes to buildings.");
	tips.Add("Many recon players are predictable. They quickly identify their favorite positions. And they use them. A lot.");
/* 	tips.Add("");
	tips.Add("");
	tips.Add("");
	tips.Add("");
	tips.Add("");
	tips.Add(""); */
	// Add more here

	// Pick the next tip
	String key = "index for tips";
	int i = 0;
	if (!plugin.Data.issetInt(key)) {
		Random r = new Random();
		i = r.Next(tips.Count);
	} else {
		i = plugin.Data.getInt(key);
	}
	i = (i + 1) % tips.Count;
	plugin.Data.setInt(key, i);

	// Set it
	//plugin.SendGlobalMessage("TIP: " + tips[i]);
	//plugin.ServerCommand("vars.serverMessage", tips[i]);
	plugin.SendPlayerYell(player.Name, plugin.R ("\nTIP: "+ tips[i]), 15);

}
else {
	return false;
}

return false;