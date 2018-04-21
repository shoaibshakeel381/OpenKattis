using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    internal class SudokuBoard
    {
        private readonly int[,] _data;
        public const int MAX_CELLS = 9;

        public SudokuBoard(IReadOnlyList<string> rows)
        {
            _data = new int[MAX_CELLS, MAX_CELLS];
            for (var i = 0; i < rows.Count; i++)
            {
                for (var j = 0; j < rows[i].Length; j++)
                {
                    _data[i, j] = int.Parse(rows[i][j].ToString());
                }
            }
        }

        public SudokuBoard(int[,] data)
        {
            _data = data;
        }

        /// <summary>
        /// Rotate Board Counter Clockwise
        /// </summary>
        /// <returns></returns>
        public SudokuBoard RotateCounterClockwise()
        {
            var temp = new int[MAX_CELLS, MAX_CELLS];
            for (int fRow = 0, tCol = 0; fRow < MAX_CELLS; fRow++, tCol++)
            {
                for (int fCol = MAX_CELLS - 1, tRow = 0; fCol >= 0; fCol--, tRow++)
                {
                    temp[tRow, tCol] = _data[fRow, fCol];
                }
            }

            return new SudokuBoard(temp);
        }

        /// <summary>
        /// Rotate Baord Clockwise
        /// </summary>
        /// <returns></returns>
        public SudokuBoard RotateClockwise()
        {
            var temp = new int[MAX_CELLS, MAX_CELLS];
            for (int fRow = 0, tCol = MAX_CELLS - 1; fRow < MAX_CELLS; fRow++, tCol--)
            {
                for (int fCol = 0, tRow = 0; fCol < MAX_CELLS; fCol++, tRow++)
                {
                    temp[tRow, tCol] = _data[fRow, fCol];
                }
            }

            return new SudokuBoard(temp);
        }

        public SudokuBoard SwapColumns(int colA, int colB)
        {
            var result = CloneData();
            for (var row = 0; row < MAX_CELLS; row++)
            {
                var swap = result[row, colA];
                result[row, colA] = result[row, colB];
                result[row, colB] = swap;
            }

            return new SudokuBoard(result);
        }

        public SudokuBoard SwapRows(int rowA, int rowB)
        {
            if (rowA < 0 || rowA >= MAX_CELLS || rowB < 0 || rowB >= MAX_CELLS)
                return null;

            var result = CloneData();
            for (var col = 0; col < MAX_CELLS; col++)
            {
                var swap = result[rowA, col];
                result[rowA, col] = result[rowB, col];
                result[rowB, col] = swap;
            }

            return new SudokuBoard(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowSegA"></param>
        /// <param name="rowSegB"></param>
        /// <returns></returns>
        public SudokuBoard SwapRowSegment(int rowSegA, int rowSegB)
        {
            var result = CloneData();
            var rowAStart = rowSegA * 3;
            var rowBStart = rowSegB * 3;
            for (var rowOffset = 0; rowOffset < 3; rowOffset++)
            {
                for (var col = 0; col < MAX_CELLS; col++)
                {
                    var swap = result[rowAStart + rowOffset, col];
                    result[rowAStart + rowOffset, col] = result[rowBStart, col];
                    result[rowBStart + rowOffset, col] = swap;
                }
            }

            return new SudokuBoard(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colSegA"></param>
        /// <param name="colSegB"></param>
        /// <returns></returns>
        public SudokuBoard SwapColumnSegment(int colSegA, int colSegB)
        {
            var result = CloneData();
            var colAStart = colSegA * 3;
            var colBStart = colSegB * 3;
            for (var colOffset = 0; colOffset < 3; colOffset++)
            {
                for (var row = 0; row < MAX_CELLS; row++)
                {
                    var swap = result[row, colAStart + colOffset];
                    result[row, colAStart + colOffset] = result[row, colBStart];
                    result[row, colBStart + colOffset] = swap;
                }
            }

            return new SudokuBoard(result);
        }

        private int[,] CloneData()
        {
            var result = new int[MAX_CELLS, MAX_CELLS];
            for (var row = 0; row < MAX_CELLS; row++)
            {
                for (var col = 0; col < MAX_CELLS; col++)
                {
                    result[row, col] = _data[row, col];
                }
            }

            return result;
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            for (var i = 0; i < MAX_CELLS; i++)
            {
                for (var j = 0; j < MAX_CELLS; j++)
                {
                    str.Append(_data[i, j] + "   ");
                }

                str.AppendLine();
            }

            return str.ToString();
        }

        public int this[int i, int j] => _data[i, j];
    }
}
