using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface IKingSafetyValidator
    {
        bool ValidateKingSafety(Tile from, Tile to, MovementType movementType, Board board);
        bool ValidateKingTileSafety(Tile checkTile, Board board);
    }
}
