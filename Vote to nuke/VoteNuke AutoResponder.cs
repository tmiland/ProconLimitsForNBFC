/* First check
Expression */
(Regex.Match(player.LastChat, @"(?:baserape|base\s*rape)", RegexOptions.IgnoreCase).Success)
/* Second check
Code: */
double tsec = 0;
if (plugin.Data.issetDouble("last response")) tsec = plugin.Data.getDouble("last response");
if ((server.TimeTotal - tsec) < (1*10.0)) return false; // 5*60 is 5 minutes in seconds, you can change this

if (limit.Activations(player.Name) > 1) return false;

List<String> responses = new List<String>();
// This is the list of messages
responses.Add("Type !votecamp to initiate a vote to NUKE the ATTACKING team!");
responses.Add("Are you being base-raped? Initiate a vote with !votecamp to NUKE the ATTACKING team!");
responses.Add("This is the last chance! Initiate a vote with !votecamp to save your ass!");
// ... you can add more messages here by adding more responses.Add lines

int rc = 0;
if (plugin.Data.issetInt("rc")) rc = plugin.Data.getInt("rc");
String msg = responses[(rc % responses.Count)];
rc = rc + 1;
plugin.Data.setInt("rc", rc);

plugin.SendGlobalMessage( msg );
plugin.SendGlobalYell(msg, 8);
plugin.PRoConChat("[ADMIN] > " + msg);
plugin.Data.setDouble("last response", server.TimeTotal);
return false;