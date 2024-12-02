using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using System;
using System.Linq;
using System.Net;

namespace ChessAPI.Helpers
{
    public class MoveValidatorHelper
    {
        public MoveValidatorHelper()
        {

        }
      
        public static int[] GetMovementRange(MovementType movementType)
        {
            switch (movementType)
            {
                case MovementType.Diagonal:
                    return [7, 9];
                case MovementType.Horizontal:
                    return [1];
                case MovementType.Vertical:
                    return [8];
                case MovementType.LShaped:
                    return [5, 6, 10, 11, 15, 17];
                default:
                    return [];
            }
        }

        public static bool CheckTileRange(int[] pieceRange, Tile from, Tile to, ChessBoard board, bool isCapture)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var movementType = MoveValidatorHelper.DetermineMovementType(from, to, board);
            var differenceBetweenTiles = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);
            if (movementType == MovementType.Invalid) return false;
            // loop through every step in the piece range
            foreach (var step in pieceRange)
            {
                // check if the difference between tiles is module of the step making it a legal move
                if (differenceBetweenTiles % step == 0)
                {
                    // check if the path from the starting index to the finishing index is clear or obstructed by other pieces
                    // if it is return false.
                    if (!CheckPath(indexes.fromIndex, indexes.toIndex, step, board, movementType, isCapture))
                    {
                        return false;
                    }
                }
            }

            return pieceRange.Any() ? true : false;
        }

        public static bool CheckPath(int fromIndex, int toIndex, int step, ChessBoard board, MovementType movementType, bool isCapture)
        {
            Tile from = board.playingFieldDictionary.GetValueAtIndex(fromIndex);
            Tile to = board.playingFieldDictionary.GetValueAtIndex(toIndex);

            // early return on faulty movement
            if (movementType == MovementType.Invalid) return false;

            int currentIndex = fromIndex;
            while (currentIndex != toIndex)
            {
                currentIndex = toIndex > fromIndex ? currentIndex + Math.Abs(step) : currentIndex - Math.Abs(step);

                var tileCheck = board.playingFieldDictionary.GetValueAtIndex(currentIndex);

                // if we're on the tile we're going to move to and the movement type is capture we continue
                if (isCapture && currentIndex == toIndex)
                {
                    continue;
                }

                // check if movement type is not capture and the tile we're checking has no occupying piece,
                // also if the movement type is capture, but there we have not reached the destination tile, the path is blocked and we can return false.
                if (!isCapture && tileCheck.piece != null || (isCapture && tileCheck.piece != null && currentIndex != toIndex))
                {
                    return false;
                }

            }

            return true;  // Path is clear and allows movement or potential capture
        }

        public static (int fromIndex, int toIndex) GetMovementIndexes(Tile from, Tile to, ChessBoard board)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            return (fromIndex, toIndex);
        }

        public static int GetMovementDifference(int fromIndex, int toIndex)
        {
            return fromIndex > toIndex ? fromIndex - toIndex : toIndex - fromIndex;
        }

        public static MovementType DetermineMovementType(Tile from, Tile to, ChessBoard board)
        {
            // Get basic indexes and differences
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);

            // Early exit: no piece on 'from' tile
            if (from.piece == null) return MovementType.Invalid;

            // Check for captures
            bool isCapture = to.piece != null && from.piece.color != to.piece.color;

            // Determine applicable movement patterns
            var applicablePatterns = isCapture ? from.piece.capturePattern : from.piece.movePattern;

            switch (from.piece)
            {
                case King:
                    {  // Castle
                        if (from.rank == to.rank && to.piece == null)
                        {
                            var pieceColor = from.piece.color;
                            if ((pieceColor == Color.PieceColor.White && to.tileAnnotation == CastleHelper.WhiteKingSideCastleTileAnnotation) ||
                                (pieceColor == Color.PieceColor.Black && to.tileAnnotation == CastleHelper.BlackKingSideCastleTileAnnotation))
                            {
                                return MovementType.CastleKingSide;
                            }

                            if ((pieceColor == Color.PieceColor.White && to.tileAnnotation == CastleHelper.WhiteQueenSideCastleTileAnnotation) ||
                                (pieceColor == Color.PieceColor.Black && to.tileAnnotation == CastleHelper.BlackQueenSideCastleTileAnnotation))
                            {
                                return MovementType.CastleQueenSide;
                            }
                        }
                    }
                    break;
                case Pawn:
                    { // Promotion
                        var pieceColor = from.piece.color;
                        if ((pieceColor == Color.PieceColor.White && to.rank == 8) ||
                            (pieceColor == Color.PieceColor.Black && to.rank == 1 ))
                        {
                            return MovementType.Promotion;
                        }
                    }
                    break;
            }

            // Horizontal
            if (from.rank == to.rank && applicablePatterns.Contains(MovementType.Horizontal))
            {
                return MovementType.Horizontal;
            }

            // Vertical
            if (difference % 8 == 0 && applicablePatterns.Contains(MovementType.Vertical))
            {
                return MovementType.Vertical;
            }

            // Diagonal
            if (((Math.Abs(difference) % 9 == 0) || (Math.Abs(difference) % 7 == 0)) &&
                applicablePatterns.Contains(MovementType.Diagonal))
            {
                return MovementType.Diagonal;
            }

            // L-Shaped
            if (MoveValidatorHelper.GetMovementRange(MovementType.LShaped).Contains(difference) &&
                applicablePatterns.Contains(MovementType.LShaped))
            {
                return MovementType.LShaped;
            }

            // Default: Invalid movement
            return MovementType.Invalid;
        }


    }
}
