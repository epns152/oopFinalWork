using FinalWorkConsole.Games;

namespace FinalWorkConsole.DAO;

public interface IDaoInterface
{
    Player.Player CreatePlayer(string name, string login, string pass);
    bool SaveGame(Game game);
    Player.Player Login(string login, string pass);
    Game[] GetAllGames(int playerId);
    Game CreateNewGame(int playerId, int hardLevel);
}