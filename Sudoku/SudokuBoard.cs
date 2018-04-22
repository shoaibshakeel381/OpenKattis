using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    internal class SudokuBoard
    {
        private int[,] _data;
        public const int MAX_CELLS = 9;
        
        public SudokuBoard()
        {
            _data = new int[MAX_CELLS, MAX_CELLS];
        }

        public SudokuBoard(IReadOnlyList<string> rows)
        {
            _data = new int[MAX_CELLS, MAX_CELLS];
            for (var i = 0; i < rows.Count; i++)
            {
                for (var j = 0; j < rows[i].Length; j++)
                {
                    this[i, j] = int.Parse(rows[i][j].ToString());
                }
            }
        }

        /// <summary>
        /// Rotate Board Counter Clockwise
        /// </summary>
        /// <returns></returns>
        public SudokuBoard RotateCounterClockwise()
        {
            var board = new SudokuBoard();
            for (int fRow = 0, tCol = 0; fRow < MAX_CELLS; fRow++, tCol++)
            {
                for (int fCol = MAX_CELLS - 1, tRow = 0; fCol >= 0; fCol--, tRow++)
                {
                    board[tRow, tCol] = this[fRow, fCol];
                }
            }
            
            return board;
        }

        /// <summary>
        /// Rotate Board Clockwise
        /// </summary>
        /// <returns></returns>
        public SudokuBoard RotateClockwise()
        {
            var board = new SudokuBoard();
            for (int fRow = 0, tCol = MAX_CELLS - 1; fRow < MAX_CELLS; fRow++, tCol--)
            {
                for (int fCol = 0, tRow = 0; fCol < MAX_CELLS; fCol++, tRow++)
                {
                    board[tRow, tCol] = this[fRow, fCol];
                }
            }

            return board;
        }

        public SudokuBoard SwapColumns(int colA, int colB)
        {
            var board = Clone();
            for (var row = 0; row < MAX_CELLS; row++)
            {
                var swap = board[row, colA];
                board[row, colA] = board[row, colB];
                board[row, colB] = swap;
            }

            return board;
        }

        public SudokuBoard SwapRows(int rowA, int rowB)
        {
            var board = Clone();
            for (var col = 0; col < MAX_CELLS; col++)
            {
                var swap = board[rowA, col];
                board[rowA, col] = board[rowB, col];
                board[rowB, col] = swap;
            }

            return board;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowSegA"></param>
        /// <param name="rowSegB"></param>
        /// <returns></returns>
        public SudokuBoard SwapRowSegment(int rowSegA, int rowSegB)
        {
            var board = Clone();
            var rowAStart = rowSegA * 3;
            var rowBStart = rowSegB * 3;
            for (var rowOffset = 0; rowOffset < 3; rowOffset++)
            {
                for (var col = 0; col < MAX_CELLS; col++)
                {
                    var swap = board[rowAStart + rowOffset, col];
                    board[rowAStart + rowOffset, col] = board[rowBStart + rowOffset, col];
                    board[rowBStart + rowOffset, col] = swap;
                }
            }

            return board;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colSegA"></param>
        /// <param name="colSegB"></param>
        /// <returns></returns>
        public SudokuBoard SwapColumnSegment(int colSegA, int colSegB)
        {
            var board = Clone();
            var colAStart = colSegA * 3;
            var colBStart = colSegB * 3;
            for (var colOffset = 0; colOffset < 3; colOffset++)
            {
                for (var row = 0; row < MAX_CELLS; row++)
                {
                    var swap = board[row, colAStart + colOffset];
                    board[row, colAStart + colOffset] = board[row, colBStart + colOffset];
                    board[row, colBStart + colOffset] = swap;
                }
            }

            return board;
        }

        public SudokuBoard Clone()
        {
            var board = new SudokuBoard();
            for (var row = 0; row < MAX_CELLS; row++)
            {
                for (var col = 0; col < MAX_CELLS; col++)
                {
                    board[row, col] = this[row, col];
                }
            }

            return board;
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            for (var i = 0; i < MAX_CELLS; i++)
            {
                for (var j = 0; j < MAX_CELLS; j++)
                {
                    str.Append(this[i, j] + "   ");
                }

                str.AppendLine();
            }

            return str.ToString();
        }

        public int this[int i, int j]
        {
            get { return _data[i, j]; }
            private set { _data[i, j] = value; }
        }
    }
}
