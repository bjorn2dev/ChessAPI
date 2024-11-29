using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Game;
using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services.Game
{
    public class GameRenderingService : IGameRenderingService
    {
        private readonly IBoardService _boardService;
        private readonly IColorSideSelector _colorSideSelector;
        public GameRenderingService(IBoardService boardService, IColorSideSelector colorSideSelector)
        {
            _boardService = boardService;
            _colorSideSelector = colorSideSelector;
        }

        public string RenderColorSelector(List<Color.PlayerColor> showColor)
        {
            return _colorSideSelector.Render(showColor);
        }

        public string RenderBoard()
        {
            return _boardService.GetBoard();
        }
    }
}
