using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace BotNetCore
{
    class Program
    {
        static bool leave = false;

        static void Main(string[] args)
        {
            Console.Title = "Twitch BOT";
            
            // Password from www.twitchapps.com/tmi/
            // include the "oauth: " portion
            Thread chatThread = new Thread(new ParameterizedThreadStart(ircThread));
            IrcBot irc = new IrcBot("irc.twitch.tv", 6667, "JoLeRobot",
                "oauth:qj4ri8j6z4n80tdrj4drq1zd4wtkqj");

            chatThread.Start(irc);
            /*
            while (!leave)
            {
                string command;
                command = Console.ReadLine();

                if (command != null)
                {
                    Console.WriteLine("command");

                    if (command.Contains("exit")) {
                        Console.WriteLine("command exit");
                        leave = !leave;
                        Console.WriteLine(leave);
                    }
                }
            }*/

            

        }

        static void ircThread(object argIrc)
        {
            IrcBot irc = (IrcBot)argIrc;
            string channel;

            Console.WriteLine("Le Bot Twitch");
            Console.WriteLine();
            Console.WriteLine("Tapez le nom du channel");

            channel = Console.ReadLine();

            irc.joinRoom(channel);

            while (!leave)
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
                    if (message.Contains("!pvp jolerobot") || message.Contains("!pvp jolamachette"))
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
            result = result.Substring(1, result.Length - 2);

            return result;
        }
    }
}
