using System;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace BotNetCore
{
    class IrcBot
    {
        private string userName;
        private string channel;

        private TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;

        public string Channel { get { return channel; } set { channel = value; } }

        public IrcBot(string ip, int port, string userName, string password)
        {
            this.userName = userName;

            tcpClient = new TcpClient();
            tcpClient.Client.Connect(ip, port);
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());

            outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + userName);
            outputStream.WriteLine("USER " + userName + " / * :" + userName);
            outputStream.Flush();

        }

        public void joinRoom()
        {
            outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }

        public void joinRoom(string channel)
        {
            this.channel = channel;
            outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }

        public void sendIrcMessage(string message)
        {
            outputStream.WriteLine(message);
            outputStream.Flush();
        }

        public void sendChatMessage(string message)
        {
            sendIrcMessage(":" + userName + "!" + userName + "@" + userName
                + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
        }

        public void sendPong()
        {
            sendIrcMessage("PONG tmi.twitch.tv\r\n");
        }

        public string readMessage()
        {
            string message = inputStream.ReadLine();
            return message;
        }

            
         
    }
}
