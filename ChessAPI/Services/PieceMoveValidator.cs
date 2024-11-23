using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace ChessAPI.Services
{
    public class PieceMoveValidator : IPieceMoveValidator
    {
        private readonly IKingSafetyValidator _kingSafetyValidator;
        public PieceMoveValidator(IKingSafetyValidator kingSafetyValidator)
        {
            this._kingSafetyValidator = kingSafetyValidator;
        }

        public bool ValidateMove(Tile from, Tile to, Board board)
        {
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            // Basic validation: piece existence, movement type, and color mismatch for captures and king safetey
            if (movementType == MovementType.Invalid ||
                from.piece == null || (to.piece != null && from.piece.color == to.piece.color) ||
                !this._kingSafetyValidator.ValidateKingSafety(from, to, movementType, board)) return false;

            // Validate if the move leaves the king in check
            return this.ValidateBasicMove(from, to, movementType, board);
        }

        private bool ValidateBasicMove(Tile from, Tile to, MovementType movementType, Board board)
        {
            if (from.piece == null || (to.piece != null && from.piece.color == to.piece.color)) return false;
            return movementType != MovementType.Capture
                ? from.piece.IsValidMovement(from, to, board)  // Basic movement validation
                : from.piece.IsValidCapture(from, to, board);  // Capture validation
        }

        
    }
}
