using System;

class Program
{
    static void Main()
    {
        string reference = "John 3:16";
        string scriptureText = "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.";
        List<string> words = scriptureText.Split(' ').ToList();
        HashSet<int> hiddenIndices = new HashSet<int>();
        Random random = new Random();

        while (hiddenIndices.Count < words.Count)
        {
            Console.WriteLine(reference);
            Console.WriteLine(HideWords(words, hiddenIndices));
            Console.WriteLine("\nPress Enter to continue hiding words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                return;
            
            HideRandomWord(words, hiddenIndices, random);
        }

        Console.WriteLine(reference);
        Console.WriteLine(HideWords(words, hiddenIndices));
        Console.WriteLine("\nAll words are hidden. Program will exit.");
        Thread.Sleep(3000);
    }

    static void HideRandomWord(List<string> words, HashSet<int> hiddenIndices, Random random)
    {
        if (hiddenIndices.Count >= words.Count)
            return;

        int index;
        do
        {
            index = random.Next(words.Count);
        } while (hiddenIndices.Contains(index));
        
        hiddenIndices.Add(index);
    }

    static string HideWords(List<string> words, HashSet<int> hiddenIndices)
    {
        return string.Join(" ", words.Select((word, index) => hiddenIndices.Contains(index) ? new string('_', word.Length) : word));
    }
}