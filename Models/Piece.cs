using ChessAPI.Models;
using ChessAPI.Models.Enums;
using System.Drawing;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Models
{
    public class Piece
    {

        private PieceColor _color;
        private string _boardLocation = string.Empty;
        public PieceColor color
        {
            get => _color;
            set
            {
                _color = value;
                // Automatically update HTML whenever color is set
                UpdateHtml();
            }
        }

        public string boardLocation
        {
            get => _boardLocation;
            set
            {
                _boardLocation = value;
                UpdateHtml();  // Automatically update HTML whenever boardLocation is set
            }
        }

        public string name = string.Empty;
        public string html = string.Empty;
        // This method will automatically render the HTML when called
        protected void UpdateHtml()
        {
            this.html = RenderHtml(this);  // Auto-generate the HTML
        }

        public virtual string RenderHtml(Piece piece)
        {
            // Default rendering logic (can be overridden by subclasses)
            return $"<p>{piece.name}</p>";
        }

    }
}
