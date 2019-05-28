using System;

namespace MepAirlines.ConsoleUi
{
    public interface IUserInputService
    {
        T GetDataFromUser<T>(string message, Func<string, bool> validator, Func<string, T> converter,
            string errorMessage);
    }
    public class UserInputService : IUserInputService
    {
        public T GetDataFromUser<T>(string message, Func<string, bool> validator, Func<string, T> converter, string errorMessage)
        {
            while (true)
            {
                Console.Write(message);
                var input = Console.ReadLine();
                if (validator(input))
                {
                    return converter(input);
                }

                Console.WriteLine(errorMessage);
            }
        }
    }
}
