using ChessAPI.Models;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Helpers
{
    public static class CastleHelper
    {
        public static readonly string WhiteKingTileAnnotation = "E1";
        public static readonly string BlackKingTileAnnotation = "E8";
        public static readonly string WhiteKingSideCastleTileAnnotation = "G1";
        public static readonly string WhiteQueenSideCastleTileAnnotation = "C1";
        public static readonly string BlackKingSideCastleTileAnnotation = "G8";
        public static readonly string BlackQueenSideCastleTileAnnotation = "C8";
        public static readonly string WhiteKingSideCastleRookTileAnnotation = "H1";
        public static readonly string WhiteQueenSideCastleRookTileAnnotation = "A1";
        public static readonly string BlackKingSideCastleRookTileAnnotation = "H8";
        public static readonly string BlackQueenSideCastleRookTileAnnotation = "A8";
        public static readonly string WhiteKingSideCastleRookFinalTileAnnotation = "H1";
        public static readonly string WhiteQueenSideCastleRookFinalTileAnnotation = "A1";
        public static readonly string BlackKingSideCastleRookFinalTileAnnotation = "H8";
        public static readonly string BlackQueenSideCastleRookFinalTileAnnotation = "A8";
        public static readonly string[] WhiteKingSideCastleCheckTiles = ["F1", "G1"];
        public static readonly string[] WhiteQueenSideCastleCheckTiles = ["D1", "C1"];
        public static readonly string[] BlackKingSideCastleCheckTiles = ["F8", "G8"];
        public static readonly string[] BlackQueenSideCastleCheckTiles = ["D8", "C8"];
        public static (string KingTileAnnotation, string KingToTileAnnotation, string RookTileAnnotation, string RookFinalPositionTileAnnotation, string[] CheckTiles) GetCastleTiles(PieceColor playerColor, MovementType castleDirection)
        {
            var kingTile = playerColor == PieceColor.White ? CastleHelper.WhiteKingTileAnnotation : CastleHelper.BlackKingTileAnnotation;

            if (castleDirection == MovementType.CastleKingSide)
                return (kingTile,
                    playerColor == PieceColor.White ? CastleHelper.WhiteKingSideCastleTileAnnotation : BlackKingSideCastleTileAnnotation,
                    playerColor == PieceColor.White ? CastleHelper.WhiteKingSideCastleRookTileAnnotation : CastleHelper.BlackKingSideCastleRookTileAnnotation,
                    playerColor == PieceColor.White ? CastleHelper.WhiteKingSideCastleRookFinalTileAnnotation : CastleHelper.BlackKingSideCastleRookFinalTileAnnotation,
                    playerColor == PieceColor.White ? CastleHelper.WhiteKingSideCastleCheckTiles : CastleHelper.BlackKingSideCastleCheckTiles);

            if (castleDirection == MovementType.CastleQueenSide)
                return (kingTile,
                    playerColor == PieceColor.White ? CastleHelper.WhiteQueenSideCastleTileAnnotation : CastleHelper.BlackQueenSideCastleTileAnnotation,
                    playerColor == PieceColor.White ? CastleHelper.WhiteKingSideCastleRookTileAnnotation : CastleHelper.BlackQueenSideCastleRookTileAnnotation,
                    playerColor == PieceColor.White ? CastleHelper.WhiteQueenSideCastleRookFinalTileAnnotation : CastleHelper.BlackQueenSideCastleRookFinalTileAnnotation,
                    playerColor == PieceColor.White ? CastleHelper.WhiteQueenSideCastleCheckTiles : CastleHelper.BlackQueenSideCastleCheckTiles);

            return ("", "", "", "", []);
        }
    }
}
