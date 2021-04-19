using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11_TelegramBot
{
    public interface IChatCommand
    {
        bool CheckMessage(string message);
    }
}
