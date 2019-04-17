using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods
{
    public class HotDog : Food
    {
        public override double CalculateHappiness(double happiness) => happiness + 2;
    }
}
