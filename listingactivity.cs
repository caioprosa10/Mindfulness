using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    public class ListingActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        private List<string> _unusedPrompts;

        public ListingActivity()
            : base("Listing Activity",
                   "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
            _unusedPrompts = new List<string>(_prompts);
        }

        public override void Run()
        {
            DisplayStartingMessage();

            if (_unusedPrompts.Count == 0)
                _unusedPrompts = new List<string>(_prompts);

            var rnd = new Random();
            int idx = rnd.Next(_unusedPrompts.Count);
            string prompt = _unusedPrompts[idx];
            _unusedPrompts.RemoveAt(idx);

            Console.WriteLine();
            Console.WriteLine("List as many responses you can to the following prompt:");
            Console.WriteLine($"  --- {prompt} ---");
            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.WriteLine();

            int count = 0;
            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string response = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(response))
                    count++;
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {count} items!");
            DisplayEndingMessage();
        }
    }
}
