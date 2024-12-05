using ChessAPI.Helpers;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using System.Linq;
using System.Runtime.Intrinsics.Arm;

namespace ChessAPI.Services.Piece
{
    public class PieceMoveValidator : IPieceMoveValidator
    {
        private readonly IKingSafetyValidator _kingSafetyValidator;
        public PieceMoveValidator(IKingSafetyValidator kingSafetyValidator)
        {
            _kingSafetyValidator = kingSafetyValidator;
        }

        public MovementType ValidateMove(Tile from, Tile to, ChessBoard board, ChessPiece promoteTo = null)
        {
             var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            // Basic validation
            if (movementType == MovementType.Invalid ||
                from.piece == null || to.piece != null && from.piece.color == to.piece.color ||
                !IsMoveLegal(from, to, movementType, board, promoteTo))
            {
                return MovementType.Invalid;
            }

            return movementType;
        }

        private bool IsMoveLegal(Tile from, Tile to, MovementType movementType, ChessBoard board, ChessPiece promoteTo = null)
        {
            if(from.piece != null && to.piece != null && from.piece.color != to.piece.color)
            {
                movementType = MovementType.Capture;
            }
            
            if (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide)
            {
                // If we're castling we only want to know about the king safety and return.
                return _kingSafetyValidator.ValidateKingSafety(from, to, movementType, board);
            }

            if(movementType == MovementType.Promotion)
            {
                return ((Pawn)from.piece).CanPromote(from, to ,board, promoteTo);
            }

            // Ensure king is safe
            var kingTile = board.GetKingTile(from.piece.color);
            var kingSafety = _kingSafetyValidator.ValidateKingSafety(from, to, movementType, board);

            // Normal movement or capture validation
            var moveValidation = movementType != MovementType.Capture
                ? from.piece.IsValidMovement(from, to, board)
                : from.piece.IsValidCapture(from, to, board);

            return kingSafety && moveValidation;
        }


    }
}
