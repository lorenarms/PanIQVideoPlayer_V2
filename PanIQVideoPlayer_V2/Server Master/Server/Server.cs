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
    
    class Server
    {
        
        private static SimpleTcpServer _server;
        public static Dictionary<string, string> ClientSlaveList { get; set; }
        // ip address, computer name
        public static Dictionary<string, string> ClientMasterList { get; set; }
        public string ServerName { get; set; }
        public string ServerIp { get; set; }
        


        static void Main(string[] args)
        {
            _server = new SimpleTcpServer(GetLocalIpAddress() + ":9001");
            _server.Events.ClientConnected += Events_ClientConnected;
            _server.Events.ClientDisconnected += Events_ClientDisconnected;
            _server.Events.DataReceived += Events_DataReceived;

            ClientSlaveList = new Dictionary<string, string>();
            ClientMasterList = new Dictionary<string, string>();

            _server.Start();
            
            Console.WriteLine(GetLocalIpAddress());

            Console.ReadKey();

        }


        static void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            // When a client connects, request the name of the client (computer name)

            Console.WriteLine($"[{e.IpPort}] client connected");
            _server.Send(e.IpPort, "REQUESTNAME+" + e.IpPort);
            
        }

        static void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client disconnected");
            ClientSlaveList.Remove(e.IpPort);
            foreach (var master in ClientMasterList)
            {
                _server.Send(master.Key, "REFRESHLIST");
            }
            
        }


        static void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // When data is received, read the "header" first to determine where data should go for
            // processing

            // HEADER FORMAT: [HEADER] + [ipAddress] , [message]

            if (e.Data.Array != null)
            {
                // use ".ToArray() instead of ".Array" to avoid a '\0' character appending to computer name
                string messageReceived = Encoding.UTF8.GetString(e.Data.ToArray());

                if (messageReceived.Contains("REQUESTNAMESLAVE"))
                {

                    // remove "REQUESTNAMESLAVE" from message
                    char[] splitterMessage = {'+'};
                    string[] messageSplit = messageReceived.Split(splitterMessage, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var entry in messageSplit)
                    {
                        if (entry.Equals("REQUESTNAMESLAVE"))
                        {
                            continue;
                        }
                        
                        // split remaining message into 'ip address' and 'name'
                        char[] spitterEntries = {','};
                        string[] singleEntry = entry.Split(spitterEntries, StringSplitOptions.None);

                        // index [0] is IP Address, index [1] is Computer Name
                        // add to dictionary
                        ClientSlaveList.Add(singleEntry[0], singleEntry[1]);

                        Console.WriteLine("Slave " + singleEntry[1] + " added to client list.");
                        
                    }
                }

                else if (messageReceived.Contains("REQUESTNAMEMASTER"))
                {

                    // remove "REQUESTNAMEMASTER" from message
                    char[] splitterMessage = { '+' };
                    string[] messageSplit = messageReceived.Split(splitterMessage, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var entry in messageSplit)
                    {
                        if (entry.Equals("REQUESTNAMEMASTER"))
                        {
                            continue;
                        }

                        // split remaining message into 'ip address' and 'name'
                        char[] spitterEntries = { ',' };
                        string[] singleEntry = entry.Split(spitterEntries, StringSplitOptions.None);

                        // index [0] is IP Address, index [1] is Computer Name
                        // add to dictionary
                        ClientMasterList.Add(singleEntry[0], singleEntry[1]);

                        Console.WriteLine("Master " + singleEntry[1] + " added to master list.");

                    }
                }


                else if (messageReceived.Contains("COMMAND"))
                {
                    char[] splitterMessage = { '+' };
                    string[] messageSplit = messageReceived.Split(splitterMessage, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var entry in messageSplit)
                    {
                        if (entry.Equals("COMMAND"))
                        {
                            continue;
                        }
                        // split remaining message into 'ip address' and 'message'
                        char[] spitterEntries = { ',' };
                        string[] singleEntry = entry.Split(spitterEntries, StringSplitOptions.None);

                        Console.WriteLine("Command received! Rerouting to..." + singleEntry[0]);

                        _server.Send(singleEntry[0], singleEntry[1]);

                    }
                }

                else if (messageReceived.Contains("REQUESTSLAVELIST+"))
                {
                    if (ClientSlaveList.Count != 0)
                    {
                        // remove "REQUESTSLAVELIST" from message
                        
                        char[] splitterMessage = { '+' };
                        string[] messageSplit = messageReceived.Split(splitterMessage, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var entry in messageSplit)
                        {
                            if (entry.Equals("REQUESTSLAVELIST"))
                            {
                                continue;
                            }

                            foreach (var slave in ClientSlaveList)
                            {
                                _server.Send(e.IpPort, "SLAVE+" + slave.Key + "," + slave.Value);
                                Console.WriteLine("Slave " + slave.Key + "," + slave.Value + " sent to client " + e.IpPort);
                            }
                            
                        }

                    }
                    else
                    {
                        _server.Send(e.IpPort, "No clients connected yet!");
                    }
                }

                else 
                { 
                    Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");

                }
            }

            
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
