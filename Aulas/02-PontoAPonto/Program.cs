using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _02_PontoAPonto
{
    class Program
    {
        const int porta = 7000;

        const string ip = "10.0.2.255";

        static void Main(string[] args)
        {
            try  // par servidor
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                UdpClient par = new UdpClient(porta);
                Console.WriteLine("Para sair, pressione CTRL+C.");

                while (true)
                {
                    byte[] bytesRecebidos = par.Receive(ref endPoint);
                    Console.WriteLine("Mensagem recebida:{0}", Encoding.ASCII.GetString(bytesRecebidos));
                    Console.WriteLine("Mensagem enviada por {0}:{1}",
                        endPoint.Address.ToString(),
                        endPoint.Port.ToString());
                }
            }
            catch (Exception)  // par cliente
            {
                UdpClient par = new UdpClient();
                par.EnableBroadcast = true;

                Console.WriteLine("Envie uma mensagem. Para sair, pressione ENTER.");
                Console.Write("> ");
                string msg = Console.ReadLine();

                while(msg.Length > 0)
                {
                   byte[] bytesEnviados = Encoding.ASCII.GetBytes(msg);
                   par.Send(bytesEnviados, bytesEnviados.Length, ip, porta);

                   Console.Write("> ");
                   msg = Console.ReadLine();
                }

                par.Close();
            }
        }
    }
}
