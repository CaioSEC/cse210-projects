using System;


abstract class Goal
{
    protected string name;
    protected string description;
    protected int points;

    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract bool IsComplete();
    public abstract string SaveString();
    public abstract void LoadData(string[] parts);

    public string GetName() => name;
}

class SimpleGoal : Goal
{
    private bool completed = false;

    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!completed)
        {
            completed = true;
            return points;
        }
        return 0;
    }

    public override string GetStatus() => completed ? "[X]" : "[ ]";

    public override bool IsComplete() => completed;

    public override string SaveString() =>
        $"SimpleGoal|{name}|{description}|{points}|{completed}";

    public override void LoadData(string[] parts)
    {
        completed = bool.Parse(parts[4]);
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent() => points;

    public override string GetStatus() => "[~]";

    public override bool IsComplete() => false;

    public override string SaveString() =>
        $"EternalGoal|{name}|{description}|{points}";

    public override void LoadData(string[] parts) { }
}

class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus) 
        : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (currentCount < targetCount)
        {
            currentCount++;
            if (currentCount == targetCount)
                return points + bonus;
            return points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return currentCount >= targetCount ? "[X]" : $"[ ] Completed {currentCount}/{targetCount} times";
    }

    public override bool IsComplete() => currentCount >= targetCount;

    public override string SaveString() =>
        $"ChecklistGoal|{name}|{description}|{points}|{targetCount}|{bonus}|{currentCount}";

    public override void LoadData(string[] parts)
    {
        targetCount = int.Parse(parts[4]);
        bonus = int.Parse(parts[5]);
        currentCount = int.Parse(parts[6]);
    }
}

class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void AddGoal(Goal goal) => goals.Add(goal);

    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()} {goals[i].GetName()}");
        }
    }

    public void RecordGoal(int index)
    {
        if (index >= 0 && index < goals.Count)
        {
            int earned = goals[index].RecordEvent();
            Console.WriteLine($"You earned {earned} points!");
            score += earned;
        }
    }

    public void DisplayScore() => Console.WriteLine($"Total Score: {score}");

    public void Save(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(score);
            foreach (Goal goal in goals)
                writer.WriteLine(goal.SaveString());
        }
    }

    public void Load(string filename)
    {
        if (!File.Exists(filename)) return;

        string[] lines = File.ReadAllLines(filename);
        score = int.Parse(lines[0]);
        goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0];

            Goal goal = type switch
            {
                "SimpleGoal" => new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])),
                "EternalGoal" => new EternalGoal(parts[1], parts[2], int.Parse(parts[3])),
                "ChecklistGoal" => new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5])),
                _ => null
            };

            if (goal != null)
            {
                goal.LoadData(parts);
                goals.Add(goal);
            }
        }
    }

    public int GoalCount() => goals.Count;
}

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool quit = false;

        while (!quit)
        {
            Console.WriteLine("\nEternal Quest Menu");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Select goal type:");
                    Console.WriteLine("1. Simple Goal");
                    Console.WriteLine("2. Eternal Goal");
                    Console.WriteLine("3. Checklist Goal");
                    string type = Console.ReadLine();

                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());

                    if (type == "1")
                        manager.AddGoal(new SimpleGoal(name, desc, points));
                    else if (type == "2")
                        manager.AddGoal(new EternalGoal(name, desc, points));
                    else if (type == "3")
                    {
                        Console.Write("Enter target count: ");
                        int count = int.Parse(Console.ReadLine());
                        Console.Write("Enter bonus: ");
                        int bonus = int.Parse(Console.ReadLine());
                        manager.AddGoal(new ChecklistGoal(name, desc, points, count, bonus));
                    }
                    break;

                case "2":
                    manager.DisplayGoals();
                    break;

                case "3":
                    manager.DisplayGoals();
                    Console.Write("Which goal did you accomplish? ");
                    int goalIndex = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordGoal(goalIndex);
                    break;

                case "4":
                    manager.DisplayScore();
                    break;

                case "5":
                    manager.Save("goals.txt");
                    Console.WriteLine("Goals saved.");
                    break;

                case "6":
                    manager.Load("goals.txt");
                    Console.WriteLine("Goals loaded.");
                    break;

                case "7":
                    quit = true;
                    break;
            }
        }
    }
}