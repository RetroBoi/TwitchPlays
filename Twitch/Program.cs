using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using WindowsInput;

namespace Twitch
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
        
            Bot bot = new Bot();
            Console.ReadLine();
        }
    }

    class Bot
    {
        

        TwitchClient client;

        public Bot()
        {
            string bot_pass = System.IO.File.ReadAllText("read from text");
            ConnectionCredentials credentials = new ConnectionCredentials("bot name here", bot_pass);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, "Your Twitch Chat");

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            client.SendMessage(e.Channel, "Connected to Chat!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {

            if (e.ChatMessage.Message.Equals("B"))
            {
                var sim = new InputSimulator();
                sim.Keyboard
                    .KeyDown(WindowsInput.Native.VirtualKeyCode.VK_Z)
                    .Sleep(250)
                    .KeyUp(WindowsInput.Native.VirtualKeyCode.VK_Z);
                Console.WriteLine(e.ChatMessage.Username + " Pressed the B Button");
            }
            else if (e.ChatMessage.Message.Equals("LEFT"))
            {
                var sim = new InputSimulator();
                sim.Keyboard
                    .KeyDown(WindowsInput.Native.VirtualKeyCode.LEFT)
                    .Sleep(250)
                    .KeyUp(WindowsInput.Native.VirtualKeyCode.LEFT);
                Console.WriteLine(e.ChatMessage.Username + " Pressed Left");
            }
            else if (e.ChatMessage.Message.Equals("RIGHT"))
            {
                var sim = new InputSimulator();
                sim.Keyboard
                    .KeyDown(WindowsInput.Native.VirtualKeyCode.RIGHT)
                    .Sleep(250)
                    .KeyUp(WindowsInput.Native.VirtualKeyCode.RIGHT);
                Console.WriteLine(e.ChatMessage.Username + " Pressed Right");
            }
            else if (e.ChatMessage.Message.Equals("DOWN"))
            {
                var sim = new InputSimulator();
                sim.Keyboard
                    .KeyDown(WindowsInput.Native.VirtualKeyCode.DOWN)
                    .Sleep(250)
                    .KeyUp(WindowsInput.Native.VirtualKeyCode.DOWN);
                Console.WriteLine(e.ChatMessage.Username + " Pressed Down");
            }
            else if (e.ChatMessage.Message.Equals("A"))
            {
                var sim = new InputSimulator();
                sim.Keyboard
                    .KeyDown(WindowsInput.Native.VirtualKeyCode.VK_X)
                    .Sleep(250)
                    .KeyUp(WindowsInput.Native.VirtualKeyCode.VK_X);
                Console.WriteLine(e.ChatMessage.Username + " Pressed the A Button");
            }
        }

        
    }
}