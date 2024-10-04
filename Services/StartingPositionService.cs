using ChessAPI.Interfaces;
using ChessAPI.Models.Pieces;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services
{
    public class StartingPositionService : IStartingPositionProvider
    {
        private readonly string _kingWhiteStart = "E1";
        private readonly string _queenWhiteStart = "D1";
        private readonly string[] _rookWhiteStart = ["A1", "H1"];
        private readonly string[] _bishopWhiteStart = ["C1", "F1"];
        private readonly string[] _knightWhiteStart = ["B1", "G1"];
        private readonly string[] _pawnWhiteStart = ["A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2"];
        private readonly string _kingBlackStart = "E8";
        private readonly string _queenBlackStart = "D8";
        private readonly string[] _rookBlackStart = ["A8", "H8"];
        private readonly string[] _bishopBlackStart = ["C8", "F8"];
        private readonly string[] _knightBlackStart = ["B8", "G8"];
        private readonly string[] _pawnBlackStart = ["A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7"];
        private readonly string _startingLocation = string.Empty;
        public StartingPositionService()
        {           
        }

        public bool IsWhiteStartingPosition(string tileLocation)
        {
            return !String.IsNullOrEmpty(tileLocation) && (
                tileLocation == _kingWhiteStart ||
                tileLocation == _queenWhiteStart ||
                _rookWhiteStart.Contains(tileLocation) ||
                _bishopWhiteStart.Contains(tileLocation) ||
                _knightWhiteStart.Contains(tileLocation) ||
                _pawnWhiteStart.Contains(tileLocation));
        }

        public bool IsBlackStartingPosition(string tileLocation)
        {
            return !String.IsNullOrEmpty(tileLocation) && (
               tileLocation == _kingBlackStart ||
               tileLocation == _queenBlackStart ||
               _rookBlackStart.Contains(tileLocation) ||
               _bishopBlackStart.Contains(tileLocation) ||
               _knightBlackStart.Contains(tileLocation) ||
               _pawnBlackStart.Contains(tileLocation));
        }

        public Type GetPieceTypeForLocation(string location)
        {
            if (_kingWhiteStart == location || _kingBlackStart == location)
            {
                return typeof(King);
            }
            if (_queenWhiteStart == location || _queenBlackStart == location)
            {
                return typeof(Queen);
            }
            if (_rookWhiteStart.Contains(location) || _rookBlackStart.Contains(location))
            {
                return typeof(Rook);
            }
            if (_bishopWhiteStart.Contains(location) || _bishopBlackStart.Contains(location))
            {
                return typeof(Bishop);
            }
            if (_knightWhiteStart.Contains(location) || _knightBlackStart.Contains(location))
            {
                return typeof(Knight);
            }
            if (_pawnWhiteStart.Contains(location) || _pawnBlackStart.Contains(location))
            {
                return typeof(Pawn);
            }

            return null; // No piece found for the location
        }
    }
}
