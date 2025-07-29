using System;
using System.Threading;

namespace MindfulnessProgram
{
    public abstract class Activity
    {
        private string _name;
        private string _description;
        protected int Duration { get; private set; }

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"--- {_name} ---");
            Console.WriteLine(_description);
            Console.Write("How long, in seconds, would you like to do this activity? ");
            while (!int.TryParse(Console.ReadLine(), out int input) || input <= 0)
            {
                Console.Write("Please enter a positive integer for seconds: ");
            }
            Duration = input;
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
        }

        public void DisplayEndingMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(2);
            Console.WriteLine($"You have completed the {_name} for {Duration} seconds.");
            ShowSpinner(3);
        }

        protected void ShowSpinner(int seconds)
        {
            string[] sequence = { "|", "/", "—", "\\" };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int index = 0;
            while (DateTime.Now < endTime)
            {
                Console.Write(sequence[index % sequence.Length]);
                Thread.Sleep(250);
                Console.Write("\b \b");
                index++;
            }
        }

        protected void ShowCountDown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        public abstract void Run();
    }
}
