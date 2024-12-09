namespace ChessAPI.Models.Enums
{

    // todo change capture etc to movementaction?
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
