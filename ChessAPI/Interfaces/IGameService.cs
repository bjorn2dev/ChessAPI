using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IGameService
    {
        string GetColorSelector();
        void SetupPlayer(Color.PlayerColor playerColor);
        bool IsBoardInitialized();
    }
}
