

namespace FinalWorkConsole.Sudoku;

public static class SudokuTools
{
    private static int _counter = 1;
    private static readonly byte[] NumberList = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private static readonly Random Random = new Random();

    public static SudokuField CloneGrid(SudokuField sudokuField)
    {
        return new SudokuField((byte[,])sudokuField.Field.Clone());
    } 
    
    public static SudokuField CreateGrid(int hardLevel)
    {
        var sudokuField = new SudokuField();
        FillGrid(sudokuField.Field);
        var countOfDigits = 81;
        while (countOfDigits > hardLevel)
        {
            var row = Random.Next(0, 9);
            var col = Random.Next(0, 9);
            while (sudokuField.Field[row, col] == 0)
            {
                row = Random.Next(0, 9);
                col = Random.Next(0, 9);
            }

            var backup = sudokuField.Field[row, col];
            sudokuField.Field[row, col] = 0;

            var copy = (byte [,]) sudokuField.Field.Clone();
            _counter = 0;
            SolveGrid(copy);
            if (_counter != 1)
            {
                sudokuField.Field[row, col] = backup;
                continue;
            }

            countOfDigits--;
        }

        return sudokuField;
    }

    private static bool CheckGrid(byte[,] grid)
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (grid[i, j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool SolveGrid(byte[,] grid)
    {
        var row = -1;
        var col = -1;
        for (var i = 0; i < 81; i++)
        {
            row = i / 9;
            col = i % 9;
            if (grid[row, col] != 0) continue;
            for (var value = 1; value < 10; value++)
            {
                if (CheckIfPresentInLine(row, value, grid, true) > 0) continue;
                if (CheckIfPresentInLine(col, value, grid, false) > 0) continue;
                if (CheckIfIsInSquare(row, col, value, grid) > 0) continue;
                grid[row, col] = (byte) value;
                if (CheckGrid(grid))
                {
                    _counter++;
                    break;
                }

                if (SolveGrid(grid))
                {
                    return true;
                }
            }
            break;
        }
        grid[row, col] = 0;

        return false;
    }

    private static bool FillGrid(byte[,] grid)
    {
        var row = -1;
        var col = -1;
        for (var i = 0; i < 81; i++)
        {
            row = i / 9;
            col = i % 9;
            if (grid[row, col] != 0) continue;
            Shuffle();
            foreach (int value in NumberList)
            {
                if (CheckIfPresentInLine(row, value, grid, true) > 0) continue;
                if (CheckIfPresentInLine(col, value, grid, false) > 0) continue;
                if (CheckIfIsInSquare(row, col, value, grid) > 0) continue;
                grid[row, col] = (byte) value;
                if (CheckGrid(grid))
                {
                    return true;
                } 
                if (FillGrid(grid))
                {
                    return true;
                }
            }
            break;
        }
        grid[row, col] = 0;
        return false;
    }

    public static bool CheckIfSolved(SudokuField sudokuField)
    {
        var grid = sudokuField.Field;
        for (var i = 0; i < 81; i++)
        {
            var row = i / 9;
            var col = i % 9;
            if (grid[row, col] == 0) return false;
            foreach (int value in NumberList)
            {
                if (CheckIfPresentInLine(row, value, grid, true) > 1) return false;
                if (CheckIfPresentInLine(col, value, grid, false) > 1) return false;
                if (CheckIfIsInSquare(row, col, value, grid) > 1) return false;
            }
        }
        return true;
    }

    private static int CheckIfPresentInLine(int sequenceNum, int value, byte[,] grid, bool isRow)
    {
        var result = 0;
        if (isRow)
        {
            for (var i = 0; i < 9; i++)
            {
                if (grid[sequenceNum, i] == value)
                {
                    result++;
                }
            }
        }
        else
        {
            for (var i = 0; i < 9; i++)
            {
                if (grid[i, sequenceNum] == value)
                {
                    result++;
                }
            }
        }
        return result;
    }

    private static int CheckIfIsInSquare(int row, int col, int value, byte[,] grid)
    {
        row = (row / 3) * 3;
        col = (col / 3) * 3;
        return CheckIfIsInNextThree(row, col, value, grid);
    }

    private static int CheckIfIsInNextThree(int row, int col, int value, byte[,] grid)
    {
        var result = 0;
        for (var i = row; i < row + 3; i++)
        {
            for (var j = col; j < col + 3; j++)
            {
                if (grid[i, j] == value)
                {
                    result++;
                }
            }
        }
        return result;
    }

    private static void Shuffle()
    {
        var n = NumberList.Length;
        while (n > 1)
        {
            n--;
            var k = Random.Next(n + 1);
            (NumberList[k], NumberList[n]) = (NumberList[n], NumberList[k]);
        }
    }

    public static bool CheckIfOverwriteStartField(SudokuField startField, SudokuField currentState)
    {
        var startGrid = startField.Field;
        var currentGrid = currentState.Field;
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (startGrid[i, j] != 0 && startGrid[i, j] != currentGrid[i, j])
                {
                    return true;
                }
            }
            
        }
        return false;
    }
}