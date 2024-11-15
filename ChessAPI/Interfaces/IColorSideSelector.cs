using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces
{
    public interface IColorSideSelector
    {
        string Render(List<Color.PieceColor> pieceColorsToShow);
    }
}
