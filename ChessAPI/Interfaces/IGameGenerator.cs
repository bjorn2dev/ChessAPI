
using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces
{
    public interface IGameGenerator
    {
        void InitializeBoard();

        string GetBoard(Color.PlayerColor playerColorToShow);
        string ChooseColor(List<Color.PieceColor> pieceColorsToShow);
    }
}
