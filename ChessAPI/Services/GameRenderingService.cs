using ChessAPI.Interfaces;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services
{
    public class GameRenderingService : IGameRenderingService
    {
        private readonly IBoardService _boardService;
        private readonly IDashboardRenderer _colorSideSelector;
        public GameRenderingService(IBoardService boardService, IDashboardRenderer colorSideSelector)
        {
            _boardService = boardService;
            _colorSideSelector = colorSideSelector;
        }

        public string RenderColorSelector(List<Color.PlayerColor> showColor)
        {
            return this._colorSideSelector.Render(showColor);
        }

        public string RenderBoard()
        {
            return this._boardService.GetBoard();
        }
    }
}
