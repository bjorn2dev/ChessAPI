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
            var board = _boardGenerator.Board.playingFieldDictionary;

            var fromFileLetter = from.Substring(0, 1);
            var fromFile = TileHelper.ConvertLetterToFileNumber(fromFileLetter);
            var fromRank = Int32.Parse(from.Substring(1, 1));
            var fromTileEntry = board.FirstOrDefault((t)=> t.Key.Item1 == fromRank && t.Key.Item2 == fromFile);

            var toFileLetter = to.Substring(0, 1);
            var toFile = TileHelper.ConvertLetterToFileNumber(toFileLetter);
            var toRank = Int32.Parse(to.Substring(1, 1));
            var toTileEntry = board.FirstOrDefault((t) => t.Key.Item1 == toRank && t.Key.Item2 == toFile);

            // return if move isn't valid
            if (!_pieceMoveValidator.ValidateMove(fromTileEntry.Value, toTileEntry.Value)) return;

            //move piece to other tile
            toTileEntry.Value.piece = fromTileEntry.Value.piece;
            toTileEntry.Value.html = _tileRenderer.Render(toTileEntry.Value);

            //clean from tile
            fromTileEntry.Value.piece = null;
            fromTileEntry.Value.html = _tileRenderer.Render(fromTileEntry.Value);
        }
    }
}
