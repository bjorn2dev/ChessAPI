using ChessAPI.Models.Pieces;
using ChessAPI.Models;
using ChessAPITests.BoardSetup;

public class BishopTests
{
    // create static board setups to avoid creating new instances for every separate test.
    // Static board setups for various test cases.
    private static readonly CompleteBoardSetup _standardMoveSetup = new CompleteBoardSetup("BishopStandardMovesPosition");
    private static readonly CompleteBoardSetup _captureMoveSetup = new CompleteBoardSetup("BishopCapturePosition");
    private static readonly CompleteBoardSetup _discoveredCheckSetup = new CompleteBoardSetup("BishopDiscoveredCheckPosition");
    private static readonly CompleteBoardSetup _invalidMoveSetup = new CompleteBoardSetup("BishopInvalidMovesPosition");

    // Member data for capture moves
    public static IEnumerable<object[]> GetBishopCaptureMoves()
    {
        yield return new object[] { _captureMoveSetup.GetTileByNotation("C4"), _captureMoveSetup.GetTileByNotation("D5") };
        yield return new object[] { _captureMoveSetup.GetTileByNotation("C4"), _captureMoveSetup.GetTileByNotation("B5") };
        yield return new object[] { _captureMoveSetup.GetTileByNotation("F6"), _captureMoveSetup.GetTileByNotation("E5") };
    }

    // Member data for standard diagonal moves
    public static IEnumerable<object[]> GetBishopStandardMoves()
    {
        yield return new object[] { _standardMoveSetup.GetTileByNotation("C1"), _standardMoveSetup.GetTileByNotation("E3") };//short
        yield return new object[] { _standardMoveSetup.GetTileByNotation("F1"), _standardMoveSetup.GetTileByNotation("D3") };
        yield return new object[] { _standardMoveSetup.GetTileByNotation("C1"), _standardMoveSetup.GetTileByNotation("B2") };
        yield return new object[] { _standardMoveSetup.GetTileByNotation("C1"), _standardMoveSetup.GetTileByNotation("H6") };//long
        yield return new object[] { _standardMoveSetup.GetTileByNotation("F1"), _standardMoveSetup.GetTileByNotation("A6") };
    }

    // Member data for discovered checks
    public static IEnumerable<object[]> GetBishopDiscoveredCheckMoves()
    {
        yield return new object[] { _discoveredCheckSetup.GetTileByNotation("D3"), _discoveredCheckSetup.GetTileByNotation("C4") };
        yield return new object[] { _discoveredCheckSetup.GetTileByNotation("D3"), _discoveredCheckSetup.GetTileByNotation("E4") };
    }

    // Member data for invalid moves
    public static IEnumerable<object[]> GetInvalidBishopMoves()
    {
        yield return new object[] { _invalidMoveSetup.GetTileByNotation("C1"), _invalidMoveSetup.GetTileByNotation("C2") };
        yield return new object[] { _invalidMoveSetup.GetTileByNotation("F1"), _invalidMoveSetup.GetTileByNotation("G1") };
        yield return new object[] { _invalidMoveSetup.GetTileByNotation("C1"), _invalidMoveSetup.GetTileByNotation("D1") };
        yield return new object[] { _invalidMoveSetup.GetTileByNotation("F1"), _invalidMoveSetup.GetTileByNotation("F3") };
        yield return new object[] { _invalidMoveSetup.GetTileByNotation("C1"), _invalidMoveSetup.GetTileByNotation("E3") }; // blocked
    }

    [Theory]
    [MemberData(nameof(GetBishopCaptureMoves))]
    public void Test_BishopCaptureMoves(Tile fromTile, Tile toTile)
    {
        // Act
        bool isValidMove = new Bishop().IsValidCapture(fromTile, toTile, _captureMoveSetup._boardStateService.Board);
        // Assert
        Assert.True(isValidMove);
    }

    [Theory]
    [MemberData(nameof(GetBishopStandardMoves))]
    public void Test_BishopStandardMoves(Tile fromTile, Tile toTile)
    {
        bool result = new Bishop().IsValidMovement(fromTile, toTile, _standardMoveSetup._boardStateService.Board);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(GetBishopDiscoveredCheckMoves))]
    public void Test_BishopDiscoveredCheck(Tile fromTile, Tile toTile)
    {
        bool result = new Bishop().IsValidMovement(fromTile, toTile, _discoveredCheckSetup._boardStateService.Board);
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(GetInvalidBishopMoves))]
    public void Test_InvalidBishopMoves(Tile fromTile, Tile toTile)
    {
        bool result = new Bishop().IsValidMovement(fromTile, toTile, _invalidMoveSetup._boardStateService.Board);
        Assert.False(result);
    }
}