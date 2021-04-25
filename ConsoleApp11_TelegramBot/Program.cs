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
            
            botWorker.Start();
                        
            Console.ReadKey();

            string command;
            do
            {
                command = Console.ReadLine();
            } 
            while (command != "stop");

            botWorker.Stop();
        
        }

    }
}
