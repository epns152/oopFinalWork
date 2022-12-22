using FinalWorkConsole.Games;

namespace FinalWorkConsole.DAO;

public class DaoImplementation : IDaoInterface
{
    private readonly List<Player.Player> _players = new List<Player.Player>();
    private readonly List<Game> _games = new List<Game>();
    

    public DaoImplementation()
    {
        _players.Add(new Player.Player("Roman", "roman", "asdf"));
        _players.Add(new Player.Player("Sergiy", "sergiy", "qwerty"));
        _players.Add(new Player.Player("Ship", "asdf", "asf"));

        _games.Add(new Game(_players[1].Id, 80));
        _games.Add(new Game(_players[0].Id, 30));
        _games.Add(new Game(_players[2].Id, 35));
    }

    public Player.Player CreatePlayer(string name, string login, string pass)
    {
        if (_players.Any(p => p.Login.Equals(login)))
        {
            Console.WriteLine("User with that login already exists.");
            return null;
        }

        var p = new Player.Player(name, login, pass);
        _players.Add(p);
        return p;
    }

    public bool SaveGame(Game game)
    {
        return true;
    }

    public Player.Player Login(string login, string pass)
    {
        if (_players.Any(p => login.Equals(p.Login) && pass.Equals(p.Pass)))
        {
            return _players.Find(p => p.Login.Equals(login)) ?? throw new InvalidOperationException();
        }
        Console.WriteLine("Incorrect login or password");
        return null;
    }

    public Game[] GetAllGames(int playerId)
    {
        return _games.Where(g => g.PlayerId == playerId).ToArray();
    }

    public Game CreateNewGame(int playerId, int hardLevel)
    {
        var game = new Game(playerId, hardLevel);
        _games.Add(game);
        return game;
    }
}