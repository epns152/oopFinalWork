namespace FinalWorkConsole.Sudoku;

public class SudokuField
{
    internal readonly byte[,] Field = new byte[9, 9];

    public SudokuField()
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                Field[i, j] = 0;
            }
        }
    }

    public SudokuField(byte[,] grid)
    {
        Field = grid;
    }

    public void PrintField()
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                var str = Field[i, j] > 0 ? Field[i, j].ToString() : " ";
                Console.Write(" " + str + " ");
                if ((j + 1) % 3 == 0 && j != 8)
                {
                    Console.Write("|");
                }
            }

            if ((i + 1) % 3 == 0 && i != 8)
            {
                Console.Write("\n---------|---------|---------");
            }
            Console.WriteLine();
        }
    }
}