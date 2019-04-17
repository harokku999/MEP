using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods.Extra
{
    public class Mustard : Extra
    {
        public Mustard(IFood food) : base(food) { }

        public override double CalculateHappiness(double happiness) => happiness + 1;
    }
}
