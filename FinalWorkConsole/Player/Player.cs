using FinalWorkConsole.Games;

namespace FinalWorkConsole.Player;

public class Player
{
    private string Name { get; }
    public int Id { get; }
    private static int _id = 10000;
    public readonly string Login;
    public readonly string Pass;

    public Player(string name)
    {
        this.Name = name;
        Id = _id++;
    }

    public Player(string name, string login, string pass)
    {
        Name = name;
        Login = login;
        Pass = pass;
        Id = _id++;
    }
    
    public void PlayGame(Game game)
    {
        Console.WriteLine("If you want to end and save the game type 'q' ");
        while (true)
        {
            game.PrintCurrentState();
            Console.Write("Enter coordinates x, y and digit to place in that format {x y digit}: ");
            var answer = Console.ReadLine() ?? throw new InvalidOperationException();
            if (answer.Equals("q"))
            {
                game.Save();
                return;
            }
            if (answer.Split(" ").Length == 3)
            {
                int x, y, digit;
                try
                {
                    var ints = answer.Split(" ");
                    x = Convert.ToInt32(ints[0]);
                    y = Convert.ToInt32(ints[1]);
                    digit = Convert.ToInt32(ints[2]);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Wrong format. Try again using only digits from 1 to 9 inclusively separated by spaces");
                    continue;
                }

                try
                {
                    if (!game.PutDigit(x, y, digit)) continue;
                    Console.WriteLine("Congratulations, You have solved Sudoku!");
                    game.Save();
                    return;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            else
            {
                Console.WriteLine("Unknown operation. Please, try again.");
            }
            
        }
    }
}