using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
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
            var difference = (fromPiece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex);

            // ^
            // 1 square up the board = 8 positions up or down the chess board field counting left from right.
            if (fromPiece is Pawn)
            {
                // if the difference is higher than 8 its an illegal move
                isValid = CheckTileRange([difference], difference == fromPiece.moveRadius, fromIndex, toIndex, difference) ;
            }

            // *
            // king can move in any way for 1 square
            int[] kingRange = [1, 7, 8, 9];
            if (fromPiece is King)
            {
                isValid = kingRange.Contains(difference);

                // king has multiple ways to move, but can always only move one square, so we make a new int array with the difference found.
                isValid = CheckTileRange([difference], isValid, fromIndex, toIndex, difference);
            }

            // + 
            // rook can move up down left and right to the end of the board. we check the difference for modulo 8, or if the rank is the same
            if (fromPiece is Rook)
            {
                // check with module if the rook has moved up 1 or more squares up or down 
                // or when moving left to right the rank (A3 -> H3) should be the same.
                isValid = (difference % 8 == 0 ? true : to.rank == from.rank);

                isValid = CheckTileRange([difference], isValid, fromIndex, toIndex, difference);
            }

            // /
            // bishop can move diagonally 
            int[] bishopRange = [7, 9];
            if (fromPiece is Bishop)
            {
                isValid = bishopRange.Any(b => difference % b == 0);

                isValid = CheckTileRange(bishopRange, isValid, fromIndex, toIndex, difference);
                
            }

            // *
            int[] queenRange = [1, 7, 8, 9];
            if (fromPiece is Queen)
            {
                isValid = queenRange.Any(q => difference % q == 0);

                isValid = CheckTileRange(queenRange, isValid, fromIndex, toIndex, difference);
            }

            // L
            int[] knightRange = [-5, -6, -10, -11, -15, -17, 5, 6, 10, 11, 15, 17];
            if (fromPiece is Knight)
            {
                isValid = knightRange.Contains(difference);
                isValid = CheckTileRange([difference], isValid, fromIndex, toIndex, difference);
            }

            return isValid && to.piece == null;
        }

        private bool CheckTileRange(int[] range, bool previousValid, int fromIndex, int toIndex, int difference)
        {
            if (!previousValid) return previousValid;

            var board = _boardGenerator.Board.playingFieldDictionary;
            var lastTileCheckedIndex = fromIndex;
            for (int i = 0; i < range.Length; i++)
            {
                var rangeHolder = range[i];
                if (difference % rangeHolder == 0)
                {
                    var isUp = difference > 0;
                    while (lastTileCheckedIndex != toIndex && (lastTileCheckedIndex - rangeHolder > 0))
                    {
                        lastTileCheckedIndex = isUp ? lastTileCheckedIndex + range[i] : lastTileCheckedIndex - range[i];
                        var tileCheck = board.GetValueAtIndex(lastTileCheckedIndex);
                        if (tileCheck.piece != null)
                        {
                            previousValid = false;
                            break;
                        }

                    }

                }
            }

            return previousValid;
        }
    }
}
