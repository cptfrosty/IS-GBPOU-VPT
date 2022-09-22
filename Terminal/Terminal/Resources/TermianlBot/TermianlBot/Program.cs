using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot
{

    class Program
    {
        public static List<Terminal> terminalsList = new List<Terminal>();

        static DispatcherTimer timer = new DispatcherTimer();

        static bool IsLogged = false;

        static ITelegramBotClient bot = new TelegramBotClient("5430802560:AAGIuFywe6a5O0W4b5wmO6_uUv1JygubetI");

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update) + "\n");
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                try
                {
                    if (IsLogged == false)
                    {
                        Login(botClient, update);
                    }
                    else
                    {
                        Functional(botClient, update);
                    }

                }
                catch (Exception)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Я начинаю ломаться(((");
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static async void Login(ITelegramBotClient botClient, Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                await botClient.SendTextMessageAsync(message.Chat, "Введите пароль:");

                if (message.Text.ToLower() == TermianlBot.Properties.Settings.Default.Password.ToString())
                {
                    IsLogged = true;

                    Functional(botClient, update);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Пароль не верный!\nПопробуйте снова...");
                }
            }
        }

        static async void Functional(ITelegramBotClient botClient, Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                String[] words = message.Text.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch (message.Text.ToLower())
                {
                    case "/start":
                        await botClient.SendTextMessageAsync(message.Chat, "ОБЯЗАТЕЛЬНО К ПРОЧТЕНИЮ!\n" +
                                                                            "После того как попользовались ботом нужно обязательно выйти '/exit'\n" +
                                                                            "Для просмотра всех команд пропишите '/help'\n" +
                                                                            "Команды не спамить!!!\n" +
                                                                            "");
                        break;

                    case "/help":
                        await botClient.SendTextMessageAsync(message.Chat, "\tВсе команды:\n\b" +
                                                                        " /start - Инструкция\n" +
                                                                        " /addterminal - Добавить терминал\n" +
                                                                        " / - Выбор терминала\n" +
                                                                        " /exit - Выйти\n");
                        break;

                    case "/addterminal":
                        //await botClient.SendTextMessageAsync(message.Chat, "Введите имя терминала и его ip (Пример[terminal 192.168.0.1])");
                        Terminal terminal = new Terminal();                        
                        break;

                    case "/exit":
                        IsLogged = false;
                        await botClient.SendTextMessageAsync(message.Chat, "Вы вышли!");
                        break;

                    case "/startterminal":
                        Process.Start("KillExplorer.bat");

                        await botClient.SendTextMessageAsync(message.Chat, "Терминал запущен!");
                        break;

                    case "/offterminal":
                        Process.Start("Explorer.exe");
                        Process[] proc = Process.GetProcessesByName("Terminal");
                        proc[0].Kill();

                        await botClient.SendTextMessageAsync(message.Chat, "Терминал выключен!");
                        break;

                    default:
                        await botClient.SendTextMessageAsync(message.Chat, "Я не распознаю вашу команду!\n" +
                                                                                "Напишите '/help' что бы узнать все команды");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен Терминал бот\n Telegram: @TerminalVPTBot   Pass: Ugh6joosh\n" + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        static async void Client(string address, ITelegramBotClient botClient, Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                try
                {

                    Console.WriteLine("Идёт подключение...\n");

                    await botClient.SendTextMessageAsync(message.Chat, "Идёт подключение...");

                    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), 8005);

                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);
                    Console.Write("Выберите команду:\n" +
                        "1) Открыть проводник\n");
                    Console.Write("Команда: ");
                    string answer = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(answer);
                    socket.Send(data);

                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    Console.WriteLine("ответ сервера: " + builder.ToString());

                    // закрываем сокет
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
