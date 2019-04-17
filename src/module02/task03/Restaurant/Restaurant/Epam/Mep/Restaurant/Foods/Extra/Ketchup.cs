﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods.Extra
{
    public class Ketchup : Extra
    {
        public Ketchup(IFood food) : base(food) { }

        public override double CalculateHappiness(double happiness) =>
            (Food.CalculateHappiness(happiness) - happiness) * 2 + happiness;
    }
}
