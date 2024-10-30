using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class KnightTests
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

    public static IEnumerable<object[]> GetInvalidKnightMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("D2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("D1") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("E2") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("E1") };
    }
    public static IEnumerable<object[]> GetStandardKnightMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("C3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("F3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("A3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("H3") };
    }

    public static IEnumerable<object[]> GetKnightCaptureBoardCaptureMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("D5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("E5") };
    }

    public static IEnumerable<object[]> GetKnightCaptureBoardMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("B1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("A4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("E2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("B5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("G1") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("H4") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("D2") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("G5") };
    }

    [Theory]
    [MemberData(nameof(GetKnightCaptureBoardCaptureMoves))]
    public void Test_KnightMovementCapture(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Knight().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertKnightMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
    {
        bool result = new Knight().IsValidMovement(fromTile, toTile, _standardBoardSetup._boardStateService.Board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetStandardKnightMoves))]
    public void Test_KnightMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertKnightMovement(fromTile, toTile, true);
    }

    [Theory]
    [MemberData(nameof(GetInvalidKnightMoves))]
    public void Test_KnightMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertKnightMovement(fromTile, toTile, false);
    }
}