using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Numerics;

namespace ChessAPI.Services
{
    public class GameMoveValidator : IGameMoveValidator
    {

        private readonly IPlayerTurnService _playerTurnService;
        private readonly IPieceMovingService _pieceMovingService;
        private readonly IBoardStateService _boardStateService;
        private readonly IPieceMoveValidator _pieceMoveValidator;
        public GameMoveValidator(IPlayerTurnService playerTurnService,
            IPieceMovingService pieceMovingService,
            IBoardStateService boardStateService,
            IPieceMoveValidator pieceMoveValidator)
        {
            this._playerTurnService = playerTurnService;
            this._pieceMovingService = pieceMovingService;
            this._boardStateService = boardStateService;
            this._pieceMoveValidator = pieceMoveValidator;
        }

        public void Move(string from, string to, User player)
        {
            var turn = this._playerTurnService.CheckWhoseTurn();
            var fromTile = this._boardStateService.Board.GetTileByAnnotation(from);
            var toTile = this._boardStateService.Board.GetTileByAnnotation(to);
            if (fromTile == null || toTile == null || fromTile.piece == null)
                throw new InvalidOperationException("Invalid move");

            if (!this._playerTurnService.IsValidTurn(player))
                throw new InvalidOperationException("It's not this player's turn");

            // Check if the move is legal and get the movement type
            var movementType = this._pieceMoveValidator.ValidateMove(fromTile, toTile, this._boardStateService.Board);
            if (movementType == MovementType.Invalid)
                throw new InvalidOperationException("Invalid move");

            // record turn
            var playerTurn = this._playerTurnService.ConfigureTurn(fromTile, toTile, player);

            // Handle the move
            this._pieceMovingService.MovePiece(fromTile, toTile, movementType, this._boardStateService.Board);

            // Record turn in turn history
            this._playerTurnService.RecordTurn(playerTurn);
        }
    }
}
