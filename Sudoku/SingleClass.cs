using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Kattis.IO;

namespace Sudokus
{
    public class Program
    {
        internal List<SudokuBoard> CompletedBoards { get; private set; }
        internal List<SudokuBoard> IncompleteBoards { get; private set; }

        public static void Main(string[] args)
        {
            var program = new Program();

            program.TakeUserInput();

            for (var i = 0; i < program.CompletedBoards.Count; i++)
            {
                var matcher = new SudokuBoardMatcher();
                Console.WriteLine(matcher.Match(program.CompletedBoards[i], program.IncompleteBoards[i]) ? "Yes" : "No");
            }
        }

        public void TakeUserInput()
        {
            CompletedBoards = new List<SudokuBoard>();
            IncompleteBoards = new List<SudokuBoard>();

            var scanner = new Scanner();
            while (scanner.HasNext())
            {
                var count = int.Parse(scanner.Next());

                for (var i = 0; i < count; i++)
                {
                    var boardData = new List<string>();
                    for (var j = 0; j < 9; j++)
                    {
                        boardData.Add(scanner.Next());
                    }
                    CompletedBoards.Add(new SudokuBoard(boardData));

                    boardData = new List<string>();
                    for (var j = 0; j < 9; j++)
                    {
                        boardData.Add(scanner.Next());
                    }
                    IncompleteBoards.Add(new SudokuBoard(boardData));
                }
            }
        }
    }

    internal class SudokuBoard
    {
        private readonly int[,] _data;
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

        public bool IsBoardPermutation(SudokuBoard board)
        {
            return IsColumnSegmentPermutation(board, 0) &&
                   IsColumnSegmentPermutation(board, 1) &&
                   IsColumnSegmentPermutation(board, 2);
        }

        public bool IsColumnSegmentPermutation(SudokuBoard board, int colSeg)
        {
            var colSegStart = colSeg * 3;
            if (TestColumnSegmentPermutation(board, colSegStart))
            {
                return true;
            }

            return false;
        }

        private bool TestColumnSegmentPermutation(SudokuBoard board, int colSegStart)
        {
            for (var row = 0; row < MAX_CELLS; row++)
            {
                for (var col = colSegStart; col < colSegStart + 3; col++)
                {
                    if (board[row, col] != 0 && this[row, col] != board[row, col])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private SudokuBoard Clone()
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

    internal class SudokuBoardMatcher
    {
        public bool Match(SudokuBoard oldBoard, SudokuBoard newBoard)
        {
            // Try rotating board
            for (var rot = 0; rot < 2; rot++)
            {
                // Try swapping row regions
                for (var rowseg = 0; rowseg < 6; rowseg++)
                {
                    // Try swapping rows in First 3x9 Band
                    for (var row0 = 0; row0 < 6; row0++)
                    {
                        // Try swapping rows in Second 3x9 Band
                        for (var row1 = 0; row1 < 6; row1++)
                        {
                            // Try swapping rows in Third 3x9 Band
                            for (var row2 = 0; row2 < 6; row2++)
                            {
                                // Try swapping column regions
                                for (var colseg = 0; colseg < 6; colseg++)
                                {
                                    // Try matching board
                                    if (oldBoard.IsBoardPermutation(newBoard))
                                    {
                                        return true;
                                    }
                                    oldBoard = oldBoard.SwapColumnSegment(colseg % 2 == 0 ? 1 : 0, 2);
                                }
                                oldBoard = oldBoard.SwapRows(row2 % 2 == 0 ? 7 : 6, 8);
                            }
                            oldBoard = oldBoard.SwapRows(row1 % 2 == 0 ? 4 : 3, 5);
                        }
                        oldBoard = oldBoard.SwapRows(row0 % 2 == 0 ? 1 : 0, 2);
                    }
                    oldBoard = oldBoard.SwapRowSegment(rowseg % 2 == 0 ? 1 : 0, 2);
                }
                oldBoard = oldBoard.RotateClockwise();
            }

            return false;
        }
    }
}

namespace Kattis.IO
{
    public class Scanner : Tokenizer
    {

        public int NextInt()
        {
            return int.Parse(Next());
        }

        public long NextLong()
        {
            return long.Parse(Next());
        }

        public float NextFloat()
        {
            return float.Parse(Next());
        }

        public double NextDouble()
        {
            return double.Parse(Next());
        }
    }

    public class Tokenizer
    {
        string[] tokens = new string[0];
        private int pos;
        StreamReader reader;

        public Tokenizer(Stream inStream)
        {
            var bs = new BufferedStream(inStream);
            reader = new StreamReader(bs);
        }

        public Tokenizer() : this(Console.OpenStandardInput())
        {
            // Nothing more to do
        }

        private string PeekNext()
        {
            if (pos < 0)
                // pos < 0 indicates that there are no more tokens
                return null;
            if (pos < tokens.Length)
            {
                if (tokens[pos].Length == 0)
                {
                    ++pos;
                    return PeekNext();
                }
                return tokens[pos];
            }
            string line = reader.ReadLine();
            if (line == null)
            {
                // There is no more data to read
                pos = -1;
                return null;
            }
            // Split the line that was read on white space characters
            tokens = line.Split(null);
            pos = 0;
            return PeekNext();
        }

        public bool HasNext()
        {
            return (PeekNext() != null);
        }

        public string Next()
        {
            string next = PeekNext();
            if (next == null)
                throw new NoMoreTokensException();
            ++pos;
            return next;
        }
    }

    public class NoMoreTokensException : Exception
    {
    }
}
