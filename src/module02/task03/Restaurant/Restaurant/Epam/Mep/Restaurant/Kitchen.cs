using Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods;
using Epam.Mep.Restaurant.Epam.Mep.Restaurant.Foods.Extra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Mep.Restaurant.Epam.Mep.Restaurant
{
    public class Kitchen
    {
        private static readonly IReadOnlyDictionary<string, Func<IFood>> FoodFactory =
            new Dictionary<string, Func<IFood>>
            {
                [nameof(HotDog)] = () => new HotDog(),
                [nameof(Chips)] = () => new Chips()
            };

        private static readonly IReadOnlyDictionary<string, Func<IFood, IFood>> ExtraFactory =
            new Dictionary<string, Func<IFood, IFood>>
            {
                [nameof(Ketchup)] = food => new Ketchup(food),
                [nameof(Mustard)] = food => new Mustard(food)
            };

        internal IFood Cook(Order order)
        {
            Console.WriteLine($"Kitchen: Preparing food, order {order}");

            var mainFood = AddExtras(CreateMainFood(order.Food), order.Extras);

            Console.WriteLine($"Kitchen: Food prepared, order {order}");

            return mainFood;
        }

        private IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            var food = mainFood;

            foreach (var extra in extras)
            {
                food = AddExtra(food, extra);
            }

            return food;
        }

        private IFood AddExtra(IFood mainFood, string extra)
        {
            if (string.IsNullOrEmpty(extra))
            {
                throw new ArgumentException("Extra must be a valid string", nameof(extra));
            }

            if (!ExtraFactory.TryGetValue(extra, out var extraFactory))
            {
                throw new ArgumentOutOfRangeException(nameof(extra), "The extra must be either Ketchup or Mustard.");
            }

            return extraFactory(mainFood);
        }

        private IFood CreateMainFood(string food)
        {
            if (string.IsNullOrEmpty(food))
            {
                throw new ArgumentException("Food must be a valid string", nameof(food));
            }

            if (!FoodFactory.TryGetValue(food, out var foodFactory))
            {
                throw new ArgumentOutOfRangeException(nameof(food), "The food must be either HotDog or Chips.");
            }

            return foodFactory();
        }
    }
}
