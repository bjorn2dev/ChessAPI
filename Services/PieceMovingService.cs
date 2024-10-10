using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class PieceMovingService : IPieceMovingService
    {
        private readonly ITileRenderer _tileRenderer;
        private readonly IBoardGenerator _boardGenerator;
        private readonly IPieceMoveValidator _pieceMoveValidator;
        public PieceMovingService(ITileRenderer tileRenderer, IBoardGenerator boardGenerator, IPieceMoveValidator pieceMoveValidator)
        {
            _tileRenderer = tileRenderer;
            _boardGenerator = boardGenerator;
            _pieceMoveValidator = pieceMoveValidator;
        }
        public void MovePiece(string from, string to)
        {
            var fromFileLetter = from.Substring(0, 1);
            var fromFile = TileHelper.ConvertLetterToFileNumber(fromFileLetter);
            var fromRank = Int32.Parse(from.Substring(1, 1));

            Tile fromTile = null;

            foreach (var location in _boardGenerator.Board.playingFieldDictionary.Keys)
            {
                // find item by rank + file combination. this because the board generates from rank to file.
                if (location.Item1 == fromRank && location.Item2 == fromFile)
                {
                    fromTile = _boardGenerator.Board.playingFieldDictionary[location];
                }
            }
            var isValid = false;
            var toFileLetter = to.Substring(0, 1);
            var toFile = TileHelper.ConvertLetterToFileNumber(toFileLetter);
            var toRank = Int32.Parse(to.Substring(1, 1));

            Tile toTile = null;

            foreach (var location in _boardGenerator.Board.playingFieldDictionary.Keys)
            {
                if (location.Item1 == toRank && location.Item2 == toFile && fromTile != null)
                {
                    toTile = _boardGenerator.Board.playingFieldDictionary[location];

                   isValid = _pieceMoveValidator.ValidateMove(fromTile,toTile);
                    if (!isValid) { 
                        break;
                    }

                    toTile.piece = fromTile.piece;
                    toTile.html = _tileRenderer.Render(toTile);
                    _boardGenerator.Board.playingFieldDictionary[location] = toTile;
                }
            }
            if (!isValid)
            {
                return;
            }

            foreach (var location in _boardGenerator.Board.playingFieldDictionary.Keys)
            {
                // find item by rank + file combination. this because the board generates from rank to file.
                if (location.Item1 == fromRank && location.Item2 == fromFile)
                {
                    fromTile = _boardGenerator.Board.playingFieldDictionary[location];
                    fromTile.piece = null;
                    fromTile.html = _tileRenderer.Render(fromTile);
                    _boardGenerator.Board.playingFieldDictionary[location] = fromTile;
                }
            }
        }
    }
}
