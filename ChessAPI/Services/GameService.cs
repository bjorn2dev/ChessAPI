using ChessAPI.Interfaces;
using ChessAPI.Models;
using ChessAPI.Models.Enums;
namespace ChessAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IColorSideSelector _colorSideSelector;
        private User WhitePlayer;
        private User BlackPlayer;
        public bool _playersInitialized;

        public GameService(IColorSideSelector colorSideSelector)
        {
            this._colorSideSelector = colorSideSelector;
            this._playersInitialized = false;
        }
        public string GetColorSelector()
        {
            List<Color.PieceColor> pieceColorsToShow = new List<Color.PieceColor>();
            if (this.WhitePlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.White);
            }
            if (this.BlackPlayer == null)
            {
                pieceColorsToShow.Add(Color.PieceColor.Black);
            }
            if (pieceColorsToShow.Count() == 0)
            {
                this._playersInitialized = true;
            }
            return _colorSideSelector.RenderColorSelector(pieceColorsToShow);
        }

        public void SetupPlayer(Color.PlayerColor playerColor)
        {
            switch (playerColor)
            {
                case Color.PlayerColor.White:
                    this.SetWhitePlayer();
                    break;
                case Color.PlayerColor.Black:
                    this.SetBlackPlayer();
                    break;
            }
        }

        public void SetWhitePlayer()
        {
            this.WhitePlayer = new User();
        }

        public void SetBlackPlayer()
        {
            this.BlackPlayer = new User();
        }

        public bool IsBoardInitialized() => this._playersInitialized;

    }
}
