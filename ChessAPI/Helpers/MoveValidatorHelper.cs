using ChessAPI.Models;
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
        public static bool CheckTileRange(int[] pieceRange, Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var movementType = MoveValidatorHelper.GetMovementType(from, to, board);
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
                    if (!CheckPath(indexes.fromIndex, indexes.toIndex, step, board, movementType))
                    {
                        return false;
                    }
                }
            }

            return pieceRange.Any() ? true : false;
        }

        public static bool CheckPath(int fromIndex, int toIndex, int step, Board board, MovementType movementType)
        {
            Tile from = board.playingFieldDictionary.GetValueAtIndex(fromIndex);
            Tile to = board.playingFieldDictionary.GetValueAtIndex(toIndex);
            movementType = MoveValidatorHelper.CheckIfCapture(from, to, movementType) ? MovementType.Capture : movementType;

            // early return on faulty movement
            if (movementType == MovementType.Invalid) return false;

            int currentIndex = fromIndex;
            while (currentIndex != toIndex)
            {
                currentIndex = toIndex > fromIndex ? currentIndex + Math.Abs(step) : currentIndex - Math.Abs(step);

                var tileCheck = board.playingFieldDictionary.GetValueAtIndex(currentIndex);

                // if we're on the tile we're going to move to and the movement type is capture we continue
                if (movementType == MovementType.Capture && currentIndex == toIndex)
                {
                    continue;
                }
                // check if movement type is not capture and the tile we're checking has no occupying piece,
                // also if the movement type is capture, but there we have not reached the destination tile, the path is blocked and we can return false.
                if (movementType != MovementType.Capture && tileCheck.piece != null || (movementType == MovementType.Capture && tileCheck.piece != null && currentIndex != toIndex))
                {
                    return false;
                }

            }

            return true;  // Path is clear and allows movement or potential capture
        }

        public static (int fromIndex, int toIndex) GetMovementIndexes(Tile from, Tile to, Board board)
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

        public static MovementType GetMovementType(Tile from, Tile to, Board board)
        {
            var indexes = MoveValidatorHelper.GetMovementIndexes(from, to, board);
            var difference = MoveValidatorHelper.GetMovementDifference(indexes.fromIndex, indexes.toIndex);


            if (from.rank == to.rank && from.piece.movePattern.Contains(MovementType.Horizontal))
            {
                return MovementType.Horizontal;
            }
            else if (difference % 8 == 0 && from.piece.movePattern.Contains(MovementType.Vertical))
            {
                return MovementType.Vertical;
            }
            else if (
                ((Math.Abs(indexes.toIndex - indexes.fromIndex) % 9 == 0) || 
                (Math.Abs(indexes.toIndex - indexes.fromIndex) % 7 == 0) ) &&
                from.piece.movePattern.Contains(MovementType.Diagonal))
            {
                return MovementType.Diagonal;
            }
            else if (MoveValidatorHelper.GetMovementRange(MovementType.LShaped).Contains(difference) && from.piece.movePattern.Contains(MovementType.LShaped))
            {
                return MovementType.LShaped;
            }
            return MovementType.Invalid;
        }

        public static bool CheckIfCapture(Tile from, Tile to, MovementType originalMovementType) { 
            return from.piece != null && to.piece != null && from.piece.color != to.piece.color && from.piece.capturePattern.Contains(originalMovementType);
        } 
        
    }
}
