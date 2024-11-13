using ChessAPI.Helpers;
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
        private readonly IBoardStateService _boardStateService;
        private readonly IPieceMoveValidator _pieceMoveValidator;
        public GameService(IPlayerService playerService, IGameGenerator gameGenerator, IPlayerTurnService playerTurnService, IPieceMovingService pieceMovingService, IBoardStateService boardStateService, IPieceMoveValidator pieceMoveValidator)
        {
            this._playerService = playerService;
            this._gameGenerator = gameGenerator;
            this._playerTurnService = playerTurnService;
            this._pieceMovingService = pieceMovingService;
            this._boardStateService = boardStateService;
            this._pieceMoveValidator = pieceMoveValidator;
        }

        public string InitializeGame()
        {
            if (this._playerService.PlayersInitialized)
            {
                this._gameGenerator.InitializeBoard();
                return this._gameGenerator.GetBoard(Color.PlayerColor.White);
            }
            else
            {
                return this.GetPlayerColorSelector();
            }
        }

        public void MovePiece(string from, string to, string userAgent, string userIpAddress)
        {
            var fromTile = TileHelper.GetTileByAnnotation(from, this._boardStateService.Board);
            var toTile = TileHelper.GetTileByAnnotation(to, this._boardStateService.Board);

            var player = this._playerService.GetPlayerByInfo(userAgent, userIpAddress);

            if (fromTile == null || toTile == null || fromTile.piece == null)
                throw new InvalidOperationException("Invalid move");

            if (!this._playerTurnService.IsValidTurn(this._playerService.PlayerTurns, player))
                throw new InvalidOperationException("It's not this player's turn");

            // check if the move is legal
            if (!this._pieceMoveValidator.ValidateMove(fromTile, toTile, this._boardStateService.Board))
                throw new InvalidOperationException("Invalid move");

            // record turn
            var playerTurn = this._playerTurnService.ConfigureTurn(fromTile, toTile, player);

            this._playerService.RecordTurn(playerTurn);

            this._pieceMovingService.MovePiece(fromTile, toTile, userAgent, userIpAddress);
        }

        public void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            this._playerService.ConfigurePlayer(playerColor, userAgent, userIp);
        }

        private string GetPlayerColorSelector()
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
