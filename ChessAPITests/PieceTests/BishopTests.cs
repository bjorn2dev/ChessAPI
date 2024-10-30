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
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("D5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("E5") };
    }

    public static IEnumerable<object[]> GetBishopCaptureBoardMoves()
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
    [MemberData(nameof(GetBishopCaptureBoardCaptureMoves))]
    public void Test_BishopMovementCapture(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Bishop().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    private void AssertBishopMovement(Tile fromTile, Tile toTile, bool expectedOutcome)
    {
        bool result = new Bishop().IsValidMovement(fromTile, toTile, _standardBoardSetup._boardStateService.Board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetStandardBishopMoves))]
    public void Test_BishopMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertBishopMovement(fromTile, toTile, true);
    }

    [Theory]
    [MemberData(nameof(GetInvalidBishopMoves))]
    public void Test_BishopMovementWithinLimits_Expect_False(Tile fromTile, Tile toTile)
    {
        AssertBishopMovement(fromTile, toTile, false);
    }
}