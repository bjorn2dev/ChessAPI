using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardGenerator
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void SetupBoard(Board board);
        void AddInitialPieces(Board board);
    }
}
