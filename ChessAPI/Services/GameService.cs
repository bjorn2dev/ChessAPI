using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
namespace ChessAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IColorSideSelector _colorSideSelector;
        private readonly IPlayerTurnService _playerTurnService;
        public bool _playersInitialized;
        private readonly GameSettings _gameSettings;
 

        public GameService(IColorSideSelector colorSideSelector, IOptions<GameSettings> gameSettings, IPlayerTurnService playerTurnService)
        {
            this._gameSettings = gameSettings.Value;
            this._colorSideSelector = colorSideSelector;
            this._playerTurnService = playerTurnService;
            this._playersInitialized = this._gameSettings.SkipColorSelection ? true : false;
        }
        public string GetColorSelector()
        {
            if (this._playersInitialized)
            {
                return string.Empty; // return empty string thus continuing to board
            }
            List<Color.PieceColor> pieceColorsToShow = new List<Color.PieceColor>();
            if (this._playerTurnService.WhitePlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.White);
            }
            if (this._playerTurnService.BlackPlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.Black);
            }
            if (pieceColorsToShow.Count() == 0)
            {
                this._playersInitialized = true;
            }
            return _colorSideSelector.RenderColorSelector(pieceColorsToShow);
        }

        public void SetupPlayer(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            switch (playerColor)
            {
                case Color.PlayerColor.White:
                    this._playerTurnService.SetWhitePlayer(userAgent, userIp);
                    break;
                case Color.PlayerColor.Black:
                    this._playerTurnService.SetBlackPlayer(userAgent, userIp);
                    break;
            }
        }

        public bool IsGameInitialized() => this._playersInitialized;

    }
}
