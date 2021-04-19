using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace ConsoleApp11_TelegramBot
{
    public class Word
    {
       private string WordRus;
        private string WordEng;
        private string WordSubj;
       

        public Word (string wordRus, string wordEng, string wordSubj)
        {
            WordRus = wordRus;
            WordEng = wordEng;
            WordSubj = wordSubj;
         }

       
    }
}
