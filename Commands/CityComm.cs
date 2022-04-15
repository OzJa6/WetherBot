using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using baseComm;

namespace CityComm
{
        public class CityHandler : UpdateHandler
    {

        bool waitForText;

        public override async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine((JsonSerializer.Serialize(update)));
            if (update.Type == UpdateType.Message)
            {

                if (update.Message.Text.Equals("/city", StringComparison.OrdinalIgnoreCase))
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat, "насколько сильно?", cancellationToken: cancellationToken);
                    waitForText = true;
                }
                else
                {
                    if (waitForText)
                    {
                        await botClient.SendTextMessageAsync(update.Message.Chat, $@"вот так вот люблю: {update.Message.Text}", cancellationToken: cancellationToken);
                        waitForText = false;
                    }
                    else
                    {
                        Successor.HandleUpdateAsync(botClient, update, cancellationToken);
                    }
                }

            }

        }
    }
}