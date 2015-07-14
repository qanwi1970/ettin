using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RightHead
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var factory = new ConnectionFactory {HostName = "localhost"};
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare("ettin", false, false, false, null);

					var consumer = new QueueingBasicConsumer(channel);
					channel.BasicConsume("ettin", true, consumer);

					Console.WriteLine("Waiting for messages.\nPress Ctrl-C to exit.");

					while (true)
					{
						var ea = (BasicDeliverEventArgs) consumer.Queue.Dequeue();
						var body = ea.Body;
						var message = Encoding.UTF8.GetString(body);
						Console.WriteLine("  Received {0}", message);
						Thread.Sleep(1200);
						Console.WriteLine("Retrieving data for {0}", message);
						Thread.Sleep(1450);
						Console.WriteLine("Doing some processing for {0}", message);
						Thread.Sleep(2500);
						Console.WriteLine("Sending emails for {0}", message);
						Console.WriteLine("All done with {0}", message);
					}
				}
			}
		}
	}
}
