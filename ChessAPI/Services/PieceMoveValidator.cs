﻿using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class PieceMoveValidator : IPieceMoveValidator
    {
        private readonly IBoardStateService _boardStateService;
        public PieceMoveValidator(IBoardStateService boardStateService)
        {
            _boardStateService = boardStateService;
        }

        public bool ValidateMove(Tile from, Tile to)
        {
            var fromPiece = from.piece;
            var board = _boardStateService.Board;
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);

            // every move is done with a piece and cant capture pieces of the same color
            if (fromPiece == null || (to.piece != null && fromPiece.color == to.piece.color) || movementType == MovementType.Invalid) return false;

            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            if (movementType != MovementType.Capture)
            {
                return fromPiece.IsValidMovement(from, to, board, movementType);
            }
            else
            {
                return fromPiece.IsValidCapture(from, to, board);
            }
        }
    }
}