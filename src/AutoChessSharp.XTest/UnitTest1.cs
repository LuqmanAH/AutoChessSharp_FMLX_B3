using AutoChessSharp.Core;
namespace AutoChessSharp.XTest;

public class UnitTest1
{
    Board? testBoard;
    GameRunner? testGame;

    [Fact]
    public void GetInitCurrentRoundTest()
    {
        testBoard = new Board(8);
        testGame = new GameRunner(testBoard);
        int actualRound = testGame.GetCurrentRound();

        Assert.Equal(1, actualRound);

    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(46184, true)]
    public void SetRoundTest(int round, bool expected)
    {
        testBoard = new Board(8);
        testGame = new GameRunner(testBoard);

        bool setStatus =testGame.SetRound(round);
        Assert.Equal(expected, setStatus);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(-3, false)]
    [InlineData(1, true)]
    [InlineData(3457, true)]
    [InlineData(23, true)]
    public void SetCountDownTest(int countdown, bool expected)
    {
        testBoard = new Board(8);
        testGame = new GameRunner(testBoard);

        bool setStatus = testGame.SetCountDown(countdown);
        Assert.Equal(expected, setStatus);
    }

    [Theory]
    [InlineData(GameStatusEnum.NotStarted, false)]
    [InlineData(GameStatusEnum.Completed, true)]
    [InlineData(GameStatusEnum.Ongoing, true)]
    public void SetGameStatusTest(GameStatusEnum gameStatus, bool expected)
    {
        testBoard = new Board(8);
        testGame = new GameRunner(testBoard);

        bool setStatus = testGame.SetGameStatus(gameStatus);
        Assert.Equal(expected, setStatus);
    }
}