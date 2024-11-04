using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;
using static ChessAPI.Models.Enums.Color;
namespace ChessAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IColorSideSelector _colorSideSelector;
        private readonly IPlayerService _playerService;
        private readonly GameSettings _gameSettings;

        public GameService(IColorSideSelector colorSideSelector, IOptions<GameSettings> gameSettings, IPlayerService playerService)
        {
            this._gameSettings = gameSettings.Value;
            this._colorSideSelector = colorSideSelector;
            this._playerService = playerService;
            if (this._gameSettings.SkipColorSelection)
            {
                this._playerService.ConfigurePlayer(PlayerColor.White, this._gameSettings.SkipUserAgent, this._gameSettings.SkipUserIpAddress);
                this._playerService.ConfigurePlayer(PlayerColor.Black, this._gameSettings.SkipUserAgent, this._gameSettings.SkipUserIpAddress);
            }
        }


        public string GetColorSelector()
        {
            if (this._gameSettings.SkipColorSelection)
            {
                return string.Empty; // return empty string thus continuing to board
            }
            List<Color.PieceColor> pieceColorsToShow = new List<Color.PieceColor>();
            if (this._playerService.WhitePlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.White);
            }
            if (this._playerService.BlackPlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.Black);
            }
            return _colorSideSelector.RenderColorSelector(pieceColorsToShow);
        }

        public Color.PlayerColor ShowBoardForPlayerColor(string userAgent, string userIpAddress)
        {
            return this._playerService.GetPlayerByInfo(userAgent, userIpAddress).color;
        }

        public bool IsGameInitialized() => this._playerService.PlayersInitialized;
    }
}
