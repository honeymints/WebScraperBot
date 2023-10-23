using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebScraper;

public class TelegramBot
{
    public readonly TelegramBotClient _client;

    public TelegramBot(TelegramBotClient client)
    {
        _client = client;
    }
    
    public static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        List<Announcement> lists = new List<Announcement>();
        WorkItScraper workItScraper=new WorkItScraper();
        lists=await workItScraper.Scrape();
        
        var message = update.Message;
        if (update.Type.Equals(UpdateType.Message))
        {
            if (update.Message.Text.Equals("/start"))
            {
                await client.SendTextMessageAsync(message.Chat, lists[0].Content);
            }
        }
        else
        {
            return;
        }
            
    }
    public async Task GetMessagesAsync()
    {
        using var cts = new CancellationTokenSource();

        var receiverOption = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler:HandlePollingErrorAsync,
            receiverOptions:receiverOption,
            cancellationToken:cts.Token
        );
        var me = await _client.GetMeAsync();
        Console.WriteLine($"started listenig to {me.Username}");
        Console.ReadLine();
    }
    public static async Task HandlePollingErrorAsync(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        await Task.CompletedTask;
    }
}