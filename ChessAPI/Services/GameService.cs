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

        private readonly IGameInitializationService _gameInitializationService;
      //  private readonly IPieceMovementService _pieceMovementService;
        private readonly IPlayerManagementService _playerManagementService;
        private readonly IGameRenderingService _gameRenderingService;
        public GameService(IPlayerService playerService, IGameGenerator gameGenerator, IPlayerTurnService playerTurnService, IPieceMovingService pieceMovingService, IBoardStateService boardStateService, IPieceMoveValidator pieceMoveValidator)
        {
            this._playerService = playerService;
            this._gameGenerator = gameGenerator;
            this._playerTurnService = playerTurnService;
            this._pieceMovingService = pieceMovingService;
            this._boardStateService = boardStateService;
            this._pieceMoveValidator = pieceMoveValidator;
        }

        public void MovePiece(string from, string to, string userAgent, string userIpAddress)
        {

            var player = this._playerManagementService.GetPlayerByInfo(userAgent, userIpAddress);

            var fromTile = TileHelper.GetTileByAnnotation(from, this._boardStateService.Board);
            var toTile = TileHelper.GetTileByAnnotation(to, this._boardStateService.Board);

            if (fromTile == null || toTile == null || fromTile.piece == null)
                throw new InvalidOperationException("Invalid move");

            if (!this._playerTurnService.IsValidTurn(player))
                throw new InvalidOperationException("It's not this player's turn");

            // check if the move is legal
            if (!this._pieceMoveValidator.ValidateMove(fromTile, toTile, this._boardStateService.Board))
                throw new InvalidOperationException("Invalid move");

            // record turn
            var playerTurn = this._playerTurnService.ConfigureTurn(fromTile, toTile, player);

            this._pieceMovingService.MovePiece(fromTile, toTile);
            this._playerTurnService.RecordTurn(playerTurn);
        }

        public void RegisterPlayerColor(Color.PlayerColor playerColor, string userAgent, string userIp)
        {
            _playerManagementService.RegisterPlayerColor(playerColor, userAgent, userIp);
        }

        public string StartGame()
        {
            if (!this._playerManagementService.ArePlayersRegistered())
            {
                return _gameRenderingService.RenderColorSelector();
            } 
            else
            {
                this._gameInitializationService.InitializeGame();
                return _gameRenderingService.RenderBoard();
            }
        }
    }
}
