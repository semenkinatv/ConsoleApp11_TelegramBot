using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11_TelegramBot
{
    public class Train
    {
        public string TrainType { get; set; } //слова по тематикам или все (Subj или All)
        public string TrainSubj { get; set; } // выбранная тематика тренировки
        public string TrainRoute { get; set; } //направление (RusEng или EngRus)
        public bool TrainPlan { get; set; }    //Признак, что идет планирование тренировки
        public bool TrainStart { get; set; }   //Признак, что тренировка начата
        public int OrderNum { get; set; }      //номер элемента списка, по которому проверяют перевод

        public Train()
        {
            TrainType = "RusEng";
            TrainRoute = "All";
            TrainStart = false;
            TrainPlan = false;
            OrderNum = 0;
        }
    }
}
