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
        public static Battleship _battleShip = new Battleship();
        public static string _outQueue = "group1";
        public static string _incQueue = "to_group1";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            //устанавливаем соединение
            var connFactory = new ConnectionFactory { Uri = "amqp://group1:F3pgbj@91.241.45.69/debug" };
            var connection = connFactory.CreateConnection();
            var channel = connection.CreateModel();
            //настраиваем
            channel.QueueDeclare(_outQueue, exclusive: false);

            //конфигурируем для входищих
            var consumer = new EventingBasicConsumer(channel);
            channel.QueueDeclare(_incQueue, exclusive: false);
            channel.QueueBind(_incQueue, _incQueue, _incQueue);
            channel.BasicConsume(_incQueue, true, consumer);
            //подписка
            consumer.Received += (s,e) =>   ProcessIncomingMess(s,e,channel);
            //отправляем
            channel.BasicPublish(_outQueue, _outQueue, null, Encoding.UTF8.GetBytes("start:SELF"));


            //расстановка
            Console.WriteLine("Setting Ships");
            channel.BasicPublish(_outQueue, _outQueue, null, Encoding.UTF8.GetBytes(_battleShip.SetUpShips()));

            Console.ReadLine();
            //отписка, диспозим
            //consumer.Received -= ProcessIncomingMess;
            connection?.Dispose();
            channel?.Dispose();
        }

        private static void ProcessIncomingMess(object sender, BasicDeliverEventArgs e, IModel channel)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Body));
            var message = Encoding.UTF8.GetString(e.Body);
            if (message.Contains("prepare"))
            {
                var point = _battleShip.GetPointToFire();
                channel.BasicPublish(_outQueue, _outQueue, null, Encoding.UTF8.GetBytes(point.ToString()));
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

    public enum CellType
    {
        Empty,
        Ship,
        Wreck,
        Mine
    }

}
   
