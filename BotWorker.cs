using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelBot
{
    class BotWorker
    {
        const string botKey = "1360234596:AAE6dvPtYEtC4_KNW7_USMEMqgc_bdCA8oE";
        public TelegramBotClient bot { get; }
        private User Me;
        public BotWorker()
        {
            bot = new TelegramBotClient(botKey);
            //событие на сообщение от пользователя
            bot.OnMessage += Bot_OnMessage;
            //событие на нажатие кнопок
            bot.OnCallbackQuery += Bot_OnCallbackQuery;
            //получение инфы о самом боте
            Me = bot.GetMeAsync().Result;
        }
        public void StartWork()
        {
            Console.WriteLine(Me.FirstName + " " + Me.LastName + "\n");

            //начало прослушивания сообщений
            bot.StartReceiving();

            Console.ReadLine();

            bot.StopReceiving();
        }
        private async void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var butText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"Пользователь {name} нажал кнопку: {butText}");

            if (butText == "Картинка")
            {
                string imgPath = "https://lh3.googleusercontent.com/proxy/5SMTR4FvDtP6cE7jY09avJxw4SC9YBaSzUmzzDSIWULRo6DrGmSGNxo4_1BhqoiEstJWurmzbdGd6ltmwq7StfDPWeHPD7m2L4eSOSDwEuwB28qsBMvsjX-6YEWTKuMgG4H-PK_6TJi9gT4MK5rDtu9v";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, imgPath);
            }
            if (butText == "Видео")
            {
                string videoPath = "https://www.youtube.com/watch?v=h7BVwVNsIQk";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, videoPath);
            }
            //всплывающее уведомление в телеге сверху
            await bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы нажали кнопку: \"{butText}\"");
        }
        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Type != MessageType.Text || message == null)
            {
                return;
            }
            var userName = $"{message.From.FirstName} {message.From.LastName}";
            Console.WriteLine($"Message: \"{message.Text}\" from <{userName}>");

            switch (message.Text)
            {
                case "/start":
                    Commands.Start(bot, message);
                    break;

                case "/inline":
                    Commands.Inline(bot, message);
                    break;

                case "/keyboard":
                    Commands.Keyboard(bot, message);
                    break;

                default:
                    Commands.Default(bot, message);
                    //Commands.AI(bot, message);
                    break;
            }
        }
    }
}
