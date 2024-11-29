using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests.BoardSetup;

public class KingTests
{
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("KingCapturePosition");
    private static readonly CompleteBoardSetup _castlingSetup = new CompleteBoardSetup("KingCastlingPosition");
    private static readonly CompleteBoardSetup _checkSetup = new CompleteBoardSetup("KingCheckPosition");

    // MemberData for king captures
    public static IEnumerable<object[]> GetKingCaptureBoardCaptureMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("E2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("G6") };
    }

    // MemberData for valid king moves within one square
    public static IEnumerable<object[]> GetKingCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("E1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("G1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("E3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("G3") };
    }

    // MemberData for invalid king moves
    public static IEnumerable<object[]> GetInvalidKingMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("F4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("F5") };
    }

    // MemberData for king castling moves
    public static IEnumerable<object[]> GetKingCastlingMoves()
    {
        yield return new object[] { _castlingSetup.GetTileByNotation("E1"), _castlingSetup.GetTileByNotation("C1") }; // White long castle
        yield return new object[] { _castlingSetup.GetTileByNotation("E1"), _castlingSetup.GetTileByNotation("G1") }; // White short castle
        yield return new object[] { _castlingSetup.GetTileByNotation("E8"), _castlingSetup.GetTileByNotation("C8") }; // Black long castle
        yield return new object[] { _castlingSetup.GetTileByNotation("E8"), _castlingSetup.GetTileByNotation("G8") }; // Black short castle
    }

    // MemberData for invalid king castling moves (blocked or under threat)
    public static IEnumerable<object[]> GetInvalidKingCastlingMoves()
    {
        yield return new object[] { _checkSetup.GetTileByNotation("E1"), _checkSetup.GetTileByNotation("C1") }; // Blocked or under threat
        yield return new object[] { _checkSetup.GetTileByNotation("E8"), _checkSetup.GetTileByNotation("G8") };
    }

    // MemberData for king moves that would place it in check
    public static IEnumerable<object[]> GetKingMovesIntoCheck()
    {
        yield return new object[] { _checkSetup.GetTileByNotation("E1"), _checkSetup.GetTileByNotation("E2") };
        yield return new object[] { _checkSetup.GetTileByNotation("E8"), _checkSetup.GetTileByNotation("D8") };
    }

    // Tests

    [Theory]
    [MemberData(nameof(GetKingCaptureBoardCaptureMoves))]
    public void Test_KingMovementCapture(Tile fromTile, Tile toTile)
    {
        bool isValidMove = new King().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        Assert.True(isValidMove);
    }

    private void AssertKingMovement(Tile fromTile, Tile toTile, bool expectedOutcome, ChessBoard board)
    {
        bool result = new King().IsValidMovement(fromTile, toTile, board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetKingCaptureBoardMoves))]
    public void Test_KingMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, true, _captureBoardSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetInvalidKingMoves))]
    public void Test_InvalidKingMoves(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, false, _captureBoardSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetKingCastlingMoves))]
    public void Test_ValidKingCastlingMoves(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, true, _castlingSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetInvalidKingCastlingMoves))]
    public void Test_InvalidKingCastlingMoves(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, false, _checkSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetKingMovesIntoCheck))]
    public void Test_KingMovesIntoCheck(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, false, _checkSetup._boardStateService.Board);
    }
}