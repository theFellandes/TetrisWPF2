using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    internal class GameGrid
    {

        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }

        public int this[int row, int col]
        {
            get => grid[row, col];
            set => grid[row, col] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[Rows, Columns];
        }

        public bool IsInside(int row, int col) => row >= 0 && row < Rows && col >= 0 && col < Columns;

        public bool IsEmpty(int row, int col) => IsInside(row, col) && grid[row, col] == 0;

        public bool IsRowFull(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (grid[row, col] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsRowEmpty(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (grid[row, col] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearRow(int row)
        {
            for (int col = 0; col < Columns; col++)
            {
                grid[row, col] = 0;
            }
        }

        private void MoveRowDown(int row, int numRows)
        {
            for(int col = 0; col < Columns; col++)
            {
                grid[row + numRows, col] = grid[row, col];
                grid[row, col] = 0;
            }
        }

        public int ClearFullRows()
        {
            int cleared = 0;

            for(int row = Rows - 1; row >= 0; row--)
            {
                if (IsRowFull(row))
                {
                    ClearRow(row);
                    cleared++;
                }
                else if(cleared > 0)
                {
                    MoveRowDown(row, cleared);
                }
            }
            return cleared;
        }
    }
}
