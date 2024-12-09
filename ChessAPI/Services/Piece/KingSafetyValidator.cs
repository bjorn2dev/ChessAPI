﻿using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;
using ChessAPI.Helpers;
using ChessAPI.Interfaces.Board;
using ChessAPI.Interfaces.Piece;
using ChessAPI.Models.Enums;

namespace ChessAPI.Services.Piece
{
    public class KingSafetyValidator : IKingSafetyValidator
    {

        private readonly IBoardSimulationService _boardSimulationService;
        public KingSafetyValidator(IBoardSimulationService boardSimulationService)
        {
            _boardSimulationService = boardSimulationService;
        }

        public bool ValidateKingSafety(Tile from, Tile to, MovementType movementType, ChessBoard originalBoard)
        {
            var kingTile = from.piece is King ? from : originalBoard.GetKingTile(from.piece.color);
            // Check if king ends up in check after the move
            var simulatedBoard = _boardSimulationService.SimulateMove(from, to, originalBoard);
            var kingChecked = ((King)kingTile.piece).IsInCheck(simulatedBoard);

            // If it's castling, perform additional checks
            if (!kingChecked && (movementType == MovementType.CastleKingSide || movementType == MovementType.CastleQueenSide))
            {
                return ValidateCastleMovement(kingTile.piece.color, movementType, originalBoard);
            }

            // Return true if king is not in check
            return !kingChecked;
        }

        public bool ValidateKingTileSafety(Tile checkTile, ChessBoard board)
        {
            var opponentSideColor = checkTile.piece.color == PieceColor.White ? PieceColor.Black : PieceColor.White;
            var oppositeSidePieceTiles = board.playingFieldDictionary.Select((x) => x.Value).Where((x) => x.piece != null && x.piece.color == opponentSideColor).ToList();

            foreach (var oppositeSidePieceTile in oppositeSidePieceTiles)
            {
                if (oppositeSidePieceTile.piece.IsCheckingKing(oppositeSidePieceTile, checkTile, board))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidateCastleMovement(PieceColor playerColor, MovementType castleDirection, ChessBoard board)
        {
            if (castleDirection != MovementType.CastleKingSide && castleDirection != MovementType.CastleQueenSide)
                return false;

            var castleTiles = CastleHelper.GetCastleTiles(playerColor, castleDirection);

            // Ensure rook is present and allows castling
            var rookTile = board.GetTileByAnnotation(castleTiles.RookTileAnnotation)?.piece;
            var kingTile = board.GetTileByAnnotation(castleTiles.KingTileAnnotation)?.piece;
            if (rookTile == null || kingTile == null || !rookTile.AllowsCastling || !kingTile.AllowsCastling)
                return false;

            // Ensure no pieces are blocking the castling path
            foreach (var tileAnnotation in castleTiles.CheckTiles)
            {
                var tile = board.GetTileByAnnotation(tileAnnotation);
                if (tile.piece != null) return false;

                // Simulate each tile to ensure it's not under attack
                var simulatedBoard = _boardSimulationService.SimulateMove(board.GetTileByAnnotation(castleTiles.KingTileAnnotation), tile, board);
                tile = simulatedBoard.GetTileByAnnotation(tileAnnotation);
                if (!ValidateKingTileSafety(tile, simulatedBoard)) return false;
            }

            return true;
        }
    }
}
