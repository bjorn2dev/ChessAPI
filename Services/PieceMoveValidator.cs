using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;

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
            var isValid = false;
            var fromPiece = from.piece;
            
            var board = _boardGenerator.Board.playingFieldDictionary;


            var fromLocation = board.FirstOrDefault((s) => s.Value == from);
            var fromIndex = board.IndexOfKey(fromLocation.Key);

            var toLocation = board.FirstOrDefault(s => s.Value == to);
            var toIndex = board.IndexOfKey(toLocation.Key);

            var pattern = "";
            var moveRadiusPawn = 8;
            // To find the difference between two numbers, take the larger one and subtract the smaller one 
            var difference = 0;


            if (fromPiece is Pawn)
            {
                // (d2 = 51) - (d3 = 43) = 8
                difference = (fromPiece.color == Models.Enums.Color.PieceColor.White ? toIndex - fromIndex : fromIndex - toIndex);
                // 1 square up the board = 8 positions up or down the chess board field counting left from right.
                // if the difference is higher than 8 its an illegal move
                isValid = difference == fromPiece.moveRadius && to.piece == null;
            }





            return isValid;
        }
    }
}
