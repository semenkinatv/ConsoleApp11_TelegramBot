using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11_TelegramBot
{/// <summary>
///  хранилище  команд
/// </summary>
    public class CommandParser
    {
        private List<IChatCommand> Command;

        public CommandParser()
        {
            Command = new List<IChatCommand>();
        }
    }
}
