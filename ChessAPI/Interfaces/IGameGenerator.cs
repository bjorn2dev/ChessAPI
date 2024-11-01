namespace ChessAPI.Interfaces
{
    public interface IGameGenerator
    {
        void InitializeBoard();

        string GetBoard(string userAgent, string userIpAddress);

        string GetBoardAsJson();

        void RenderBoardFromJson(string jsonBoardString);
    }
}
