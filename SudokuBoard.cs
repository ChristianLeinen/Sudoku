using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public class CellValueChangedEventArgs : EventArgs
    {
        public SudokuCell Cell { get; }
        internal CellValueChangedEventArgs(SudokuCell cell)
        {
            this.Cell = cell;
        }
    }

    public delegate void CellValueChangedEventHandler(object sender, CellValueChangedEventArgs e);

    public class SudokuBoard
    {
        #region constants
        public const int SIZE = 9;
        public const char EMPTY = char.MinValue;
        #endregion

        #region Fields
        private SudokuCell[] cells = new SudokuCell[SIZE * SIZE];
        #endregion

        #region Properties
        public char this[int row, int col]
        {
            get => this.cells.Where(item => item.Row == row && item.Col == col).First().Value;
            set => this.cells.Where(item => item.Row == row && item.Col == col).First().Value = value;
        }
        #endregion

        public event CellValueChangedEventHandler CellValueChanged;

        #region Ctor/dtor
        internal SudokuBoard()
        {
            for (int i = 0; i < SIZE * SIZE; ++i)
            {
                this.cells[i] = new SudokuCell(i / SIZE, i % SIZE);
                this.cells[i].PropertyChanged += (sender, e) => this.CellValueChanged?.Invoke(this, new CellValueChangedEventArgs(sender as SudokuCell));
            }

            //for (int row = 0; row < SIZE; ++row)
            //{
            //    for (int col = 0; col < SIZE; ++col)
            //    {
            //        this.cells[row * SIZE + col] = new Cell(row, col);
            //    }
            //}
        }

        public void Init(string s)
        {
            var lines = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int row = 0; row < lines.Length; ++row)
            {
                var line = lines[row].Trim();
                for (int col = 0; col < line.Length; ++col)
                {
                    var digit = line[col];
                    Debug.Assert(char.IsDigit(digit));
                    this[row, col] = digit;
                }
            }
        }

        // Example:
        // 123456789
        // 456789123
        // 789123456
        // 234567891
        // 567891234
        // 891234567
        // 345678912
        // 678912345
        // 912345678
        public static SudokuBoard FromString(string s)
        {
            var board = new SudokuBoard();
            board.Init(s);
            //var lines = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //for (int row = 0; row < lines.Length; ++row)
            //{
            //    var line = lines[row];
            //    for (int col = 0; col < line.Length; ++col)
            //    {
            //        var digit = line[col];
            //        Debug.Assert(char.IsDigit(digit));
            //        board[row, col] = digit - '0';
            //    }
            //}
            return board;
        }
        #endregion

        #region Public access methods
        public SudokuCell GetCell(int row, int col) => this.cells[row * SIZE + col];

        public IEnumerable<SudokuCell> GetRow(int row) => this.cells.Where(cell => cell.Row == row);

        public IEnumerable<SudokuCell> GetColumn(int col) => this.cells.Where(cell => cell.Col == col);

        public IEnumerable<SudokuCell> GetTile(int tile) => this.cells.Where(cell => cell.Tile == tile);

        public IEnumerable<SudokuCell> GetEmptyCells() => this.cells.Where(cell => cell.Value == EMPTY);

        public IEnumerable<SudokuCell> GetInvalidCells()
        {
            // group all non-empty cells by row and value
            // select those, that have more than one occurence
            var invalidRows = from cell in this.cells
                              where cell.Value != EMPTY
                              group cell by new { cell.Row, cell.Value } into g
                              where g.Count() > 1
                              select g;

            var invalidColumns = from cell in this.cells
                                 where cell.Value != EMPTY
                                 group cell by new { cell.Col, cell.Value } into g
                                 where g.Count() > 1
                                 select g;

            var invalidTiles = from cell in this.cells
                               where cell.Value != EMPTY
                               group cell by new { cell.Tile, cell.Value } into g
                               where g.Count() > 1
                               select g;

            return invalidRows.SelectMany(item => item.AsEnumerable()).Concat(invalidColumns.SelectMany(item => item.AsEnumerable())).Concat(invalidTiles.SelectMany(item => item.AsEnumerable())).Distinct();
        }
        #endregion

        #region Public check methods
        public bool IsComplete => this.cells.All(cell => cell.Value != EMPTY);

        /// <summary>
        /// Checks if the given set of values (either a row, column or tile) is valid, i.e. does not contain duplicates (other than empty).
        /// </summary>
        /// <param name="values">A set of values of either a row, column or tile to be checked for validity.</param>
        /// <returns>True if the given set of values does not contain duplicates (other than empty).</returns>
        public static bool IsValid(IEnumerable<char> values)
        {
            var valueSeen = new bool[SIZE];
            foreach (var item in values)
            {
                if (item == EMPTY)
                    continue;
                else if (valueSeen[item - '1'])
                    return false;
                valueSeen[item - '1'] = true;
            }
            return true;
        }

        /// <summary>
        /// Checks if the board ist valid, i.e. each cell is either empty (0) or unique within its row, column and tile.
        /// </summary>
        /// <returns>True, if the board ist valid, i.e. no row, column or tile contains duplicate values (other than empty).</returns>
        public bool IsValid()
        {
            // check rows
            for (var row = 0; row < SIZE; ++row)
            {
                var rowVals = this.GetRow(row).Select(cell => cell.Value);
                if (!IsValid(rowVals))
                    return false;
            }

            // check columns
            for (var col = 0; col < SIZE; ++col)
            {
                var colVals = this.GetColumn(col).Select(cell => cell.Value);
                if (!IsValid(colVals))
                    return false;
            }

            // check squares
            for (var tile = 0; tile < SIZE; ++tile)
            {
                var tileVals = this.GetTile(tile).Select(cell => cell.Value);
                if (!IsValid(tileVals))
                    return false;
            }
            return true;
        }
        #endregion
    }
}
