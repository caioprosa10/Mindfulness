using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    public class ReflectingActivity : Activity
    {
        private readonly List<string> _prompts = new()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
        private readonly List<string> _questions = new()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
        private List<string> _unusedQuestions;

        public ReflectingActivity()
            : base("Reflection Activity",
                   "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
            _unusedQuestions = new List<string>(_questions);
        }

        public override void Run()
        {
            DisplayStartingMessage();

            var rnd = new Random();
            string prompt = _prompts[rnd.Next(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine("When you have something in mind, press ENTER to continue.");
            Console.ReadLine();

            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            while (DateTime.Now < endTime)
            {
                if (_unusedQuestions.Count == 0)
                    _unusedQuestions = new List<string>(_questions);

                int idx = rnd.Next(_unusedQuestions.Count);
                string question = _unusedQuestions[idx];
                _unusedQuestions.RemoveAt(idx);

                Console.Write($"> {question} ");
                ShowSpinner(4);
                Console.WriteLine();
            }

            DisplayEndingMessage();
        }
    }
}
