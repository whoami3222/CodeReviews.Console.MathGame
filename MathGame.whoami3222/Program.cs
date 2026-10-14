using System.Diagnostics;
int points = 0;
List<string> history = new List<string>();
MathGame();


void MathGame()
{
    DisplayMenu();
    string? input = Console.ReadLine();
    HandleMenuChoice(input);

}

void DisplayMenu()
{
    Console.WriteLine("Choose from the menu below:");
    Console.WriteLine("1. Start Game");
    Console.WriteLine("2. Display History");
    Console.WriteLine("3. Exit");
}

void HandleMenuChoice(string? choice)
{
    switch (choice)
    {
        case "1":
            StartGame();
            break;
        case "2":
            DisplayList();
            break;
        case "3":
            Console.WriteLine("Exiting the game. Goodbye!");
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void StartGame()
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    
    for (int i = 0; i < 5; i++)
    {
        int number1 = new Random().Next(1, 9);
        int number2 = new Random().Next(1, 9);
        MathOperation operation = (MathOperation)new Random().Next(0, 4);

        string operationSymbol = operation switch
        {
            MathOperation.Addition => "+",
            MathOperation.Subtraction => "-",
            MathOperation.Multiplication => "*",
            MathOperation.Division => "/",
            _ => throw new ArgumentOutOfRangeException()
        };

        Console.WriteLine($"Question {i + 1}: What is {number1} {operationSymbol} {number2}?");
        string? input = Console.ReadLine();
        if (CheckAnswer(number1, number2, operation, input))
        {
            points++;
        }
    }
    stopwatch.Stop();
    Console.WriteLine($"You scored {points} out of 5.");
    Console.WriteLine($"Time taken: {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
    history.Add($"Game played on {DateTime.Now}: Scored {points}/5, Time taken: {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
    PlayAgainPrompt();
}

void PlayAgainPrompt()
{
    Console.WriteLine("Do you want to return to the menu? (yes/no)");
    string? playAgain = Console.ReadLine();
    if (playAgain?.ToLower() == "yes")
    {
        points = 0;
        MathGame();
    }
    else if(playAgain?.ToLower() == "no")
    {
        Console.WriteLine("Thank you for playing!");
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid input, try again.");
        PlayAgainPrompt();
    }
}

void DisplayList()
{
    Console.WriteLine("History of previous games:");
    foreach (string item in history)
    {
        Console.WriteLine($"- {item}");
    }

    PlayAgainPrompt();
}

bool CheckAnswer(int number1, int number2, MathOperation operation, string? result)
{
    int correctAnswer = operation switch
    {
        MathOperation.Addition => number1 + number2,
        MathOperation.Subtraction => number1 - number2,
        MathOperation.Multiplication => number1 * number2,
        MathOperation.Division => number1 / number2,
        _ => throw new ArgumentException("Invalid operation")
    };

    if (int.TryParse(result, out int parsedValue) && parsedValue == correctAnswer)
    {
        return true;
    }

    return false;
}

public enum MathOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}