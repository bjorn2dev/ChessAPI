using ChessAPI.Models;

namespace ChessAPI.Interfaces.Board
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
        void SetupBoard(ChessBoard board);
        void AddInitialPieces(ChessBoard board);
    }
}
