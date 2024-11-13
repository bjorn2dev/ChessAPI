using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.Extensions.Options;
namespace ChessAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IPlayerService _playerService;
        private readonly IGameGenerator _gameGenerator;
        private readonly IPlayerTurnService _playerTurnService;
        private readonly IPieceMovingService _pieceMovingService;
        public GameService(IPlayerService playerService, IGameGenerator gameGenerator, IPlayerTurnService playerTurnService, IPieceMovingService pieceMovingService)
        {
            this._playerService = playerService;
            this._gameGenerator = gameGenerator;
            this._playerTurnService = playerTurnService;
            this._pieceMovingService = pieceMovingService;
        }

        public string InitializeGame()
        {
            if (this._playerService.PlayersInitialized)
            {
                this._gameGenerator.InitializeBoard();
                return this._gameGenerator.GetBoard(this._playerTurnService.CheckWhoseTurn());
            }
            else
            {
                return this.GetColorSelector();
            }
        }

        public void MovePiece(string from, string to, string userAgent, string userIpAddress)
        {
            this._pieceMovingService.MovePiece(from, to, userAgent, userIpAddress);
        }

        public void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            this._playerService.ConfigurePlayer(playerColor, userAgent, userIp);
        }

        private string GetColorSelector()
        {
            List<Color.PieceColor> pieceColorsToShow = new List<Color.PieceColor>();
            if (this._playerService.WhitePlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.White);
            }
            if (this._playerService.BlackPlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.Black);
            }
            return this._gameGenerator.ChooseColor(pieceColorsToShow);
        }
    }
}
