using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces.Renderer
{
    public interface IColorSideSelector
    {
        string Render(List<Color.PlayerColor> pieceColorsToShow);
    }
}
