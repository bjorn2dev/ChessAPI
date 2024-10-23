using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    public interface ITileRenderer
    {
        string Render(Tile tile);
    }
}
