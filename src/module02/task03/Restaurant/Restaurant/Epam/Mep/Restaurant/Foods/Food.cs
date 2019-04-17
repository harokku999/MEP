using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods
{
    public abstract class Food : IFood
    {
        public abstract double CalculateHappiness(double happiness);
    }
}
