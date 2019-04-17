using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant
{
    public class Waitress
    {
        private readonly Kitchen _kitchen; // should be private property, but cmon, srsly?...
        private readonly Queue<Order> _orders;

        public Waitress(Kitchen kitchen)
        {
            _kitchen = kitchen;
            _orders = new Queue<Order>();
        }

        public void ServeOrders()
        {
            Console.WriteLine($"WaitressRobot: Processing {_orders.Count} order(s)...");

            while (_orders.Count > 0)
            {
                var nextOrder = _orders.Dequeue();
                var food = _kitchen.Cook(nextOrder);
                nextOrder.NotifyReady(food);
            }
        }

        public void TakeOrder(Client client, Order order)
        {
            order.FoodReady += (sender, args) =>
            {
                Console.WriteLine($"Notifying observers of order {order}");
                client.Eat(args.Food);
            };
            _orders.Enqueue(order);
            Console.WriteLine($"WaitressRobot: Order registered, client: {client}, order: {order}.");
        }
    }
}
