using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            Console.WriteLine(ip);
            TcpListener server = new TcpListener(ip, 8080);
            TcpClient client = default(TcpClient);

            try 
            {
                server.Start();
                Console.WriteLine("Server Started...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                Console.Read();
            }

            while (true)
            {
                client = server.AcceptTcpClient();

                byte[] rBuffer = new byte[100];
                NetworkStream stream = client.GetStream();

                stream.Read(rBuffer, 0, rBuffer.Length);

                StringBuilder msg = new StringBuilder();

                foreach (byte b in rBuffer)
                {
                    if (b.Equals(00))//check for null
                    {
                        break;
                    }
                    else
                    {
                        msg.Append(Convert.ToChar(b).ToString());
                    }
                }

                Console.Write(msg.ToString() + msg.Length); ;
            }
        }
    }
}
