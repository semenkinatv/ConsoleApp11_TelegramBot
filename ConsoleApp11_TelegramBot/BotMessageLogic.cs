using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleApp11_TelegramBot
{
    public class BotMessageLogic
    {//управлять логикой обработки полученных сообщений 
        private Messenger messanger;
        private Dictionary<long, Conversation> chatList;
        private ITelegramBotClient botClient;

        List<Word> wordList = new List<Word>();
       
        public BotMessageLogic(ITelegramBotClient botClient)
        {
            messanger = new Messenger();
            chatList = new Dictionary<long, Conversation>();
            this.botClient = botClient;
        }
        //функционал хранения чатов и отправки ответа в чат в зависимости от того, какой это чат        
        public async Task Response(MessageEventArgs e)
        {
            
            var Id = e.Message.Chat.Id;

                if (!chatList.ContainsKey(Id))
                {
                    var newchat = new Conversation(e.Message.Chat);

                    chatList.Add(Id, newchat);
                }

            //добавляем новое сообщение в историю сообщений конкретного чата
            var chat = chatList[Id];
            chat.AddMessage(e.Message);
                       
            //отправляем ответ пользователю
            await SendTextMessage(chat);

        }
        public async Task SendTextMessage(Conversation chat)
        {
            var mes = chat.GetLastMessage();
            string text;
            
            if (GlobalVar.fProcAddWord == false && mes == "/addword" )
            {
                GlobalVar.fProcAddWord = true;
                text = "Введите русское значение слова";
            }
            else if (GlobalVar.fProcAddWord == false && mes == "/deleteword")
            {
                GlobalVar.fProcDelWord = true;
                text = "Введите русское значение слова, которое хотите удалить из словаря.";
            }
            else if (GlobalVar.fProcDelWord == true)
            {
                var delWord = mes;
                var numList = -1;
                int delItem = -1;
                foreach (var item in wordList)
                {
                    numList++;
                    if (item.WordRus == delWord)
                    {
                        delItem = numList;
                    }
                }
                if (delItem > 0)
                {
                    wordList.RemoveAt(delItem);
                    text = $"Успешно! Слово {delWord} удалено из словаря.";
                }
                else
                { 
                    text = $"Слово { delWord} не найдено в словаре.";
                }
                
                GlobalVar.fProcDelWord = false;

            }
            else if (GlobalVar.fProcAddWord == true && GlobalVar.WordRus == "")
            {
               GlobalVar.WordRus = mes;
               text = "Введите английское значение слова";
               
            }
            else if (GlobalVar.fProcAddWord == true && GlobalVar.WordRus != "" && GlobalVar.WordEng == "")
            {
                GlobalVar.WordEng = mes;
                text = "Введите тематику";
            }
            else if (GlobalVar.fProcAddWord = true && GlobalVar.WordRus != "" && GlobalVar.WordEng != "" && GlobalVar.WordSubj == "")
            {
                GlobalVar.WordSubj = mes;

                Word word = new Word(GlobalVar.WordRus, GlobalVar.WordEng, GlobalVar.WordSubj);
                wordList.Add(word);

                text = $"Успешно! Слово {word.WordRus} {word.WordEng} {word.WordSubj} добавлено в словарь.";
                GlobalVar.fProcAddWord = false;
                GlobalVar.WordRus = "";
                GlobalVar.WordEng = "";
                GlobalVar.WordSubj = "";
            }
            else
            {
                text = messanger.CreateTextMessage(chat);
            }

            await botClient.SendTextMessageAsync(
            chatId: chat.GetId(), text: text);
        }
    }
}
