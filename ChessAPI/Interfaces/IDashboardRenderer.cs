using ChessAPI.Models.Enums;
namespace ChessAPI.Interfaces
{
    public interface IDashboardRenderer
    {
        string Render(List<Color.PlayerColor> pieceColorsToShow);
    }
}
