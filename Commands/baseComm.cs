using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace baseComm
{
    public class UpdateHandler
    {
        public UpdateHandler Successor { get; set; }
        public virtual async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        { }
    }
    public class OtherHandler : UpdateHandler
    {

        public override async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine((JsonSerializer.Serialize(update)));
            if (update.Type == UpdateType.Message)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat, "нипанимаю", cancellationToken: cancellationToken);
            }
        }


    }
}