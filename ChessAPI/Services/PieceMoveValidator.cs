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
        private readonly IBoardSimulationService _boardSimulationService;
        public PieceMoveValidator(IBoardSimulationService boardSimulationService)
        {
            this._boardSimulationService = boardSimulationService;
        }

        public bool ValidateMove(Tile from, Tile to, Board board)
        {
            var fromPiece = from.piece;

            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);

            // every move is done with a piece and cant capture pieces of the same color
            if (fromPiece == null || (to.piece != null && fromPiece.color == to.piece.color) || movementType == MovementType.Invalid) return false;

            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            
            // verify it is a legal move or capture
            var isValidMove = movementType != MovementType.Capture ? fromPiece.IsValidMovement(from, to, board) : fromPiece.IsValidCapture(from, to, board);

            // check if the move is a move that sets the king in check
            isValidMove = !IsPlayingSideKingChecked(from, to, board);
             
            return isValidMove;
        }

        public bool IsPlayingSideKingChecked(Tile from, Tile to, Board board)
        {
            var simulatedBoard = this._boardSimulationService.SimulateMove(from, to, board);

            var kingTile = simulatedBoard.GetKingTile(from.piece.color == Color.PieceColor.White ? Color.PieceColor.White : Color.PieceColor.Black);
            King? kingPiece = kingTile.piece as King;
            return kingPiece != null && kingPiece.IsInCheck(simulatedBoard);
        }
    }
}
