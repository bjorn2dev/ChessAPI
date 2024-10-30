using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class KingTests
{
    // create static board setups to avoid creating new instances for every separate test.
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("CaptureStartingPosition");

    public static IEnumerable<object[]> GetCaptureBoardSetupData()
    {
        yield return new object[] { _captureBoardSetup };
    }

    public static IEnumerable<object[]> GetKingCaptureBoardCaptureMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("E2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("G6") };
    }

    public static IEnumerable<object[]> GetKingCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("E1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("G1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("E3") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("G3") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("E8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("F8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("G8") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("E7") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("E6") };
    }

    public static IEnumerable<object[]> GetInvalidKingCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("F1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("G2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F2"), _captureBoardSetup.GetTileByNotation("F3") };

        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("F6") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F7"), _captureBoardSetup.GetTileByNotation("G7") };
    }

    [Theory]
    [MemberData(nameof(GetKingCaptureBoardCaptureMoves))]
    public void Test_KingMovementCapture(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new King().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertKingMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
    {
        bool result = new King().IsValidMovement(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetKingCaptureBoardMoves))]
    public void Test_KingMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, true);
    }

    [Theory]
    [MemberData(nameof(GetInvalidKingCaptureBoardMoves))]
    public void Test_KingCaptureBoardMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertKingMovement(fromTile, toTile, false);
    }
}