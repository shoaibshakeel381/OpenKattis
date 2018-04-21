using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// This class will try to check if one sudoku board 
    /// can be derived from the other
    /// </summary>
    internal class SudokuBoardMatcher
    {
        private readonly SudokuBoard _board;

        public SudokuBoardMatcher(SudokuBoard board)
        {
            _board = board;
        }

        public bool IsADerivedBoard(SudokuBoard derivedBoard)
        {
            // Try exact baord
            var result = DoBoardsMatch(_board, derivedBoard);
            if (result)
                return true;

            // Try rotating board
            for (var rotatioCount = 0; rotatioCount < 4; rotatioCount++)
            {
                var rotatedBoard = _board.RotateCounterClockwise();
                result = DoBoardsMatch(rotatedBoard, derivedBoard);
                if (result)
                    return true;
            }

            // Try swapping rows in 3x9 Region

            // Try swapping columns in 9x3 Region

            // Try swapping row regions

            // Try swapping column regions



            return false;
        }

        /// <summary>
        /// Match Two boards. NOTE: Zeros in boardB will be ignored.
        /// </summary>
        /// <param name="boardA"></param>
        /// <param name="boardB"></param>
        /// <returns></returns>
        private bool DoBoardsMatch(SudokuBoard boardA, SudokuBoard boardB)
        {
            for (var row = 0; row < SudokuBoard.MAX_CELLS; row++)
            {
                for (var col = 0; col < SudokuBoard.MAX_CELLS; col++)
                {
                    if (boardB[row, col] == 0)
                        continue;

                    if (boardA[row, col] != boardB[row, col])
                        return false;
                }
            }
            return true;
        }
    }
}
