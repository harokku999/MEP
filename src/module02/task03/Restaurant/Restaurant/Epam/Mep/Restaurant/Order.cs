using Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant
{
    public class Order
    {
        public event EventHandler<FoodReadyEventArgs> FoodReady;
        public IEnumerable<string> Extras { get; }
        public string Food { get; }


        public void NotifyReady(IFood food)
        {
            Console.WriteLine($"Order: Notifying observers of {this}");

            FoodReady?.Invoke(this, new FoodReadyEventArgs
            {
                Food = food
            });

            Console.WriteLine("Order: Notification done.");
        }

        public Order(string food, IEnumerable<string> extras)
        {
            Food = food;
            Extras = extras;
        }

        public override string ToString()
        {
            return $"[food={Food}, extras=[{string.Join(", ", Extras)}]]";
        }
    }
}
