using ChessAPI.Models.Pieces;
using System.Drawing;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models
{
    public class Piece
    {
        protected StartingLocation _startingLocation;
        public Piece(StartingLocation startingLocation = null) {
            _startingLocation = startingLocation;
        }

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
                this.boardLocation == _startingLocation.KingWhiteStart ||
                this.boardLocation == _startingLocation.QueenWhiteStart ||
                _startingLocation.BishopWhiteStart.Contains(this.boardLocation) ||
                _startingLocation.KnightWhiteStart.Contains(this.boardLocation) ||
                _startingLocation.RookWhiteStart.Contains(this.boardLocation) ||
                _startingLocation.PawnWhiteStart.Contains(this.boardLocation)))
            {
                this.color = PieceColor.White;
            }
            if (!String.IsNullOrEmpty(this.boardLocation) && (
                this.boardLocation == _startingLocation.KingBlackStart ||
                this.boardLocation == _startingLocation.QueenBlackStart ||
                _startingLocation.BishopBlackStart.Contains(this.boardLocation) ||
                _startingLocation.KnightBlackStart.Contains(this.boardLocation) ||
                _startingLocation.RookBlackStart.Contains(this.boardLocation) ||
                _startingLocation.PawnBlackStart.Contains(this.boardLocation)))
            {
                this.color = PieceColor.Black;
            }
            // todo check
            hasColor = true;
            HasColor();
        }

        public virtual void HasColor()
        {

        }

    }
}
