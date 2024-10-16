using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using Newtonsoft.Json;
using System;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    public class PieceMoveValidator : IPieceMoveValidator
    {
        private readonly IBoardGenerator _boardGenerator;
        public PieceMoveValidator(IBoardGenerator boardGenerator)
        {
            _boardGenerator = boardGenerator;
        }

        public bool ValidateMove(Tile from, Tile to)
        {
            if (from.piece == null) { return false; }

            var isValid = false;
            var fromPiece = from.piece;

            var board = _boardGenerator.Board.playingFieldDictionary;

            var fromLocation = board.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.IndexOfKey(fromLocation.Key);

            var toLocation = board.FirstOrDefault(s => s.Value == to);
            var toIndex = board.IndexOfKey(toLocation.Key);

            // (d2 = 51th position in sortedlist) - (d3 = 43th position in list) = 8 steps = 1 step upward or -8 downward

            // To find the difference between two numbers, take the larger one and subtract the smaller one 
            var difference = fromIndex > toIndex ? fromIndex - toIndex : toIndex - fromIndex;

            // ^
            // 1 square up the board = 8 positions up or down the chess board field counting left from right.
            if (fromPiece is Pawn)
            {
                difference = fromPiece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;
                // if the difference is higher than 8 its an illegal move
                isValid = CheckTileRange([difference], difference == fromPiece.moveRadius, fromIndex, toIndex, difference);
            }

            // *
            // king can move in any way for 1 square
            if (fromPiece is King)
            {
                int[] kingRange = [-1, -7, -8, -9, 1, 7, 8, 9];
                // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
                //difference moet hier niet als differencebetween tiles 8 zijn als koning achteruit gaat, dit moet -8 zijn. dan -8 huidige positie die checken.
                isValid = CheckTileRange([difference], kingRange.Contains(difference), fromIndex, toIndex, fromIndex - toIndex);
            }

            // + 
            // rook can move up down left and right to the end of the board. we check the difference for modulo 8, or if the rank is the same
            if (fromPiece is Rook)
            {
                // check with module if the rook has moved up 1 or more squares up or down 
                // or when moving left to right the rank (A3 -> H3) should be the same.
                isValid = CheckTileRange([difference], (difference % 8 == 0 || to.rank == from.rank), fromIndex, toIndex, difference);
            }

            // /
            // bishop can move diagonally 
            if (fromPiece is Bishop)
            {
                int[] bishopRange = [-7, -9, 7, 9];
                isValid = CheckTileRange(bishopRange, bishopRange.Any(b => difference % b == 0), fromIndex, toIndex, difference);
            }

            // *
            if (fromPiece is Queen)
            {
                int[] queenRange = [-1, -7, -8, -9, 1, 7, 8, 9];
                if (difference > 1)
                {
                    if (to.rank == from.rank)
                    {
                        queenRange = [difference];

                    }
                    else if (difference > 9)
                    {
                        // exclude the short movement from the range of queen when moving difference above the maximum move amount
                        queenRange = [-7, -8, -9, 7, 8, 9];
                    }
                    else
                    {
                        foreach (int r in queenRange)
                        {
                            if (r == difference)
                            {
                                queenRange = [difference];
                                break;
                            }
                        }
                    }
                }
                isValid = CheckTileRange(queenRange, queenRange.Any(q => difference % q == 0), fromIndex, toIndex, difference);
            }

            // L
            if (fromPiece is Knight)
            {
                int[] knightRange = [-5, -6, -10, -11, -15, -17, 5, 6, 10, 11, 15, 17];
                isValid = CheckTileRange([difference], knightRange.Contains(difference), fromIndex, toIndex, difference);
            }

            return isValid && to.piece == null;
        }

        private bool CheckTileRange(int[] pieceRange, bool previousValid, int fromIndex, int toIndex, int differenceBetweenTiles)
        {
            if (!previousValid) return previousValid;

            var board = _boardGenerator.Board.playingFieldDictionary;
            var lastTileCheckedIndex = fromIndex;
            var fromTile = board.GetValueAtIndex(fromIndex);
            var toTile = board.GetValueAtIndex(toIndex);
            // loop through the provided piece range
            for (int i = 0; i < pieceRange.Length; i++)
            {
                var pieceRangeHolder = pieceRange[i];
                // check if the difference between tiles is modulo of the range item
                if (differenceBetweenTiles % pieceRangeHolder == 0)
                {

                    // checking moving sideways for rook and queen, others cant move this way.
                    if ((fromTile.piece is Queen || fromTile.piece is Rook) && (fromTile.rank == toTile.rank))
                    {
                        while (lastTileCheckedIndex != toIndex)
                        {
                            lastTileCheckedIndex = toIndex > fromIndex ? lastTileCheckedIndex + 1 : lastTileCheckedIndex - 1;
                            var tileCheck = board.GetValueAtIndex(lastTileCheckedIndex);
                            if (tileCheck.piece != null)
                            {
                                previousValid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // check if the last checked tile isn't the toIndex.  
                        // check if the lastTileCheckedIndex - pieceRangeHolder is within the bounds of the board list
                        while (lastTileCheckedIndex != toIndex && (lastTileCheckedIndex - pieceRangeHolder >= 0))
                        {
                            lastTileCheckedIndex = toIndex > fromIndex ? lastTileCheckedIndex + Math.Abs(pieceRange[i]) : lastTileCheckedIndex - Math.Abs(pieceRange[i]);

                            var tileCheck = board.GetValueAtIndex(lastTileCheckedIndex);
                            if (tileCheck.piece != null)
                            {
                                previousValid = false;
                                break;
                            }
                        }
                    }

                }
            }

            return previousValid;
        }
    }
}
