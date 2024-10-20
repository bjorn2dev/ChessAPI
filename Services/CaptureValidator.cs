using ChessAPI.Interfaces;
using ChessAPI.Models;

namespace ChessAPI.Services
{
    public class CaptureValidator : ICaptureValidator
    {
        public bool IsValidCapture(Tile from, Tile to)
        {
            return from.piece == null || (to.piece != null && from.piece.color == to.piece.color) || from.tileAnnotation == to.tileAnnotation;
        }
    }
}
