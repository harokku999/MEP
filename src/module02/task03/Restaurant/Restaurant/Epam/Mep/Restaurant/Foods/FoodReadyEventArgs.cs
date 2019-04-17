using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods
{
    public class FoodReadyEventArgs : EventArgs
    {
        public IFood Food { get; set; }
    }
}
