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
    }
}
