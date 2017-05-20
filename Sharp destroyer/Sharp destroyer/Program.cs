using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_destroyer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            var connFactory = new ConnectionFactory { Uri = "amqp://group1:F3pgbj@91.241.45.69/debug" };
            var connection = connFactory.CreateConnection();
            var channel = connection.CreateModel();
            var outQueue = "group1";
            channel.QueueDeclare(outQueue, exclusive: false);

             // отправляем
            Console.WriteLine("sended start:self");
            var consumer = new EventingBasicConsumer(channel);
            var incQueue = "to_group1";
            channel.QueueDeclare(incQueue, exclusive: false);
            channel.QueueBind(incQueue, incQueue, incQueue);
            channel.BasicConsume(incQueue, true, consumer);
            consumer.Received += ProcessIncomingMess;

            channel.BasicPublish(outQueue, outQueue, null, Encoding.UTF8.GetBytes("start:SELF"));
            Console.ReadLine();

            consumer.Received -= ProcessIncomingMess;
            connection?.Dispose();
            channel?.Dispose();
        }

        private static void ProcessIncomingMess(object sender, BasicDeliverEventArgs e)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Body));
            var message = Encoding.UTF8.GetString(e.Body);
            if (message.Contains("prepare"))
            {

            }
            else if (message.Contains("Error"))
            {

            }
            else if (message.Contains("Warning"))
            {

            }
            else if (message.Contains("Info"))
            {

            }
            //throw new NotImplementedException();
        }
    }
    
   
}
