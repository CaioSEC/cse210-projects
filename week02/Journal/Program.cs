using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please select one option.");
        Console.WriteLine();
        Console.Write("1. Write ");
        Console.WriteLine();
        Console.Write("2. Display ");
        Console.WriteLine();
        Console.Write("3. Save ");
        Console.WriteLine();
        Console.Write("4. Load ");
        Console.WriteLine();
        Console.Write("5. Quit ");
        Console.WriteLine();
        Console.Write("What would you like to do? ");
        string options = Console.ReadLine();

        if (options == "1")
        {
            Console.WriteLine("What did i learned today?");
            string answer = Console.ReadLine();
        }
    }
}


