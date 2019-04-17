using Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant
{
    public class Client
    {
        public double Happiness { get; set; } // that setter should be private ffs
        public string Name { get; }

        public Client(string name, double happiness)
        {
            Name = name;
            Happiness = happiness;
        }

        public void Eat(IFood food)
        {
            Console.WriteLine($"Client: Starting to eat food, client {this}, food: {food}");
            Console.WriteLine("Csam csam nyam nyam");

            Happiness = food.CalculateHappiness(Happiness);

            Console.WriteLine($"Client: Food eaten, client: Client {this}");
        }

        public override string ToString()
        {
            return $"[name={Name}, happiness={Happiness:F1}]";
        }
    }
}
