using ApiAiSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelBot
{
    static class Commands
    {
       public static async void Start(TelegramBotClient bot, Message message)
       {
            string text = "Список команд:\n/start - запуск бота\n/inline - вывод меню\n/keyboard - вывод клавиатуры";
            await bot.SendTextMessageAsync(message.From.Id, text);
       }
        public static async void Inline(TelegramBotClient bot, Message message)
        {
            
            // 'replyMarkup: inlineKeyBoard' - отправка какого-то объекта, в нашем случае - меню
            await bot.SendTextMessageAsync(message.Chat.Id, "Выберите пункт меню", replyMarkup: GetInlineKeyboardMarkup());
        }
        private static InlineKeyboardMarkup GetInlineKeyboardMarkup()
        {
            //создание меню кнопок (двумерный массив)
            var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    //инициализация кнопок
                    new []
                    {
                        InlineKeyboardButton.WithUrl("VK", "https://vk.com/igoralmazov00" ),
                        InlineKeyboardButton.WithUrl("Instagram", "https://www.instagram.com/igoralmazov_official/?hl=ru")
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Картинка"),
                        InlineKeyboardButton.WithCallbackData("Видео"),
                    }
                });
            return inlineKeyBoard;
        }
        public static async void Keyboard(TelegramBotClient bot, Message message)
        {
            // 'replyMarkup: replykeyboard' - отправка какого-то объекта, в нашем случае - аналогии клавиатуры с кнопками
            await bot.SendTextMessageAsync(message.Chat.Id, "Выберите сообщение", replyMarkup: GetReplyKeyboardMarkup());
        }
        private static ReplyKeyboardMarkup GetReplyKeyboardMarkup()
        {
            //своя клавиатура вместо обычной текстовой клавиатуры
            var replykeyboard = new ReplyKeyboardMarkup(new[]
            {
                        new []
                        {
                            new KeyboardButton("Привет!"),
                            new KeyboardButton("Как дела?"),
                        },
                        new []
                        {
                            new KeyboardButton("Контакт") { RequestContact = true },
                            new KeyboardButton("Геолокация") { RequestLocation = true }
                        }
                    });
            return replykeyboard;
        }
        public static async void Default(TelegramBotClient bot, Message message)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Прости, я тебя не понял :(");
        }
        public static async void AI(TelegramBotClient bot, Message message)
        {
            //создание поддержки ИИ, подключение к DialogFlow с помощью ключа
            AIConfiguration conf = new AIConfiguration("b99c02960f9b246342807ebd3940680d34137509", SupportedLanguage.Russian); 
            ApiAi AI = new ApiAi(conf);
            string answer = "";
            try
            {
                var response = AI.TextRequest(message.Text);
                answer = response.Result.Fulfillment.Speech;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (answer == "")
            {
                answer = "Сервис ИИ временно недоступен :(";
            }
            await bot.SendTextMessageAsync(message.Chat.Id, answer);
        }
    }
}
