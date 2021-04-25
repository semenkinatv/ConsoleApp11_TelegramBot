using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleApp11_TelegramBot
{
    public class BotWorker
    {
        private ITelegramBotClient botClient;
        private BotMessageLogic logic;

        public void Inizalize()
        {
            botClient = new TelegramBotClient(BotCredentials.BotToken);

            logic = new BotMessageLogic(botClient);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine("Привет! Я обучающий робот. Меня зовут {0}. Список возможных команд:", me.FirstName);
            Console.WriteLine("Для добавления слова в словарь введите:  /addword");
            Console.WriteLine("Для удаления слова из словаря введите:   /deleteword");
            Console.WriteLine("Получить список слов словаря введите:    /dictionary");
            Console.WriteLine("Для планирования тренировки введите:     /trainplan");
            Console.WriteLine("Для старта запланированной тренировки:   /start");
            Console.WriteLine("Для завершения тренировки:   /stop");
            Console.WriteLine("Жду Вашу команду...");
        }

        public void Start()
        {
            botClient.OnMessage += Bot_OnMessage;
         
            botClient.StartReceiving();
        }

        public void Stop()
        {
            botClient.StopReceiving();
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                //Console.WriteLine($"Получено сообщение в чате: {e.Message.Chat.Id}.");

                //await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "Вы написали:\n" + e.Message.Text);

                await logic.Response(e);
            }
        }
    }
}
