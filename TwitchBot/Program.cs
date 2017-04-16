using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TwitchBot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Password from www.twitchapps.com/tmi/
            // include the "oauth: " portion
            IrcBot irc = new IrcBot("irc.twitch.tv", 6667, "JoLeRobot",
                "oauth:qj4ri8j6z4n80tdrj4drq1zd4wtkqj");
            irc.joinRoom("zenix7410");
            while (true)
            {

                string message;
                message = irc.readMessage();

                if (message != null)
                {
                    Console.WriteLine(message);

                    message = message.ToLower();
                    if (message.Contains("!hello"))
                    {
                        irc.sendChatMessage("Yo yo");
                    }
                    if (message.Contains("!pvp jolerobot")|| message.Contains("!pvp jolamachette"))
                    {
                        string pseudo = getPseudo(message);
                        
                        irc.sendChatMessage("!pvp " + pseudo);
                        
                    }
                    if (message.Contains("ping"))
                    {
                        irc.sendPong();
                    }
                }
            }
        }



        static string getPseudo(string message)
        {
            string pattern = @"@\w*.";
            string result;
            MatchCollection mc = Regex.Matches(message, pattern);
            result = mc[0].ToString();
            result = result.Substring(1, result.Length-2);

            return result;
        }
    }
}
