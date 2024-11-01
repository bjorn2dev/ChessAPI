using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces
{
    public interface IColorSideSelector
    {
        string RenderColorSelector(List<Color.PieceColor> pieceColorsToShow);
    }
}
