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
        public static Point _lastHitPoint = null;
        public static IEnumerator<Point> _enumerator;
        public static Point NextShootPoint;
        public static Point point = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Space!!!");
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

            _enumerator = _battleShip.GetPointToFireEvgeny().GetEnumerator();
            //подписка
            consumer.Received += (s,e) => ProcessIncomingMess(s,e,channel);
            //отправляем
            channel.BasicPublish(_outQueue, _outQueue, null, Encoding.UTF8.GetBytes("start:USUAL"));
            
            Console.ReadLine();
            //отписка, диспозим
            //consumer.Received -= ProcessIncomingMess;
            connection.Dispose();
            channel.Dispose();
        }

        private static void ProcessIncomingMess(object sender, BasicDeliverEventArgs e, IModel channel)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Body));
            var message = Encoding.UTF8.GetString(e.Body);
           
            //Console.WriteLine("MESSAGE IS " + message);
            if (message.Contains("prepare"))
            {
                //расстановка
                Console.WriteLine("Setting Ships");
                channel.BasicPublish(_outQueue, _outQueue, null, Encoding.UTF8.GetBytes(_battleShip.SetUpShips()));
            }
            else if (message.Contains("fire!"))
            {
                
                if (SpecialEvent.Exist)
                {

                }
                else
                {
                    //var enumerator = _battleShip.GetPointToFireEvgeny().GetEnumerator();
                    if (_enumerator.MoveNext())
                    {
                        if (NextShootPoint != null && NextShootPoint!=point)
                        {
                            point = NextShootPoint;
                        }
                        else
                        {
                            point = _enumerator.Current;
                        }
                        channel.BasicPublish(_outQueue, _outQueue, null, Encoding.UTF8.GetBytes(point.ToString()));
                        //Battleship.EnemyField[point.X, point.Y] = CellType.Hitted;
                    }
                }
                
            }
            else if (message.Contains("fire result"))
            {
                var res = FireResult.Values.First(x => message.Contains(x));
                Console.WriteLine(res);
                if (res == "HIT")
                {
                    Battleship.LastHitPoint = point;
                    Battleship.LastHitStatus = "HIT";
                    Console.WriteLine("HITTED!!!!!!!!!!!!!");
                    NextShootPoint = Battleship.PointToHitWreckedShip(_lastHitPoint);
                    Console.WriteLine($"Next point to shoot {NextShootPoint.ToString()}");
                }

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
            else if (message.Contains("winner"))
            {
                //Console.ReadLine();
                //return;
            }
            //throw new NotImplementedException();
        }
    }

    public enum CellType
    {
        Empty,
        Ship,
        Wreck,
        Mine,
        Hitted
    }
    public static class FireResult
    {
        public static string[] Values = new string[] { "MISS", "MISS_AGAIN", "HIT", "HIT_AGAIN", "KILL", "HIT_MINE" };
    }
    public static class SpecialEvent
    {
        public static bool Exist { get; set; } = false;
        public static SpecialCondition Condition { get; set; }
    }
    public enum SpecialCondition
    {
        Wrecked,
        HasBigGun
    }

}
   
