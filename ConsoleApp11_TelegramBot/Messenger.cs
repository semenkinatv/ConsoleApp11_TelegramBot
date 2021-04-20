using System.Threading.Tasks;
using Telegram.Bot;

namespace ConsoleApp11_TelegramBot
{
    public class Messenger
    {

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