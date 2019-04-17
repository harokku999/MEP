using Epam.Mep.Restaurant.Epam.Mep.Restaurant;
using Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods;
using Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods.Extra;
using System;

namespace Epam.Mep.Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var waitress = new Waitress(new Kitchen());

            waitress.TakeOrder(new Client("Client1", 100),
                new Order(nameof(HotDog), new[] { nameof(Ketchup) }));
            waitress.TakeOrder(new Client("Client1", 200),
                new Order(nameof(Chips), new[] { nameof(Mustard) }));
            waitress.TakeOrder(new Client("Client1", 100),
                new Order(nameof(Chips), new[] { nameof(Ketchup) }));

            waitress.ServeOrders();

            Console.ReadKey();
        }
    }
}
