using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces
{
    public interface IGameRenderingService
    {
        string RenderColorSelector(List<Color.PlayerColor> showColor);
        string RenderBoard();
    }
}
