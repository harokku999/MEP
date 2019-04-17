using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods.Extra
{
    public abstract class Extra : IFood
    {
        protected readonly IFood Food;

        internal Extra(IFood food)
        {
            Food = food;
        }

        public abstract double CalculateHappiness(double happiness);
    }
}
