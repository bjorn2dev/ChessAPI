using ChessAPI.Interfaces.Piece;
using ChessAPI.Models;
using ChessAPI.Models.Pieces;
using Microsoft.Extensions.Options;
using static ChessAPI.Models.Enums.Color;

namespace ChessAPI.Services.Piece
{
    public class PositionProvider : IPositionProvider
    {
        private readonly StartingPositionSettings _startingPositionSettings;
        public PositionProvider(IOptions<StartingPositionSettings> startingPositionSettings)
        {
            _startingPositionSettings = startingPositionSettings.Value;
        }

        public bool IsWhiteStartingPosition(string tileLocation)
        {
            return !string.IsNullOrEmpty(tileLocation) && (
                tileLocation == _startingPositionSettings.KingWhiteStart ||
                tileLocation == _startingPositionSettings.QueenWhiteStart ||
                _startingPositionSettings.RookWhiteStart.Contains(tileLocation) ||
                _startingPositionSettings.BishopWhiteStart.Contains(tileLocation) ||
                _startingPositionSettings.KnightWhiteStart.Contains(tileLocation) ||
                _startingPositionSettings.PawnWhiteStart.Contains(tileLocation));
        }

        public bool IsBlackStartingPosition(string tileLocation)
        {
            return !string.IsNullOrEmpty(tileLocation) && (
               tileLocation == _startingPositionSettings.KingBlackStart ||
               tileLocation == _startingPositionSettings.QueenBlackStart ||
               _startingPositionSettings.RookBlackStart.Contains(tileLocation) ||
               _startingPositionSettings.BishopBlackStart.Contains(tileLocation) ||
               _startingPositionSettings.KnightBlackStart.Contains(tileLocation) ||
               _startingPositionSettings.PawnBlackStart.Contains(tileLocation));
        }

        public Type GetPieceTypeForLocation(string location)
        {
            if (_startingPositionSettings.KingWhiteStart == location || _startingPositionSettings.KingBlackStart == location)
            {
                return typeof(King);
            }
            if (_startingPositionSettings.QueenWhiteStart == location || _startingPositionSettings.QueenBlackStart == location)
            {
                return typeof(Queen);
            }
            if (_startingPositionSettings.RookWhiteStart.Contains(location) || _startingPositionSettings.RookBlackStart.Contains(location))
            {
                return typeof(Rook);
            }
            if (_startingPositionSettings.BishopWhiteStart.Contains(location) || _startingPositionSettings.BishopBlackStart.Contains(location))
            {
                return typeof(Bishop);
            }
            if (_startingPositionSettings.KnightWhiteStart.Contains(location) || _startingPositionSettings.KnightBlackStart.Contains(location))
            {
                return typeof(Knight);
            }
            if (_startingPositionSettings.PawnWhiteStart.Contains(location) || _startingPositionSettings.PawnBlackStart.Contains(location))
            {
                return typeof(Pawn);
            }

            return null; // No piece found for the location
        }
    }
}
