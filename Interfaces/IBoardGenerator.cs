using ChessAPI.Models;

namespace ChessAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBoardGenerator
    {
        Board Board { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void SetupBoard();
        void AddInitialPieces();
    }
}
