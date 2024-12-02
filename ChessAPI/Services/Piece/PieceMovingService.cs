using ChessAPI.Helpers;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Interfaces.Renderer;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
using ChessAPI.Models.Pieces;
using Microsoft.AspNetCore.Http;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services.Piece
{
    public class PieceMovingService : IPieceMovingService
    {
        private readonly ITileRenderer _tileRenderer;
        public PieceMovingService(ITileRenderer tileRenderer)
        {
            _tileRenderer = tileRenderer;
        }

        //TODO use caching instead of rendering tiles again
        public void MovePiece(Tile fromTile, Tile toTile, MovementType movementType, ChessBoard board)
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

                if (toTile.piece is King)
                {
                    ((King)toTile.piece).MarkAsMoved();
                }
                if (toTile.piece is Rook)
                {
                    ((Rook)toTile.piece).MarkAsMoved();
                }

                // Update HTML content
                toTile.html = _tileRenderer.Render(toTile);
                fromTile.html = _tileRenderer.Render(fromTile);
            }
        }

        private void HandleCastling(Tile kingTile, Tile kingDestination, MovementType castleDirection, ChessBoard board)
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
            kingDestination.html = _tileRenderer.Render(kingDestination);
            rookDestinationTile.html = _tileRenderer.Render(rookDestinationTile);
            kingTile.html = _tileRenderer.Render(kingTile);
            rookTile.html = _tileRenderer.Render(rookTile);
        }
    }
}
