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
        private readonly IPlayerTurnService _playerTurnService;
        public PieceMovingService(ITileRenderer tileRenderer, IPieceMoveValidator pieceMoveValidator, IBoardStateService boardStateService, IPlayerTurnService playerTurnService)
        {
            _tileRenderer = tileRenderer;
            _pieceMoveValidator = pieceMoveValidator;
            _boardStateService = boardStateService;
            _playerTurnService = playerTurnService;
        }
        public void MovePiece(string from, string to, string userAgent, string userIp)
        {
            var fromTile = TileHelper.GetTileByAnnotation(from, _boardStateService.Board);
            var toTile = TileHelper.GetTileByAnnotation(to, _boardStateService.Board);

            if (fromTile == null || toTile == null || fromTile.piece == null)
                throw new InvalidOperationException("Invalid move");

            if (!_playerTurnService.IsValidTurn(fromTile.piece.color, userAgent, userIp))
                throw new InvalidOperationException("It's not this player's turn");

            // check if the move is legal
            if (!_pieceMoveValidator.ValidateMove(fromTile, toTile, this._boardStateService.Board))
                throw new InvalidOperationException("Invalid move");

            // record turn
            _playerTurnService.RecordTurn(fromTile, toTile);

            // move the piece
            toTile.piece = fromTile.piece;
            fromTile.piece = null;

            // update the tiles' HTML content
            toTile.html = _tileRenderer.Render(toTile);
            fromTile.html = _tileRenderer.Render(fromTile);
        }
    }
}
