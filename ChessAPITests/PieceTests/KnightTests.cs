using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests;

public class KnightTests
{
    // create static board setups to avoid creating new instances for every separate test.
    private static readonly CompleteBoardSetup _standardBoardSetup = new CompleteBoardSetup("KnightStandardPosition");
    private static readonly CompleteBoardSetup _captureBoardSetup = new CompleteBoardSetup("KnightCapturePosition");
    private static readonly CompleteBoardSetup _edgeBoardSetup = new CompleteBoardSetup("KnightEdgePosition");
    private static readonly CompleteBoardSetup _invalidBoardSetup = new CompleteBoardSetup("KnightInvalidPosition");

    // MemberData for Knight captures
    public static IEnumerable<object[]> GetKnightCaptureMoves()
    {
        yield return new object[] { _captureBoardSetup.GetTileByNotation("C3"), _captureBoardSetup.GetTileByNotation("D5") };
        yield return new object[] { _captureBoardSetup.GetTileByNotation("F3"), _captureBoardSetup.GetTileByNotation("E5") };
    }

    // MemberData for valid Knight moves within board limits
    public static IEnumerable<object[]> GetStandardKnightMoves()
    {
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("A3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("B1"), _standardBoardSetup.GetTileByNotation("C3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("H3") };
        yield return new object[] { _standardBoardSetup.GetTileByNotation("G1"), _standardBoardSetup.GetTileByNotation("F3") };
    }

    // MemberData for invalid moves, where the Knight cannot reach
    public static IEnumerable<object[]> GetInvalidKnightMoves()
    {
        yield return new object[] { _invalidBoardSetup.GetTileByNotation("B1"), _invalidBoardSetup.GetTileByNotation("B3") };
        yield return new object[] { _invalidBoardSetup.GetTileByNotation("G1"), _invalidBoardSetup.GetTileByNotation("G3") };
    }

    // MemberData for edge cases where the Knight is on the board edge or corner
    public static IEnumerable<object[]> GetEdgeKnightMoves()
    {
        yield return new object[] { _edgeBoardSetup.GetTileByNotation("A1"), _edgeBoardSetup.GetTileByNotation("B3") };
        yield return new object[] { _edgeBoardSetup.GetTileByNotation("H1"), _edgeBoardSetup.GetTileByNotation("G3") };
        yield return new object[] { _edgeBoardSetup.GetTileByNotation("A8"), _edgeBoardSetup.GetTileByNotation("B6") };
        yield return new object[] { _edgeBoardSetup.GetTileByNotation("H8"), _edgeBoardSetup.GetTileByNotation("G6") };
    }

    // Tests

    [Theory]
    [MemberData(nameof(GetKnightCaptureMoves))]
    public void Test_KnightCaptureMoves(Tile fromTile, Tile toTile)
    {
        bool isValidMove = new Knight().IsValidCapture(fromTile, toTile, _captureBoardSetup._boardStateService.Board);
        Assert.True(isValidMove);
    }

    private void AssertKnightMovement(Tile fromTile, Tile toTile, bool expectedOutcome, Board board)
    {
        bool result = new Knight().IsValidMovement(fromTile, toTile, board);
        Assert.Equal(expectedOutcome, result);
    }

    [Theory]
    [MemberData(nameof(GetStandardKnightMoves))]
    public void Test_KnightMovementWithinLimits_Expect_True(Tile fromTile, Tile toTile)
    {
        AssertKnightMovement(fromTile, toTile, true, _standardBoardSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetInvalidKnightMoves))]
    public void Test_InvalidKnightMoves(Tile fromTile, Tile toTile)
    {
        AssertKnightMovement(fromTile, toTile, false, _invalidBoardSetup._boardStateService.Board);
    }

    [Theory]
    [MemberData(nameof(GetEdgeKnightMoves))]
    public void Test_EdgeKnightMoves(Tile fromTile, Tile toTile)
    {
        AssertKnightMovement(fromTile, toTile, true, _edgeBoardSetup._boardStateService.Board);
    }
}