using FinalWorkConsole.Sudoku;

namespace FinalWorkConsole.Games;

public class Game
{
    public int Id { get; }
    private static int _id = 10000;
    public int PlayerId { get; }
    private SudokuField StartField { get; }
    private SudokuField CurrentState { get; }

    public Game(int playerId, int hardLevel)
    {
        Id = _id++;
        PlayerId = playerId;
        StartField = SudokuTools.CreateGrid(hardLevel);
        CurrentState = SudokuTools.CloneGrid(StartField);
    }

    public void Save()
    {
        // TODO: create DAO methods
    }

    private bool CheckIfSolved()
    {
        return SudokuTools.CheckIfSolved(CurrentState);
    }

    public bool PutDigit(int row, int col, int value)
    {
        if (row is <= 0 or > 9 || col is <= 0 or > 9 || value is <= 0 or > 9)
        {
            throw new ArgumentException("Incorrect input data. You are using values out of range [1-9] inclusively!");
        }
        
        CurrentState.Field[row - 1, col - 1] = (byte)value;
        if (!SudokuTools.CheckIfOverwriteStartField(StartField, CurrentState)) return CheckIfSolved();
        CurrentState.Field[row - 1, col - 1] = StartField.Field[row - 1, col - 1];
        throw new ArgumentException("You cannot overwrite start state");
    }

    public void PrintCurrentState()
    {
        CurrentState.PrintField();
    }
}