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
        private MovementType _movementType;
        private bool _isCapture = false;
        public PieceMoveValidator(IBoardGenerator boardGenerator)
        {
            _boardGenerator = boardGenerator;
        }

        public bool ValidateMove(Tile from, Tile to)
        {
            if (from.piece == null || (to.piece != null && from.piece.color == to.piece.color) || from.tileAnnotation == to.tileAnnotation) { return false; }

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

            this.SetMovementType(from, to, fromIndex, toIndex, difference);

            // ^
            // 1 square up the board = 8 positions up or down the chess board field counting left from right.
            if (fromPiece is Pawn)
            {
                int[] pawnRange;
                if (_isCapture)
                {
                    pawnRange = fromPiece.color == PieceColor.White ? [7, 9] : [-7, -9];
                } else
                {
                    difference = fromPiece.color == PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex;
                    pawnRange = [difference];
                }
                
                // if the difference is higher than 8 its an illegal move
                isValid = CheckTileRange(pawnRange, !_isCapture && difference == fromPiece.moveRadius || _isCapture && _movementType == MovementType.Diagonal, fromIndex, toIndex, difference);
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
                int[] rookRange = _movementType == MovementType.Horizontal ? [1] : [8];

                // check with module if the rook has moved up 1 or more squares up or down 
                // or when moving left to right the rank (A3 -> H3) should be the same.
                isValid = CheckTileRange(rookRange,
                    _movementType == MovementType.Horizontal || _movementType == MovementType.Vertical,
                    fromIndex,
                    toIndex,
                    difference);
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
                int[] queenRange = [-1, -7, -8, -9, 1, 7, 8, 9]; // Queen moves in all directions

                // we only need to check the blocks up and down 
                // we only need to check the blocks exactly next to us until the to position has been reached.
                queenRange = difference > 9 ? [-7, -8, -9, 7, 8, 9] : _movementType == MovementType.Horizontal ? [1] : [8];

                isValid = CheckTileRange(queenRange,
                    queenRange.Any(q => difference % q == 0),
                    fromIndex,
                    toIndex,
                    difference);
            }

            // L
            if (fromPiece is Knight)
            {
                int[] knightRange = [-5, -6, -10, -11, -15, -17, 5, 6, 10, 11, 15, 17];
                isValid = CheckTileRange([difference], knightRange.Contains(difference), fromIndex, toIndex, difference);
            }

            return isValid && to.piece == null;
        }

        private enum MovementType
        {
            Other, // todo fix this
            Diagonal,
            Horizontal,
            Vertical
        }

        private bool CheckTileRange(int[] pieceRange, bool previousValid, int fromIndex, int toIndex, int differenceBetweenTiles)
        {
            if (!previousValid) return previousValid;

            var board = _boardGenerator.Board.playingFieldDictionary;
            var lastTileCheckedIndex = fromIndex;
            for (int i = 0; i < pieceRange.Length; i++)
            {
                var pieceRangeHolder = pieceRange[i];

                // check if the difference between tiles is modulo of the range item
                if (differenceBetweenTiles % pieceRangeHolder == 0)
                {
                    if (!CheckPath(fromIndex, toIndex, pieceRangeHolder))
                    {
                        return false;
                    }
                }
            }

            return previousValid;
        }

        private bool CheckPath(int fromIndex, int toIndex, int step)
        {
            var board = _boardGenerator.Board.playingFieldDictionary;
            int currentIndex = fromIndex;
            while (currentIndex != toIndex &&
                        (toIndex > fromIndex ?
                         (currentIndex + step >= 0) :
                         (currentIndex - step >= 0)
                    ))
            {
                currentIndex = toIndex > fromIndex ? currentIndex + Math.Abs(step) : currentIndex - Math.Abs(step);

                var tileCheck = board.GetValueAtIndex(currentIndex);

                if (_isCapture && currentIndex == toIndex)
                {
                    // continue along, returning true 
                    continue;
                }

                if (!_isCapture && tileCheck.piece != null || (_isCapture && tileCheck.piece != null && currentIndex != toIndex))
                { 
                    return false;
                }
                
            }

            return true;  // Path is clear
        }

        private void SetMovementType(Tile from, Tile to, int fromIndex, int toIndex, int difference)
        {
            bool isDiagonal = (Math.Abs(toIndex - fromIndex) % 9 == 0) || (Math.Abs(toIndex - fromIndex) % 7 == 0);
            bool isVertical = difference % 8 == 0;
            bool isHorizontal = from.rank == to.rank;

            if (to.piece != null)
            {
                _isCapture = true;
            }
            if (from.piece is Pawn && !_isCapture)
            {
                _movementType = MovementType.Other;
            }
            else if (isHorizontal)
            {
                _movementType = MovementType.Horizontal;
            }
            else if (isVertical)
            {
                _movementType = MovementType.Vertical;
            }
            else if (isDiagonal)
            {
                _movementType = MovementType.Diagonal;
            }
        }
    }
}
