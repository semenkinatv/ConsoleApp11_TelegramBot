using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace ConsoleApp11_TelegramBot
{
    public class Word
    {
        public string WordRus;
        public string WordEng;
        public string WordSubj;
       

        public Word (string wordRus, string wordEng, string wordSubj)
        {
            WordRus = wordRus;
            WordEng = wordEng;
            WordSubj = wordSubj;
         }

       
    }
}
