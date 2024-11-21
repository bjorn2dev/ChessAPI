using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class PieceMovingService : IPieceMovingService
    {
        private readonly ITileRenderer _tileRenderer;
        public PieceMovingService(ITileRenderer tileRenderer)
        {
            this._tileRenderer = tileRenderer;
        }
        public void MovePiece(Tile fromTile, Tile toTile)
        {
            // move the piece
            toTile.piece = fromTile.piece;
            fromTile.piece = null;

            // update the tiles' HTML content
            // todo dont render 
            toTile.html = this._tileRenderer.Render(toTile);
            fromTile.html = this._tileRenderer.Render(fromTile);
        }
    }
}
