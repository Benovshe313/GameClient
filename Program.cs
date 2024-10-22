
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


namespace GameClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ep = new IPEndPoint(IPAddress.Parse("192.168.100.115"), 27001);
            var client = new TcpClient();

            try
            {
                client.Connect(ep);
                if (client.Connected)
                {
                    var networkstream = client.GetStream();
                    var sw = new StreamWriter(networkstream) { AutoFlush = true };
                    var sr = new StreamReader(networkstream);

                    while (true)
                    {
                        var invitemsg = sr.ReadLine();
                        Console.WriteLine(invitemsg);

                        var msg = Console.ReadLine()!;
                        sw.WriteLine(msg);

                        if (msg == "1" || msg == "2") break;
                        Console.WriteLine("invalid input");
                    }

                    while (true)
                    {
                        var gamemsj = sr.ReadLine();
                        Console.WriteLine(gamemsj);

                        if (gamemsj.Contains("your turn"))
                        {
                            var move = Console.ReadLine()!;
                            sw.WriteLine(move);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
