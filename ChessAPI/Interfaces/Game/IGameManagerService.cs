namespace ChessAPI.Interfaces.Game
{
    public interface IGameManagerService
    {

        Guid CreateNewGame();
        IGameService GetGameById(Guid gameId);
        void EndGame(Guid gameId);
        string GetAllGames();
    }
}
