/* Create a limit to evaluate OnSpawn, call it "Yell Vote Reminder", leave Action set to None.

Set first_check to this Expression: */
(Math.Abs(team1.RemainTickets - team2.RemainTickets) > 300)
/* You can change 300 to whatever you want the maximum gap to be.

Set second_check to this code: */
if (limit.Activations() > 1) return false; // Only send yell once

String msg = "Type !votecamp to initiate a vote to NUKE the ATTACKING team!"; // CHANGE
plugin.SendGlobalYell(msg, 10);
plugin.SendGlobalMessage(msg);
plugin.PRoConChat("ADMIN > " + msg);
return false;
/* This will only yell the message once per round, even if the gap closes (gets smaller) and then goes over the max again. You can change the text of the message by changing the line that ends with // CHANGE. The 10 is the number of seconds the yell is displayed, you can change that too if you want. */