using ChessAPI.Interfaces;

namespace ChessAPI.Services
{
    /// <summary>
    /// BoardService class (using composition to separate concerns)
    /// </summary>
    public class BoardService : IBoardService
    {
      
        private readonly IBoardGenerator _boardGenerator;
        private readonly IBoardRenderer _boardRenderer;

        public BoardService(IBoardGenerator boardGenerator, IBoardRenderer boardRenderer)
        {
            _boardGenerator = boardGenerator;
            _boardRenderer = boardRenderer;
        }

        public string GetInitialBoard()
        {
            var board = _boardGenerator.GenerateBoard();
            return _boardRenderer.RenderBoard(board);
        }
    }
}
