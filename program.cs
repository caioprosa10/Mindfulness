using System;

namespace MindfulnessProgram
{
    // Creativity extension: prompts/questions never repeat until all have been used once.
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflection Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Activity activity = choice switch
                {
                    "1" => new BreathingActivity(),
                    "2" => new ReflectingActivity(),
                    "3" => new ListingActivity(),
                    "4" => null,
                    _   => null
                };

                if (activity == null)
                    break;

                activity.Run();

                Console.WriteLine();
                Console.WriteLine("Press ENTER to return to the main menu.");
                Console.ReadLine();
            }

            Console.WriteLine("Goodbye! Stay mindful!");
        }
    }
}
