using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    public class PieceMovingService : IPieceMovingService
    {
        private readonly ITileRenderer _tileRenderer;
        public PieceMovingService(ITileRenderer tileRenderer)
        {
            this._tileRenderer = tileRenderer;
        }
        public void MovePiece(Tile fromTile, Tile toTile, MovementType movementType)
        {
            if (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide)
            {
                HandleCastling(fromTile.piece.color, movementType);
            }
            // update the tiles' HTML content
            // todo dont render but use caching
            toTile.piece = fromTile.piece;
            fromTile.piece = null;
            toTile.html = this._tileRenderer.Render(toTile);
            fromTile.html = this._tileRenderer.Render(fromTile);
        }

        private void HandleCastling(PieceColor playerColor, MovementType movementType)
        {
            // move king
            toTile.piece = fromTile.piece;
            fromTile.piece = null;
            // move rook

        }
    }
}
