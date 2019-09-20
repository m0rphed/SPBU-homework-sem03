using System;
using System.Collections.Generic;
using System.Threading;

namespace dining_philosophers
{
    internal static class DiningPhilosophers
    {
        public static void Main(string[] args)
        {
            var forks = new List<string>()
            {
                "fork_01", "fork_02", "fork_03", "fork_04"
            };

            var philosophers = new List<Philosopher>()
            {
                new Philosopher("Plato", forks[0], forks[1]),
                new Philosopher("Aristotle", forks[1], forks[2]),
                new Philosopher("Immanuel Kant", forks[2], forks[3]),
                new Philosopher("Frederik Nitsche", forks[3], forks[0])
            };

            var threads = new Stack<Thread>();
            foreach (var philosopher in philosophers)
            {
                threads.Push(new Thread(() => { philosopher.Run(); }));
            }
            
            Console.WriteLine("The End.");
        }
    }
}