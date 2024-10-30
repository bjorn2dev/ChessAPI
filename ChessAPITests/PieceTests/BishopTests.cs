using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class BishopTests
{
    // create static board setups to avoid creating new instances for every separate test.
    private static readonly CompleteBoardSetup _standardBoardSetup = new CompleteBoardSetup("StartingPosition");
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("CaptureStartingPosition");

    public static IEnumerable<object[]> GetCaptureBoardSetupData()
    {
        yield return new object[] { _captureBoardSetup };
    }

    public static IEnumerable<object[]> GetStandardBoardSetupData()
    {
        yield return new object[] { _standardBoardSetup };
    }

    public static IEnumerable<object[]> GetInvalidBishopMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTileByNotation("C1"), _standardBoardSetup.GetTileByNotation("B1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("C1"), _standardBoardSetup.GetTileByNotation("D1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("C1"), _standardBoardSetup.GetTileByNotation("C2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("C1"), _standardBoardSetup.GetTileByNotation("D2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("C1"), _standardBoardSetup.GetTileByNotation("D3") };

        yield return new object[] { _standardBoardSetup.GetTileByNotation("F1"), _standardBoardSetup.GetTileByNotation("G1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("F1"), _standardBoardSetup.GetTileByNotation("E1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("F1"), _standardBoardSetup.GetTileByNotation("F2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("F1"), _standardBoardSetup.GetTileByNotation("D2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("F1"), _standardBoardSetup.GetTileByNotation("D3") };
    }
    public static IEnumerable<object[]> GetStandardBishopMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("C3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("F3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("A3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("H3") };
    }

    public static IEnumerable<object[]> GetBishopCaptureBoardCaptureMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("C6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("F6") };
    }

    public static IEnumerable<object[]> GetBishopCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("A6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("C4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("A4") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("H6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("F4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("H4") };
    }

    public static IEnumerable<object[]> GetInvalidBishopCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("C5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("B7") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("A5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("B4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("B5"), _captureBoardSetup.GetTileByNotation("D7") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("F5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("G7") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("H5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("G4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("G5"), _captureBoardSetup.GetTileByNotation("E7") };
    }

    [Theory]
    [MemberData(nameof(GetBishopCaptureBoardCaptureMoves))]
    public void Test_BishopMovementCapture(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Bishop().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertBishopMovement(Tile fromTile, Tile toTile, bool expectedOutcome, Board board)
    {
        bool result = new Bishop().IsValidMovement(fromTile, toTile, board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetBishopCaptureBoardMoves))]
    public void Test_BishopMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertBishopMovement(fromTile, toTile, true, _captureBoardSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetInvalidBishopMoves))]
    public void Test_BishopMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertBishopMovement(fromTile, toTile, false, _standardBoardSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetInvalidBishopCaptureBoardMoves))]
    public void Test_BishopCaptureBoardMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertBishopMovement(fromTile, toTile, false, _captureBoardSetup._boardStateService.Board);
    }
}