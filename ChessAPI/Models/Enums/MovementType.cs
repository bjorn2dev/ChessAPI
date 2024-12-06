namespace ChessAPI.Models.Enums
{
    public enum MovementType
    {
        Invalid,
        Diagonal,
        Horizontal,
        Vertical,
        LShaped,
        Capture,
        CastleKingSide,
        CastleQueenSide,
        Promotion,
        EnPassant
    }
}
