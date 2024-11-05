namespace ChessAPI.Interfaces
{
    public interface IGameManagerService
    {

        Guid CreateNewGame();
        IGameService GetGameById(Guid gameId);
        void EndGame(Guid gameId);
    }
}
