using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class PawnTests
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

    public static IEnumerable<object[]> GetInvalidPawnMoves()
    {
        // white
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("A2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("C2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("A1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("B1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("C1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("D4") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B2"), _standardBoardSetup.GetTileByNotation("B5") };
        // black
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B7"), _standardBoardSetup.GetTileByNotation("A7") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B7"), _standardBoardSetup.GetTileByNotation("C7") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B7"), _standardBoardSetup.GetTileByNotation("A8") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B7"), _standardBoardSetup.GetTileByNotation("B8") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B7"), _standardBoardSetup.GetTileByNotation("C8") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B7"), _standardBoardSetup.GetTileByNotation("D5") };
    }
    public static IEnumerable<object[]> GetStandardPawnMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTileByNotation("A2"), _standardBoardSetup.GetTileByNotation("A3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("A7"), _standardBoardSetup.GetTileByNotation("A6") };
        //yield return new object[] { StandardBoardSetup.GetTile(2, 1), StandardBoardSetup.GetTile(2, 2), MovementType.Horizontal };  // A2 to A4 for initial two-square move
    }

    public static IEnumerable<object[]> GetCapturePawnMoves()
    {
        // white
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D4"), _captureBoardSetup.GetTileByNotation("E5") }; 
        yield return new object[] { _captureBoardSetup.GetTileByNotation("E4"), _captureBoardSetup.GetTileByNotation("D5") };
        // black
        yield return new object[] { _captureBoardSetup.GetTileByNotation("D5"), _captureBoardSetup.GetTileByNotation("E4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("E5"), _captureBoardSetup.GetTileByNotation("D4") };
    }

    [Theory]
    [MemberData(nameof(GetCapturePawnMoves))]
    public void Test_PawnMovementCapture(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Pawn().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertPawnMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
    {
        bool result = new Pawn().IsValidMovement(fromTile, toTile, _standardBoardSetup._boardStateService.Board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetStandardPawnMoves))]
    public void Test_PawnMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertPawnMovement(fromTile, toTile, true);
    }

    [Theory]
    [MemberData(nameof(GetInvalidPawnMoves))]
    public void Test_PawnMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertPawnMovement(fromTile, toTile, false);
    }
}