using System;
using System.Threading;

namespace dining_philosophers
{
    public class Philosopher
    {
        private readonly string _name;
        private State _state;
        private readonly object _leftFork;
        private readonly object _rightFork;

        public Philosopher(string name, object leftFork, object rightFork)
        {
            this._name = name;
            this._leftFork = leftFork;
            this._rightFork = rightFork;
        }

        public void Run()
        {
            var random = new Random();

            while (true)
            {
                var randomDelay = random.Next(0, 100);
                switch (_state)
                {
                    case State.Thinking:
                        DoAction(State.EatingAndThinking, randomDelay);
                        break;
                    case State.EatingAndThinking:
                        DoAction(State.Thinking, randomDelay);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void DoAction(State action, int delay)
        {
            switch (action)
            {
                case State.Thinking:
                    Console.WriteLine($"Philosopher {this._name} just thinking.");
                    _state = State.Thinking;
                    Thread.Sleep(delay);
                    break;
                case State.EatingAndThinking:
                    lock (_leftFork)
                    {
                        Console.WriteLine($"Philosopher {this._name} picking up left fork...");
                        lock (_rightFork)
                        {
                            Console.WriteLine($"Philosopher {this._name} picking up right fork...");
                            Console.WriteLine($"Philosopher {this._name} eating and thinking...");
                            _state = State.EatingAndThinking;
                            Thread.Sleep(delay);
                        }

                        Console.WriteLine($"Philosopher {this._name} is not really hungry.");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), "No such action!");
            }
        }
    }
}