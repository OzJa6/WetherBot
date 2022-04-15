using Telegram.Bot;
using System.Text.Json;
using Telegram.Bot.Extensions.Polling;

using Parserapi;
using api;
using baseComm;
using StartComm;
using CityComm;


namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static ITelegramBotClient bot = new TelegramBotClient("5280541045:AAHx3mNW-BW9U9nU_KgOTAJlgZWNW0xuWcA");

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
            UpdateHandler otherHandler = new OtherHandler();
            startHandler.Successor = cityHandler;
            cityHandler.Successor = otherHandler;


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