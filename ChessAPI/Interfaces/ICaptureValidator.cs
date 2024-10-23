using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface ICaptureValidator
    {
        bool IsValidCapture(Tile from, Tile to);
    }
}
