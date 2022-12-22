using FinalWorkConsole.DAO;
using FinalWorkConsole.Games;

namespace FinalWorkConsole;

public class MainController
{
    private readonly IDaoInterface _dao;
    private Player.Player _player;
    private Game _currentGame;

    public MainController()
    {
        _dao = new DaoImplementation();
    }
    
    private Player.Player Login(string login, string pass)
    {
        return _dao.Login(login, pass);
    }

    private Player.Player Register(string name, string login, string pass)
    {
        return _dao.CreatePlayer(name, login, pass);
    }

    private bool CreateNewGame(int hardLevel)
    {
        _currentGame = _dao.CreateNewGame(_player.Id, hardLevel);
        _player.PlayGame(_currentGame);
        return true;
    }

    private bool StartChosenGame(int id)
    {
        var games = _dao.GetAllGames(_player.Id);
        foreach (var t in games)
        {
            if (t.Id != id) continue;
            _currentGame = t;
            _player.PlayGame(_currentGame);
            return true;
        }
        return false;
    }

    private bool ShowAllGames()
    {
        var games = _dao.GetAllGames(_player.Id);
        foreach (var game in games)
        {
            Console.WriteLine("Game id: " + game.Id);
            game.PrintCurrentState();
        }

        return true;
    }

    private void Action(int action)
    {
        var login = "";
        var pass = "";
        var name = "";
        switch (action)
        {
            case 1:
                Console.WriteLine("Enter your login:");
                login = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                pass = Console.ReadLine();

                _player = Login(login, pass);
                break;
            case 2:
                Console.WriteLine("Enter your name:");
                name = Console.ReadLine();
                Console.WriteLine("Enter your login:");
                login = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                pass = Console.ReadLine();

                _player = Register(name, login, pass);
                break;
            case 3:
                ShowAllGames();
                break;
            case 4:
                Console.WriteLine("Choose hard level (from 25 to 81): ");
                var hardLevel = Convert.ToInt32(Console.ReadLine());
                CreateNewGame(hardLevel);
                break;
            case 5:
                Console.WriteLine("Enter game id:");
                var gameId = Convert.ToInt32(Console.ReadLine());
                StartChosenGame(gameId);
                break;
            default:
                Console.WriteLine("Unknown action. Please, try again using digits 1-3 inclusively.");
                break;
        }
    }
    
    public void Run()
    {
        while (true)
        {
            if (_player == null)
            {
                Console.WriteLine("Choose one of the actions:\n" +
                                  "1 - login\n" +
                                  "2 - register\n" +
                                  "q - quit");
            }
            else
            {
                Console.WriteLine("Choose one of the actions:\n" +
                                  "1 - show all your games\n" +
                                  "2 - create new game\n" +
                                  "3 - play chosen game\n" +
                                  "q - quit");
            }
            
            var action = Console.ReadLine();
            if (action is "q")
            {
                return;
            }

            try
            {
                var n = Convert.ToInt32(action);
                Action(_player == null ? n : n + 2);
            }
            catch (Exception e)
            {
                Console.WriteLine("Incorrect format, try again.");
            }
        }
    }
}