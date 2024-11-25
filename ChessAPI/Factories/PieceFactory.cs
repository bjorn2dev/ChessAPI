using ChessAPI.Interfaces;
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

        public Piece CreatePiece(Type pieceType, PieceColor color)
        {
            if(pieceType == typeof(King))
            {
                var king = _serviceProvider.GetRequiredService<King>();
                king.color = color;
                return king;
            }

            var piece = (Piece)Activator.CreateInstance(pieceType);
            piece.color = color;
            return piece;
        }
    }
}
