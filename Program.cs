using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new IntervalTimer();

            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Add(() => Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff")));
            timer.Add(async () =>
            {
                var random = new Random(DateTime.Now.Millisecond);
                var delay = random.Next(0, 5000);

                await Task.Delay(delay);
                Console.Write($"{delay} ".PadLeft(5));
            });
            timer.Add(async () =>
            {
                var random = new Random(DateTime.Now.Millisecond);
                var delay = random.Next(0, 4999);

                await Task.Delay(delay);
                Console.Write($"{delay} ".PadLeft(5));
            });
            timer.IsAsynchronous = true;
            timer.Start();

            Console.ReadLine();

            timer.Dispose();

            Console.WriteLine("End.");
            Console.ReadLine();

        }

    }
}
