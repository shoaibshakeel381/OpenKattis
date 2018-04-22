using System;
using System.Collections.Generic;
using System.Diagnostics;
using Kattis.IO;

namespace Sudoku
{
    public class Program
    {
        private const bool DEBUG = true;
        internal List<SudokuBoard> CompletedBoards { get; private set; }
        internal List<SudokuBoard> IncompleteBoards { get; private set; }

        public static void Main(string[] args)
        {
            var program = new Program();

            program.TakeUserInput();

            // Just for handling JIT
            var stopWatch = Stopwatch.StartNew();
            stopWatch.Stop();

            for (var i = 0; i < program.CompletedBoards.Count; i++)
            {
                //Console.WriteLine("First Board: ");
                //Console.WriteLine(program.CompletedBoards[i].ToString());

                //Console.WriteLine("First Board (Rotated): ");
                //stopWatch = Stopwatch.StartNew();
                //Console.WriteLine(program.CompletedBoards[i].RotateClockwise().ToString());
                //stopWatch.Stop();
                //Console.WriteLine("Total Elapsed Time: " + stopWatch.ElapsedMilliseconds);

                //Console.WriteLine("First Board (Swap Row Segment 0<->2): ");
                //stopWatch = Stopwatch.StartNew();
                //Console.WriteLine(program.CompletedBoards[i].SwapRowSegment(0, 2).ToString());
                //stopWatch.Stop();
                //Console.WriteLine("Total Elapsed Time: " + stopWatch.ElapsedMilliseconds);

                //Console.WriteLine("First Board (Swap Column Segment 0<->2): ");
                //stopWatch = Stopwatch.StartNew();
                //Console.WriteLine(program.CompletedBoards[i].SwapColumnSegment(0, 2).ToString());
                //stopWatch.Stop();
                //Console.WriteLine("Total Elapsed Time: " + stopWatch.ElapsedMilliseconds);

                //Console.WriteLine("First Board (Swap Rows 8<->7): ");
                //stopWatch = Stopwatch.StartNew();
                //Console.WriteLine(program.CompletedBoards[i].SwapRows(0, 2).ToString());
                //stopWatch.Stop();
                //Console.WriteLine("Total Elapsed Time: " + stopWatch.ElapsedMilliseconds);

                //Console.WriteLine("First Board (Swap Columns 4<->6): ");
                //stopWatch = Stopwatch.StartNew();
                //Console.WriteLine(program.CompletedBoards[i].SwapColumns(0, 2).ToString());
                //stopWatch.Stop();
                //Console.WriteLine("Total Elapsed Time: " + stopWatch.ElapsedMilliseconds);

                //break;

                var matcher = new SudokuBoardMatcher();
                stopWatch = Stopwatch.StartNew();
                Console.WriteLine(matcher.Match(program.CompletedBoards[i], program.IncompleteBoards[i]) ? "Yes" : "No");
                stopWatch.Stop();
                Console.WriteLine("Total Elapsed Time: " + stopWatch.ElapsedMilliseconds);
            }
        }

        public void TakeUserInput()
        {
            CompletedBoards = new List<SudokuBoard>();
            IncompleteBoards = new List<SudokuBoard>();

            if (!DEBUG)
            {
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
            else
            {
                for (var i = 1; i <= 2; i++)
                {
                    var boardData = GetTestBoardData(i);
                    CompletedBoards.Add(new SudokuBoard(boardData.Item1));
                    IncompleteBoards.Add(new SudokuBoard(boardData.Item2));
                }
            }
        }

        private Tuple<List<string>, List<string>> GetTestBoardData(int caseIndex)
        {
            var completedBoardData = new List<string>();
            switch (caseIndex)
            {
                case 1:
                    completedBoardData.Add("963174258");
                    completedBoardData.Add("178325649");
                    completedBoardData.Add("254689731");
                    completedBoardData.Add("821437596");
                    completedBoardData.Add("496852317");
                    completedBoardData.Add("735961824");
                    completedBoardData.Add("589713462");
                    completedBoardData.Add("317246985");
                    completedBoardData.Add("642598173");
                    break;
                case 2:
                    completedBoardData.Add("534678912");
                    completedBoardData.Add("672195348");
                    completedBoardData.Add("198342567");
                    completedBoardData.Add("859761423");
                    completedBoardData.Add("426853791");
                    completedBoardData.Add("713924856");
                    completedBoardData.Add("961537284");
                    completedBoardData.Add("287419635");
                    completedBoardData.Add("345286179");
                    break;
            }

            var incompleteBoardData = new List<string>();
            switch (caseIndex)
            {
                case 1:
                    incompleteBoardData.Add("060104050");
                    incompleteBoardData.Add("200000001");
                    incompleteBoardData.Add("008305600");
                    incompleteBoardData.Add("800407006");
                    incompleteBoardData.Add("006000300");
                    incompleteBoardData.Add("700901004");
                    incompleteBoardData.Add("500000002");
                    incompleteBoardData.Add("040508070");
                    incompleteBoardData.Add("007206900");
                    break;
                case 2:
                    incompleteBoardData.Add("010900605");
                    incompleteBoardData.Add("025060070");
                    incompleteBoardData.Add("870000902");
                    incompleteBoardData.Add("702050043");
                    incompleteBoardData.Add("000204000");
                    incompleteBoardData.Add("490010508");
                    incompleteBoardData.Add("107000056");
                    incompleteBoardData.Add("040080210");
                    incompleteBoardData.Add("208001090");
                    break;
            }

            return new Tuple<List<string>, List<string>>(completedBoardData, incompleteBoardData);
        }
    }
}
