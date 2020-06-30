using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BotWorker botWorker = new BotWorker();
            botWorker.StartWork();
        }
    }
}
