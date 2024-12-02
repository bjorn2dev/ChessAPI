using ChessAPI.Helpers;
using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Game;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Interfaces.Player;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.Numerics;

namespace ChessAPI.Services.Game
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
            _playerTurnService = playerTurnService;
            _pieceMovingService = pieceMovingService;
            _boardStateService = boardStateService;
            _pieceMoveValidator = pieceMoveValidator;
        }

        public void Move(string from, string to, User player)
        {
            var turn = _playerTurnService.CheckWhoseTurn();
            var fromTile = _boardStateService.Board.GetTileByAnnotation(from);
            var toTile = _boardStateService.Board.GetTileByAnnotation(to);
            if (fromTile == null || toTile == null || fromTile.piece == null)
                throw new InvalidOperationException("Invalid move");

            if (!_playerTurnService.IsValidTurn(player))
                throw new InvalidOperationException("It's not this player's turn");

            // Check if the move is legal and get the movement type
            var movementType = _pieceMoveValidator.ValidateMove(fromTile, toTile, _boardStateService.Board);
            if (movementType == MovementType.Invalid)
                throw new InvalidOperationException("Invalid move")

            // record turn
            var playerTurn = _playerTurnService.ConfigureTurn(fromTile, toTile, player);

            // Handle the move
            _pieceMovingService.MovePiece(fromTile, toTile, movementType, _boardStateService.Board);

            // Record turn in turn history
            _playerTurnService.RecordTurn(playerTurn);
        }
    }
}
