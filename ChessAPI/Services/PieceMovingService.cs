using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class PieceMovingService : IPieceMovingService
    {
        private readonly ITileRenderer _tileRenderer;
        private readonly IPieceMoveValidator _pieceMoveValidator;
        private readonly IBoardStateService _boardStateService;
        public PieceMovingService(ITileRenderer tileRenderer, IPieceMoveValidator pieceMoveValidator, IBoardStateService boardStateService)
        {
            _tileRenderer = tileRenderer;
            _pieceMoveValidator = pieceMoveValidator;
            _boardStateService = boardStateService;
        }
        public void MovePiece(string from, string to)
        {
            var board = _boardStateService.Board.playingFieldDictionary;

            var fromFileLetter = from.Substring(0, 1);
            var fromFile = TileHelper.ConvertLetterToFileNumber(fromFileLetter);
            var fromRank = Int32.Parse(from.Substring(1, 1));
            var fromTileEntry = board.FirstOrDefault((t) => t.Key.Item1 == fromRank && t.Key.Item2 == fromFile);

            var toFileLetter = to.Substring(0, 1);
            var toFile = TileHelper.ConvertLetterToFileNumber(toFileLetter);
            var toRank = Int32.Parse(to.Substring(1, 1));
            var toTileEntry = board.FirstOrDefault((t) => t.Key.Item1 == toRank && t.Key.Item2 == toFile);

            // check if move is legal, 
            // return if move isn't valid
            if (_pieceMoveValidator.ValidateMove(fromTileEntry.Value, toTileEntry.Value))
            {
                _boardStateService.MovePiece(fromTileEntry.Value, toTileEntry.Value);
                toTileEntry.Value.html = _tileRenderer.Render(toTileEntry.Value);
                fromTileEntry.Value.html = _tileRenderer.Render(fromTileEntry.Value);
            }


        }
    }
}
