using System;
using System.Text;
using RabbitMQ.Client;

namespace LeftHead
{
	class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory {HostName = "localhost"};
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare("ettin", false, false, false, null);

					string message = "Cancelling";
					if (args.Length > 0) message += " " + args[0];
					var body = Encoding.UTF8.GetBytes(message);

					channel.BasicPublish("", "ettin", null, body);
					Console.WriteLine("  Sent {0}", message);
				}
			}
		}
	}
}
