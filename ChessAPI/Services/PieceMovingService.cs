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
            var fromTile = TileHelper.GetTileByAnnotation(from, _boardStateService.Board);
            var toTile = TileHelper.GetTileByAnnotation(to, _boardStateService.Board);

            // check if move is legal, 
            // return if move isn't valid
            if (_pieceMoveValidator.ValidateMove(fromTile, toTile))
            {
                _boardStateService.MovePiece(fromTile, toTile);
                toTile.html = _tileRenderer.Render(toTile);
                fromTile.html = _tileRenderer.Render(fromTile);
            }


        }
    }
}
