using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class SudokuGenerator
    {
        public static SudokuBoard Generate()
        {
            var board = new SudokuBoard();
            var rnd = new Random();

            // fill in all numbers
            for (char value = '1'; value <= '1' + SudokuBoard.SIZE; ++value)
            {
                // fill in each tile
                for (int tile = 0; tile < SudokuBoard.SIZE; ++tile)
                {
                    // get all valid positions for this number
                    var cells = board.GetTile(tile).Where(item => item.Value == SudokuBoard.EMPTY).ToList();

                    // filter invalid rows
                    var rows = cells.Select(item => item.Row).Distinct().ToList();
                    foreach (var row in rows)
                    {
                        if (board.GetRow(row).Any(item => item.Value == value))
                        {
                            // remove all cells of that row
                            cells.RemoveAll(item => item.Row == row);
                        }
                    }

                    // filter invalid cols
                    var cols = cells.Select(item => item.Col).Distinct().ToList();
                    foreach (var col in cols)
                    {
                        if (board.GetColumn(col).Any(item => item.Value == value))
                        {
                            // remove all cells of that column
                            cells.RemoveAll(item => item.Col == col);
                        }
                    }

                    // we need to have at least one valid position
                    Debug.Assert(cells.Any());
                    if(!cells.Any())
                    {
                        continue;
                    }

                    // pick one random cell
                    var pos = rnd.Next(cells.Count());
                    cells[pos].Value = value;
                }
            }

            return board;
        }
    }
}
