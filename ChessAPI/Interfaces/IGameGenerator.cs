namespace ChessAPI.Interfaces
{
    public interface IGameGenerator
    {
        void InitializeGame();

        string GetBoard();
    }
}
