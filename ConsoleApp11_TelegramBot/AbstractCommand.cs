using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11_TelegramBot
{   /// <summary>
/// абстрактная команда, проверяет по тексту, является ли она командой на этот текст. 
/// </summary>
    public abstract class AbstractCommand : IChatCommand
    {
        public string CommandText;

        public bool CheckMessage(string message)
        {
            return CommandText == message;
        }
    }
}
