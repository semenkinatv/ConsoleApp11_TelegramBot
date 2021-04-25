using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ConsoleApp11_TelegramBot
{
    public class Messenger
    {
        public string ExecTrain(ref Train train, List<Word> wordList, string mes)
        {
            var text = "";
            int num;
            num = train.OrderNum; //по какому элементу списка идет проверка перевода
            var word = wordList[num]; //проверяемое Слово

            if (mes == "/start")
            {
                num = -1;
            }
                       
            var numNext = num + 1;
            if (numNext == wordList.Count)
            {
                numNext = 0;//если дошли до конца - начинаем заново
            }

            var wordNext = wordList[numNext];

            //догоняем список до нужной тематики
            if (train.TrainType == "Subj")
            {
                while (wordNext.WordSubj != train.TrainSubj)
                {
                    numNext++;
                    wordNext = wordList[numNext];
                }
            }
            //запоминаем номер списка
            train.OrderNum = numNext;
            
            //Начало тренировки
            if (mes == "/start")
            {
                if (train.TrainRoute == "RusEng")
                {
                    text += $"\r\nПереведите слово: {wordNext.WordRus}";
                }
                else
                {
                    text += $"\r\nПереведите слово: {wordNext.WordEng}";
                }

                return text;  //в проверки дальше не идем - не с чем
            }


            //проверяем перевод в зависимости от направления
            if (train.TrainRoute == "RusEng")
            {
                if (word.WordEng.ToLower() == mes.ToLower())
                { text = "Верно!"; }
                else
                { text = $"Не верно, правильно: {word.WordEng}"; }

                text += $"\r\nПереведите слово: {wordNext.WordRus}";
            }
            else
            {
                if (word.WordRus.ToLower() == mes.ToLower())
                { text = "Верно!"; }
                else
                { text = $"Не верно, правильно: {word.WordRus}"; }
                text += $"\r\nПереведите слово: {wordNext.WordEng}";
            }
                        
            return text;
        }

        public string CreateTextMessage(Conversation chat)
        {
            var text = "";
            switch (chat.GetLastMessage())
            {
                case "/saymehi":
                    text = "привет";
                    break;
                case "/askme":
                    text = "как дела";
                    break;
                
                default:
                    var delimiter = ",";
                    text = "История ваших сообщений: " + string.Join(delimiter, chat.GetTextMessages().ToArray());
                    break;
            }

            return text;
        }
       

    }
}