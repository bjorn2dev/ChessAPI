using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Game
{
    public interface IGameRenderingService
    {
        string RenderColorSelector(List<Color.PlayerColor> showColor);
        string RenderBoard();
    }
}
