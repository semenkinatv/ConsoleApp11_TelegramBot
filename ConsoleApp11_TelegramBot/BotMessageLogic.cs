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

        //private Dictionary<long, List<Word>> chatWordList;
       // private Dictionary<long, Word> chatWordList;

        private List<Word> wordList ;
        private Train train;

        public BotMessageLogic(ITelegramBotClient botClient)
        {
            messanger = new Messenger();
            chatList = new Dictionary<long, Conversation>();
            this.botClient = botClient;
            wordList = new List<Word>();
            // chatWordList = new Dictionary<long, List<Word>>();

            train = new Train();
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
            await SendTextMessage(chat );
        }

        public async Task SendTextMessage(Conversation chat)
        {
            string text = "";
            var mes = chat.GetLastMessage();
            var meslast = chat.GetLastLastMessage();

            if (GlobalVar.fProcAddWord == false & train.TrainPlan==false & train.TrainStart == false  & mes == "/addword")
            {
                GlobalVar.fProcAddWord = true;
                text = "Введите русское значение слова";
            }

            else if (GlobalVar.fProcAddWord == true & (mes == "/addword" | mes == "/deleteword" | mes == "/trainplan" | mes == "/start"))
            {
                text = "Сперва закончите предыдущий ввод слова.";
            }

            else if (train.TrainPlan == true & (mes == "/addword" | mes == "/deleteword" | mes == "/trainplan" | mes == "/start"))
            {
                text = "Сперва закончите планирование тренировки.";
            }

            else if (GlobalVar.fProcAddWord == false & train.TrainPlan == false & train.TrainStart == false & mes == "/deleteword")
            {
                GlobalVar.fProcDelWord = true;
                text = "Введите русское значение слова, которое хотите удалить из словаря.";
            }
            else if (GlobalVar.fProcAddWord == false & train.TrainPlan == false & train.TrainStart == false & mes == "/listwords")
            {
                foreach (var item in wordList)
                {
                    text += item.WordRus + "; ";
                }

                if (text == "")
                {
                    text = "Ваш словарь пока пуст";
                }
                else
                {
                    text = "Перечень слов в словаре (рус.значение): " + text;
                }

            }
            else if (GlobalVar.fProcAddWord == false & train.TrainPlan == false & train.TrainStart == false & mes == "/trainplan")
            {
                text = "Запланируем параметры тренировки:\r\nДля перевода c русского на английский введите /RusEng, иначе /EngRus";
                train = new Train();
                train.TrainPlan = true;
            }

            else if (train.TrainPlan = true & (mes == "/RusEng" || mes == "/EngRus"))
            {
                train.TrainRoute = mes;
                text = $"Для тренировки по тематике введите /trainsubj, если по всем словам -  /all";
            }

            else if (train.TrainPlan = true & mes == "/trainsubj")
            {
                train.TrainType = "Subj";
                text = "Укажите тематику слов:";
            }

            else if (train.TrainPlan = true & meslast == "/trainsubj")
            {
                train.TrainSubj = mes;
                train.TrainPlan = false;
                text = $"Планирование завершено.\r\nЧтоб начать тренировку, введите  /start";
            }

            else if (train.TrainPlan = true & mes == "/all")
            {
                train.TrainPlan = false;
                text = $"Планирование завершено.\r\nЧтоб начать тренировку, введите  /start";
            }
               
            else if (GlobalVar.fProcAddWord == false & train.TrainPlan == false & train.TrainStart == false & mes == "/start" & wordList.Count == 0)
            {
                text = "Ваш словарь пока пуст. Для заполнения его введите  /addword";
            }

            else if (GlobalVar.fProcAddWord == false & train.TrainPlan == false & train.TrainStart == false & mes == "/start")
            {
                train.TrainStart = true;
                text = "Тренировка начата.\r\nДля завершения введите  /stop";
                text += "\r\n" + messanger.ExecTrain(ref train, wordList, mes);  //процесс тренировки
            }
            
            else if (train.TrainStart == true & mes != "/stop")
            {
                text = messanger.ExecTrain( ref train, wordList, mes);  //идет процесс тренировки
            }
            else if (train.TrainStart == true & mes == "/stop")
            {
                train.TrainStart = false; //останавливаем тренировку
                text = "Тренировка завершена.";
            }

            else if (GlobalVar.fProcDelWord == true)
            {
                //Блок по удалению заданного слова из словаря
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
            else if (GlobalVar.fProcAddWord == true & GlobalVar.WordRus == "")
            {
                GlobalVar.WordRus = mes;
                text = "Введите английское значение слова";

            }
            else if (GlobalVar.fProcAddWord == true & GlobalVar.WordRus != "" & GlobalVar.WordEng == "")
            {
                GlobalVar.WordEng = mes;
                text = "Введите тематику";
            }
            else if (GlobalVar.fProcAddWord = true & GlobalVar.WordRus != "" & GlobalVar.WordEng != "" & GlobalVar.WordSubj == "")
            {
                GlobalVar.WordSubj = mes;

                Word word = new Word(GlobalVar.WordRus, GlobalVar.WordEng, GlobalVar.WordSubj);
                wordList.Add(word);
                // chatWordList.Add(chat.GetId(), wordList);

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
