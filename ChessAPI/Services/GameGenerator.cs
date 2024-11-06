using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.Options;

namespace ChessAPI.Services
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IBoardService _boardService;
        private readonly GameSettings _gameSettings;
        private readonly IColorSideSelector _colorSideSelector;
        public GameGenerator(IBoardService boardService, IOptions<GameSettings> gameSettings, IColorSideSelector colorSideSelector)
        {
            this._gameSettings = gameSettings.Value;
            this._boardService = boardService;
            this._colorSideSelector = colorSideSelector;
        }

        public string GetBoard(Color.PlayerColor playerColorToShow)
        {
            //if (!this._boardService.BoardInitialized)
            //{
            //    return this.ChooseColor(pieceColorsToShow);
            //}

            return _boardService.GetBoard(playerColorToShow);
        }

        public void InitializeBoard()
        {
            if (!this._boardService.BoardInitialized)
            {
                this._boardService.InitializeBoard();
            }
        }

        public string ChooseColor(List<Color.PieceColor> pieceColorsToShow)
        {
            return this._colorSideSelector.RenderColorSelector(pieceColorsToShow);

        }
    }
}
