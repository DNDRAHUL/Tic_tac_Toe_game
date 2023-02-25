using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Tictoe
{
    public class Program
    {
        public static void Main(string[] args)
       {


            try
            {
                // Establish a remote endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 12345;
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP socket client.
                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.Connect(remoteEP);

                // Send data to the server.
                byte[] data = System.Text.Encoding.ASCII.GetBytes("Hello, server!");
                int bytesSent = client.Send(data);

                // Receive data from the server.
                byte[] buffer = new byte[1024];
                int bytesReceived = client.Receive(buffer);
                string response = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                Console.WriteLine("Received from server: {0}", response);

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
