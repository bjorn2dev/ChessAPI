using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class QueenTests
{
    // create static board setups to avoid creating new instances for every separate test.
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("CaptureStartingPosition");

    public static IEnumerable<object[]> GetCaptureBoardSetupData()
    {
        yield return new object[] { _captureBoardSetup };
    }

    public static IEnumerable<object[]> GetQueenCaptureBoardCaptureMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("A4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("E2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("D5") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("G5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("G6") };
    }

    public static IEnumerable<object[]> GetQueenCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("A3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("D2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("B2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("C4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("B1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("A1") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H7") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H3") };
    }

    public static IEnumerable<object[]> GetInvalidQueenCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("A6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("E6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("B6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("G8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("A2"), _captureBoardSetup.GetTileByNotation("E1") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("H1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("F5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("E8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("E5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("G3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("H5"), _captureBoardSetup.GetTileByNotation("D1") };
    }

    [Theory]
    [MemberData(nameof(GetQueenCaptureBoardCaptureMoves))]
    public void Test_QueenMovementCapture_Expect_True(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Queen().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertQueenMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
    {
        bool result = new Queen().IsValidMovement(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetQueenCaptureBoardMoves))]
    public void Test_QueenMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertQueenMovement(fromTile, toTile, true);
    }

    [Theory]
    [MemberData(nameof(GetInvalidQueenCaptureBoardMoves))]
    public void Test_QueenCaptureBoardMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertQueenMovement(fromTile, toTile, false);
    }
}