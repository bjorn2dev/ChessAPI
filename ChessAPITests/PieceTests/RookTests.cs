using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class RookTests
{
    // create static board setups to avoid creating new instances for every separate test.
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("CaptureStartingPosition");

    public static IEnumerable<object[]> GetCaptureBoardSetupData()
    {
        yield return new object[] { _captureBoardSetup };
    }


    public static IEnumerable<object[]> GetInvalidRookMoves()
    {
        // white
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("A8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("B5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("B4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("C4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("A1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("B8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("F2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("E2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("F3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("G3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("F8") };
        // black
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("A1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("B4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("A6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("F3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("A8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("E1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("D1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("H1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("E7") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("D3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("E6") };
    }
    public static IEnumerable<object[]> GetStandardRookMoves()
    {
        // white
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("A6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("H1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("A1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F1"), _captureBoardSetup.GetTileByNotation("C1") };
        // black
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("A3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("H8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("A8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("D6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("F8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D8"), _captureBoardSetup.GetTileByNotation("C8") };
    }

    public static IEnumerable<object[]> GetCaptureRookMoves()
    {
        // white
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("A4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A5"), _captureBoardSetup.GetTileByNotation("A7") };
        // black
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("A5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A4"), _captureBoardSetup.GetTileByNotation("A2") };
    }

    [Theory]
    [MemberData(nameof(GetCaptureRookMoves))]
    public void Test_RookMovementCapture(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Rook().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertRookMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
    {
        bool result = new Rook().IsValidMovement(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetStandardRookMoves))]
    public void Test_RookMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertRookMovement(fromTile, toTile, true);
    }

    [Theory]
    [MemberData(nameof(GetInvalidRookMoves))]
    public void Test_RookMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertRookMovement(fromTile, toTile, false);
    }
}