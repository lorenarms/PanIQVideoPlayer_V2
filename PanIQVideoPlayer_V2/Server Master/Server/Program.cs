using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;

namespace Server
{
    
    class Program
    {
        
        private static SimpleTcpServer _server;
        public Dictionary<string, string> clientSlaveList { get; set; }
        public Dictionary<string, string> clientMasterList { get; set; }
        public string serverName { get; set; }
        public string serverIp { get; set; }
        


        static void Main(string[] args)
        {
            _server = new SimpleTcpServer(GetLocalIpAddress() + ":9001");
            _server.Events.ClientConnected += Events_ClientConnected;
            _server.Events.ClientDisconnected += Events_ClientDisconnected;
            _server.Events.DataReceived += Events_DataReceived;

            _server.Start();
            
            Console.WriteLine(GetLocalIpAddress());

            Console.ReadKey();

        }


        static void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client connected");
            
        }

        static void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client disconnected");
        }
        static void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.Array != null)
                Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }





        static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var name = Dns.GetHostName();
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        static string GetLocalHostName()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var name = Dns.GetHostName();
            return name;
        }
    }
}
