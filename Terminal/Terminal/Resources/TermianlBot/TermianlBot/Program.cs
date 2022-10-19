using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot
{

    class Program
    {
        static string remoteAddress; // хост для отправки данных
        static int remotePort; // порт для отправки данных
        static int localPort = 8888; // локальный порт для прослушивания входящих подключений

        public static List<Terminal> terminalsList = new List<Terminal>();

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
                    //    if (IsLogged == false)
                    //    {
                    //        Login(botClient, update);
                    //    }
                    //    else
                    //    {
                    Functional(botClient, update);
                    //    }

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
                String[] words = message.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                switch (words[0])
                {
                    case "/start":
                        await botClient.SendTextMessageAsync(message.Chat, " ОБЯЗАТЕЛЬНО К ПРОЧТЕНИЮ\n" +
                                                                            "После того как попользовались ботом нужно обязательно выйти '/exit' - не актуально\n" +
                                                                            "Для просмотра всех команд пропишите '/help'\n" +
                                                                            "Команды не спамить!!!\n" +
                                                                            ""); //psexec \\172.16.122.19 -u AdminVPT -p Ugh6joosh -i 3 -s -d c:\Users\AdminVPT\Desktop\Release\Terminal.exe
                        break;

                    case "/on":
                        if (terminalsList.Count != 0)
                        {
                            for (int i = 0; i < terminalsList.Count; i++)
                            {
                                if (terminalsList[i].name == words[1])
                                {
                                    string command1 = $"psexec \\\\{terminalsList[i].ip} -u {terminalsList[i].name} -p Ugh6joosh -i 3 -s -d c:/Users/{terminalsList[i].name}/Desktop/Release/Terminal.exe";

                                    string command2 = $@"psexec \\172.16.122.19 -u {terminalsList[i].name} -p Ugh6joosh -i 3 -s -d taskkill /F /IM explorer.exe";

                                    Process p = new Process();
                                    p.StartInfo.UseShellExecute = false;
                                    p.StartInfo.FileName = "cmd.exe";
                                    p.StartInfo.Arguments = "/C " + command1;
                                    p.StartInfo.CreateNoWindow = false;
                                    p.Start();

                                    Process p2 = new Process();
                                    p2.StartInfo.UseShellExecute = false;
                                    p2.StartInfo.FileName = "cmd.exe";
                                    p2.StartInfo.Arguments = "/C " + command2;
                                    p2.StartInfo.CreateNoWindow = false;
                                    p2.Start();

                                    await botClient.SendTextMessageAsync(message.Chat, "Выполнено!");
                                }
                            }
                        }
                        else
                            await botClient.SendTextMessageAsync(message.Chat, "Терминалы не подключенны!");
                        break;

                    case "/off":
                        if (terminalsList.Count != 0)
                        {
                            for (int i = 0; i < terminalsList.Count; i++)
                            {
                                if (terminalsList[i].name == words[1])
                                {
                                    string command = $"psexec \\\\{terminalsList[i].ip} -i 3 -s -d -u {terminalsList[i].name} -p Ugh6joosh taskkill /F /IM Terminal.exe";
                                    string command1 = $"psexec \\\\{terminalsList[i].ip} -i 3 -s -d -u {terminalsList[i].name} -p Ugh6joosh explorer";

                                    Process p = new Process();
                                    p.StartInfo.UseShellExecute = false;
                                    p.StartInfo.FileName = "cmd.exe";
                                    p.StartInfo.Arguments = "/C " + command;
                                    p.StartInfo.CreateNoWindow = false;
                                    p.Start();

                                    Process p2 = new Process();
                                    p2.StartInfo.UseShellExecute = false;
                                    p2.StartInfo.FileName = "cmd.exe";
                                    p2.StartInfo.Arguments = "/C " + command1;
                                    p2.StartInfo.CreateNoWindow = false;
                                    p2.Start();

                                    await botClient.SendTextMessageAsync(message.Chat, "Выполнено!");
                                }
                            }
                        }
                        else
                            await botClient.SendTextMessageAsync(message.Chat, "Терминалы не подключенны!");
                        break;

                    case "/showterminal":
                        if (terminalsList.Count != 0)
                        {
                            for (int i = 0; i < terminalsList.Count; i++)
                            {
                                await botClient.SendTextMessageAsync(message.Chat, $"{i + 1}) name: {terminalsList[i].name} ip: {terminalsList[i].ip}");
                            }
                        }
                        else
                            await botClient.SendTextMessageAsync(message.Chat, "Терминалы не подключенны!");
                        break;

                    case "/help":
                        await botClient.SendTextMessageAsync(message.Chat, "\tВсе команды:\n\b" +
                                                                        " /start - Инструкция\n" +
                                                                        " /showterminal - Показать терминалы" +
                                                                        " /on name - вместо name, указать название терминала\n" +
                                                                        " /off name - вместо name, указать название терминала\n" +
                                                                        " /exit - Выйти\n");
                        break;

                    //case "/exit":
                    //    IsLogged = false;
                    //    await botClient.SendTextMessageAsync(message.Chat, "Вы вышли!");
                    //    break;

                    //case "/startterminal":
                    //    Process.Start("KillExplorer.bat");

                    //    await botClient.SendTextMessageAsync(message.Chat, "Терминал запущен!");
                    //    break;

                    //case "/offterminal":
                    //    Process.Start("Explorer.exe");
                    //    Process[] proc = Process.GetProcessesByName("Terminal");
                    //    proc[0].Kill();

                    //    await botClient.SendTextMessageAsync(message.Chat, "Терминал выключен!");
                    //    break;

                    default:
                        await botClient.SendTextMessageAsync(message.Chat, "Я не распознаю вашу команду!\n" +
                                                                                "Напишите '/help' что бы узнать все команды");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен Терминал бот: " + bot.GetMeAsync().Result.FirstName + "\n Telegram: @TerminalVPTBot  Login: AdminVPT  Pass: Ugh6joosh\n");

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
                cancellationToken);

            try
            {
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(localPort); // UdpClient для получения данных
            IPEndPoint remoteIp = null; // адрес входящего подключения
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные
                    string message = Encoding.Unicode.GetString(data);
                    String[] words = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Преобразуем строку в массив слов (0 - name 1 - ip)

                    for (int i = 0; i < terminalsList.Count; i++)
                    {
                        if (terminalsList[0].name == words[0])
                        {
                            terminalsList.RemoveAt(i);
                        }
                    }

                    Terminal terminal = new Terminal
                    {
                        name = words[0],
                        ip = words[1]
                    };

                    terminalsList.Add(terminal);

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine($"\nТерминал: {words[0]} добавлен!\n");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n   " + ex.Message + "\n");
            }
            finally
            {
                receiver.Close();
            }
        }
    }
}
