using System.Drawing;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models
{
    public class Piece
    {
        private const string _kingWhiteStart = "E1";
        private const string _kingBlackStart = "E8";
        private const string _queenWhiteStart = "D1";
        private readonly string[] _rookWhiteStart = ["A1", "H1"];
        private readonly string[] _bishopWhiteStart = ["C1", "F1"];
        private readonly string[] _knightWhiteStart = ["B1", "G1"];
        private readonly string[] _pawnWhiteStart = ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2"];
        private const string _queenBlackStart = "D8";
        private readonly string[] _rookBlackStart = ["A8", "H8"];
        private readonly string[] _bishopBlackStart = ["C8", "F8"];
        private readonly string[] _knightBlackStart = ["B8", "G8"];
        private readonly string[] _pawnBlackStart = ["A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7"];

        public PieceColor color;

        public bool hasColor;

        private string _boardLocation = string.Empty;
        public string boardLocation
        {
            get => _boardLocation;
            set
            {
                _boardLocation = value;
                if (!hasColor)
                {
                    SetInitialColor();
                }
            }
        }
        public string name = string.Empty;
        public string html = string.Empty;

        public void SetInitialColor()
        {
            if (!String.IsNullOrEmpty(this.boardLocation) && (
                this.boardLocation == _kingWhiteStart ||
                this.boardLocation == _queenWhiteStart ||
                _rookWhiteStart.Contains(this.boardLocation) ||
                _bishopWhiteStart.Contains(this.boardLocation) ||
                _knightWhiteStart.Contains(this.boardLocation) ||
                _pawnWhiteStart.Contains(this.boardLocation)))
            {
                this.color = PieceColor.White;
            }
            if (!String.IsNullOrEmpty(this.boardLocation) && (
                this.boardLocation == _kingBlackStart ||
                this.boardLocation == _queenBlackStart ||
                _rookBlackStart.Contains(this.boardLocation) ||
                _bishopBlackStart.Contains(this.boardLocation) ||
                _knightBlackStart.Contains(this.boardLocation) ||
                _pawnBlackStart.Contains(this.boardLocation)))
            {
                this.color = PieceColor.Black;
            }
            hasColor = true;
            HasColor();
        }

        public virtual void HasColor()
        {

        }

    }
}
