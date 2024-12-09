﻿using ChessAPI.Models;
using ChessAPI.Models.Enums;

namespace ChessAPI.Interfaces.Piece
{
    public interface IKingSafetyValidator
    {
        bool ValidateKingSafety(Tile kingTile, Tile to, MovementType movementType, ChessBoard board);
        bool ValidateKingTileSafety(Tile checkTile, ChessBoard board);
    }
}
