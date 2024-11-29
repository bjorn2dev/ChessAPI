using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces.Game
{
    public interface IGameGenerator
    {
        void InitializeBoard();

        string GetBoard(Color.PlayerColor playerColorToShow);
    }
}
