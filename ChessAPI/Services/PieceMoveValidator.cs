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

        public MovementType ValidateMove(Tile from, Tile to, Board board)
        {
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            // Basic validation
            if (movementType == MovementType.Invalid ||
                from.piece == null || (to.piece != null && from.piece.color == to.piece.color) ||
                !IsMoveLegal(from, to, movementType, board))
            {
                return MovementType.Invalid;
            }

            return movementType;
        }

        private bool IsMoveLegal(Tile from, Tile to, MovementType movementType, Board board)
        {
            if (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide)
            {
                return this._kingSafetyValidator.ValidateKingSafety(from, to, movementType, board);
            }

            // Normal movement or capture validation
            return movementType != MovementType.Capture
                ? from.piece.IsValidMovement(from, to, board)
                : from.piece.IsValidCapture(from, to, board);
        }


    }
}
