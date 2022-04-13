using System;
using System.Net;
using System.Text;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Parserapi;
using api;
using Telegram.Bot.Extensions.Polling;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5280541045:AAHx3mNW-BW9U9nU_KgOTAJlgZWNW0xuWcA");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(JsonSerializer.Serialize(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Мариночка-любимочка, ты самая лучшая!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine((JsonSerializer.Serialize(exception)));
        }
        static void Main(string[] args)
        {

            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            UpdateHandler startHandler = new StartHandler();
            UpdateHandler cityHandler = new CityHandler();
            startHandler.Successor = cityHandler;
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                startHandler.HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();


            //Parser.Parser.UsingLinq();
            string text = GetHTML.GetCityHTML("https://rp5.ru/Погода_в_Оренбурге");
            Console.WriteLine(text);
        }
    }


    public class StartHandler : UpdateHandler
    {

        public override async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine((JsonSerializer.Serialize(update)));
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Text.Equals("/start", StringComparison.OrdinalIgnoreCase))
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat, "Мариночка-любимочка, ты самая лучшая, без ума от тебя!", cancellationToken: cancellationToken);
                }
                else 
                {
                    Successor.HandleUpdateAsync(botClient, update, cancellationToken);
                }
            }
        }


    }

    public class CityHandler : UpdateHandler
    {

        public override async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine((JsonSerializer.Serialize(update)));
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Text.Equals("/city", StringComparison.OrdinalIgnoreCase))
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat, "писос люблю", cancellationToken: cancellationToken);
                }
                else 
                {
                    Successor.HandleUpdateAsync(botClient, update, cancellationToken);
                }
            }

            }
        }


    
        public    class UpdateHandler 
        {
            public UpdateHandler Successor { get; set; }
            public virtual async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {}
        }

}
/*

public class CityHandler : UpdateHandler
    {

        public override async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine((JsonSerializer.Serialize(update)));
            switch (update)
            {
                case
                {
                    Type: UpdateType.Message,
                    Message: { Text: { } text, Chat: { } chat },
                } when text.Equals("/start", StringComparison.OrdinalIgnoreCase):
                    {
                        await botClient.SendTextMessageAsync(chat!, "Мариночка-любимочка, ты самая лучшая, без ума от тебя!", cancellationToken: cancellationToken);
                        break;
                    }
                case
                {
                    Type: UpdateType.Message,
                    Message: { Text: { } text, Chat: { } chat },
                } when text.Equals("/city", StringComparison.OrdinalIgnoreCase):
                    {
                        await botClient.SendTextMessageAsync(chat!, "название города", cancellationToken: cancellationToken);

                        break;
                    }

            }
        }

*/