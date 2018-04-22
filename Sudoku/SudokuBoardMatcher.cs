namespace Sudoku
{
    /// <summary>
    /// This class will try to check if one sudoku board 
    /// can be derived from the other
    /// </summary>                         oldBoard
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
                oldBoard = oldBoard.RotateCounterClockwise();
            }

            return false;
        }
    }
}
