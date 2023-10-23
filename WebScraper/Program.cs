// See https://aka.ms/new-console-template for more information

using Telegram.Bot;
using WebScraper;


var token = new TelegramBotClient("6456170969:AAFTVkLXkulLmrs3seARvSNGj9SvqsnuMdc");
TelegramBot telegramBot = new TelegramBot(token);
await telegramBot.GetMessagesAsync();

