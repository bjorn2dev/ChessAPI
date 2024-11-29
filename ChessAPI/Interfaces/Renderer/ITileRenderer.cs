using ChessAPI.Models;

namespace ChessAPI.Interfaces.Renderer
{
    public interface ITileRenderer
    {
        string Render(Tile tile);
    }
}
