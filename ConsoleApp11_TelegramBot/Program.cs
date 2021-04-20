using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleApp11_TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        { 

        BotWorker botWorker = new BotWorker();

            botWorker.Inizalize();
            //botClient = new TelegramBotClient(BotCredentials.BotToken);

            botWorker.Start();
            //botClient.OnMessage += Bot_OnMessage;
            //botClient.StartReceiving();
            Console.WriteLine("Напишите stop для остановки");
            Console.ReadKey();

            string command;
            do
            
            {
                command = Console.ReadLine();
            } 
            while (command != "stop");

            botWorker.Stop();
            //botClient.StopReceiving();


        }

    }
}
