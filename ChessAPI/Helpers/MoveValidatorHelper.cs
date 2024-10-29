using ChessAPI.Models;

namespace ChessAPI.Helpers
{
    public class MoveValidatorHelper
    {
        public MoveValidatorHelper()
        {
            
        }

        public static bool CheckTileRange(int[] pieceRange, int fromIndex, int toIndex, int differenceBetweenTiles, Board board, MovementType movementType)
        {
            // loop through every step in the piece range
            foreach (var step in pieceRange)
            {
                // check if the difference between tiles is module of the step making it a legal move
                if (differenceBetweenTiles % step == 0)
                {
                    // check if the path from the starting index to the finishing index is clear or obstructed by other pieces
                    // if it is return false.
                    if (!CheckPath(fromIndex, toIndex, step, board, movementType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool CheckPath(int fromIndex, int toIndex, int step, Board board, MovementType movementType)
        {
            int currentIndex = fromIndex;
            while (currentIndex != toIndex //&&(toIndex > fromIndex ?(currentIndex + step >= 0) :(currentIndex - step >= 0))
                )
            {
                currentIndex = toIndex > fromIndex ? currentIndex + Math.Abs(step) : currentIndex - Math.Abs(step);

                var tileCheck = board.playingFieldDictionary.GetValueAtIndex(currentIndex);

                if (movementType == MovementType.Capture && currentIndex == toIndex)
                {
                    // continue along, returning true 
                    continue;
                }

                if (movementType != MovementType.Capture && tileCheck.piece != null || (movementType == MovementType.Capture && tileCheck.piece != null && currentIndex != toIndex))
                {
                    return false;
                }

            }

            return true;  // Path is clear and allows movement or potential capture
        }

        public static MovementType GetMovementType(Tile from, Tile to, Board board)
        {
            var fromLocation = board.playingFieldDictionary.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.playingFieldDictionary.IndexOfKey(fromLocation.Key);

            var toLocation = board.playingFieldDictionary.FirstOrDefault(s => s.Value == to);
            var toIndex = board.playingFieldDictionary.IndexOfKey(toLocation.Key);

            var difference = fromIndex > toIndex ? fromIndex - toIndex : toIndex - fromIndex;
            int[] lShapeRange = [5, 6, 10, 11, 15, 17];
            if (from.rank == to.rank)
            {
                return MovementType.Horizontal;
            }
            else if (difference % 8 == 0)
            {
                return MovementType.Vertical;
            }
            else if ((Math.Abs(toIndex - fromIndex) % 9 == 0) || (Math.Abs(toIndex - fromIndex) % 7 == 0))
            {
                return MovementType.Diagonal;
            }
            else if (lShapeRange.Contains(difference))
            {
                return MovementType.LShaped;
            }
            return MovementType.Invalid;
        }
    }
}
