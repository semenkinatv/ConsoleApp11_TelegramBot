using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11_TelegramBot
{
    public class Train
    {
        private string TrainType;  //слова по тематикам или все (Subj или All)
        private string TrainRoute; //направление (RusEng или EngRus)
        private bool TrainStart;  //Признак, что тренировка начата

        public Train()
        {
            TrainType = "RusEng";
            TrainRoute = "All";
            TrainStart = false;
        }
        public void SetTrainType(string trainType)
        {
            TrainType = trainType;
         }
        public string GetTrainType()
        {
            return TrainType;
        }

        public void SetTrainRoute(string trainRoute)
        {
            TrainRoute = trainRoute;
        }
        public string GetTrainRoute()
        {
            return TrainRoute;
        }

        public void SetTrainStart(bool trainStart)
        {
            TrainStart = trainStart;
        }


    }
}
