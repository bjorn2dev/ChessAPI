using ChessAPI.Interfaces.Piece;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Factories
{
    public class PieceFactory : IPieceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public PieceFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public ChessPiece CreatePiece(Type pieceType, PieceColor color)
        {
            if(pieceType == typeof(King))
            {
                var king = _serviceProvider.GetRequiredService<King>();
                king.color = color;
                return king;
            }

            var piece = (ChessPiece)Activator.CreateInstance(pieceType);
            piece.color = color;
            return piece;
        }
    }
}
