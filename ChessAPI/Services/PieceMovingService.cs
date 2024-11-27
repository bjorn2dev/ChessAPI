using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using Microsoft.AspNetCore.Http;
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
        public void MovePiece(Tile fromTile, Tile toTile, MovementType movementType, Board board)
        {
            if (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide)
            {
                HandleCastling(fromTile.piece.color, movementType, board);
            }
            // update the tiles' HTML content
            // todo dont render but use caching
            toTile.piece = fromTile.piece;
            fromTile.piece = null;
            toTile.html = this._tileRenderer.Render(toTile);
            fromTile.html = this._tileRenderer.Render(fromTile);
        }

        private void HandleCastling(PieceColor playerColor, MovementType castleDirection, Board board)
        {
           var castleTiles = CastleHelper.GetCastleTiles(playerColor, castleDirection);
            var rookToTile = board.GetTileByAnnotation(castleTiles.RookFinalPositionTileAnnotation);
            var rookFromTile = board.GetTileByAnnotation(castleTiles.RookTileAnnotation);
            // move rook
            rookToTile.piece = rookFromTile.piece;
            rookFromTile.piece = null;
            rookToTile.html = this._tileRenderer.Render(rookToTile);
            rookFromTile.html = this._tileRenderer.Render(rookFromTile);
        }
    }
}
