using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClientApp
{
    class Program
    {
        // Получение имени компьютера.
        static String host = Dns.GetHostName();
        // Получение ip-адреса.
        static IPAddress ip = Dns.GetHostByName(host).AddressList[1];
        // Показ адреса в label'е.
        static string ipToString = ip.ToString();

        static void Main(string[] args)
        {
            try
            {               
                Console.WriteLine("Name: " + System.Environment.UserName + "    Ip: " + ipToString + "\n\n");

                string ip;
                using (StreamReader readtext = new StreamReader("ipConfig.txt"))
                {
                    ip = readtext.ReadLine();
                }

                string remoteAddress = ip;
                int remotePort = 8888;

                ConnectToServer(ip, remotePort); // Подключаемся

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConnectToServer(string ip, int port)
        {
            UdpClient sender = new UdpClient(); // создаем UdpClient
            try
            {
                string message = $"{System.Environment.UserName} {ipToString}";
                byte[] data = Encoding.Unicode.GetBytes(message);
                sender.Send(data, data.Length, ip, port); // отправка
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            } 
        }
    }
}