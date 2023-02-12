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
        


        // Events
        static void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            // When a client connects, request the name of the client (computer name)

            Console.WriteLine($"[{e.IpPort}] client connected");
            _server.Send(e.IpPort, "REQUESTNAME+");
            
        }

        static void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            string computerRemoved = string.Empty;
            foreach (var VARIABLE in ClientMasterList)
            {
                if (VARIABLE.Key.Equals(e.IpPort))
                {
                    computerRemoved = VARIABLE.Value;
                    ClientMasterList.Remove(VARIABLE.Key);
                    break;
                }

            }

            foreach (var VARIABLE in ClientSlaveList)
            {
                if (VARIABLE.Key.Equals(e.IpPort))
                {
                    computerRemoved = VARIABLE.Value;
                    ClientSlaveList.Remove(VARIABLE.Key);
                    break;
                }
            }


            Console.WriteLine($"{computerRemoved} at socket [{e.IpPort}] has disconnected");

            foreach (var master in ClientMasterList)
            {
                if (ClientMasterList.Count > 0)
                {
                    _server.Send(master.Key, "REFRESHLIST");

                }
                else
                {
                    Console.WriteLine("No more Masters connected!");
                }
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
                    var slaveClientComputerName = string.Empty;
                    char[] splitter = {'+'};
                    string[] messageSplit = messageReceived.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in messageSplit)
                    {
                        if (item.Equals("REQUESTNAMESLAVE"))
                        {
                            continue;
                        }

                        slaveClientComputerName = item;

                        ClientSlaveList.Add(e.IpPort, slaveClientComputerName);

                    }
                    
                    Console.WriteLine("Slave " + slaveClientComputerName + " added to client list.");

                    // tell each master to update their slave list
                    if (ClientMasterList.Count > 0)
                    {
                        foreach (var master in ClientMasterList)
                        {
                            _server.Send(master.Key, "REFRESHLIST");
                        }
                    }
                    
                }

                else if (messageReceived.Contains("REQUESTNAMEMASTER"))
                {
                    var masterClientComputerName = string.Empty;
                    char[] splitter = { '+' };
                    string[] messageSplit = messageReceived.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in messageSplit)
                    {
                        if (item.Equals("REQUESTNAMEMASTER"))
                        {
                            continue;
                        }
                        masterClientComputerName = item;

                        ClientMasterList.Add(e.IpPort, masterClientComputerName);
                    }

                    Console.WriteLine("Slave " + masterClientComputerName + " added to master list.");
                    
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
                                _server.Send(e.IpPort, slave.Value + " has connected!");
                                
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


        static string[] MessageParser(string message, string header)
        {
            char[] splitterMessage = {'+'};
            string[] messageSplit = message.Split(splitterMessage, StringSplitOptions.RemoveEmptyEntries);
            string[] singleEntry = new string[] { };

            foreach (var entry in messageSplit)
            {
                if (entry.Equals(header))
                {
                    continue;
                }

                // split remaining message into 'ip address' and 'name'
                char[] spitterEntries = {','};
                singleEntry = entry.Split(spitterEntries, StringSplitOptions.None);

                // index [0] is IP Address, index [1] is Computer Name
                // add to dictionary
            }

            return singleEntry;

        }


        // Miscellaneous commandline methods
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
