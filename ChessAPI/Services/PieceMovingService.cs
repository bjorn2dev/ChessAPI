using ChessAPI.Helpers;
using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
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

        //TODO use caching instead of rendering tiles again
        public void MovePiece(Tile fromTile, Tile toTile, MovementType movementType, Board board)
        {
            if (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide)
            {
                HandleCastling(fromTile, toTile, movementType, board);
            }
            else
            {
                // Normal move
                toTile.piece = fromTile.piece;
                fromTile.piece = null;

                if(toTile.piece is King)
                {
                    ((King)toTile.piece).MarkAsMoved();
                }
                if (toTile.piece is Rook)
                {
                    ((Rook)toTile.piece).MarkAsMoved();
                }

                // Update HTML content
                toTile.html = this._tileRenderer.Render(toTile);
                fromTile.html = this._tileRenderer.Render(fromTile);
            }
        }

        private void HandleCastling(Tile kingTile, Tile kingDestination, MovementType castleDirection, Board board)
        {
            var castleTiles = CastleHelper.GetCastleTiles(kingTile.piece.color, castleDirection);

            // Move the king
            kingDestination.piece = kingTile.piece;
            kingTile.piece = null;

            // Move the rook
            var rookTile = board.GetTileByAnnotation(castleTiles.RookTileAnnotation);
            var rookDestinationTile = board.GetTileByAnnotation(castleTiles.RookFinalPositionTileAnnotation);

            rookDestinationTile.piece = rookTile.piece;
            rookTile.piece = null;

            // Update HTML content for both pieces
            kingDestination.html = this._tileRenderer.Render(kingDestination);
            rookDestinationTile.html = this._tileRenderer.Render(rookDestinationTile);
            kingTile.html = this._tileRenderer.Render(kingTile);
            rookTile.html = this._tileRenderer.Render(rookTile);
        }
    }
}
