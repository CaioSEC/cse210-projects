using System;

class Program
{
    static void Main(string[] args)
    {
        string answer = ""; // Store the answer globally

        while (true)
        {
            Console.WriteLine("Please select one option:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Save");
            Console.WriteLine("4. Load");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? Please insert the number: ");
            
            string options = Console.ReadLine();

            if (options == "1")
            {
                Console.WriteLine("What did I learn today?");
                answer = Console.ReadLine();
            }
            else if (options == "2")
            {
                Console.WriteLine("You wrote: " + (answer == "" ? "Nothing" : answer));
                Console.WriteLine();
            }
            else if (options == "5")
            {
                break;
            }
        }   
    }
}


