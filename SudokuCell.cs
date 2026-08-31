using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuCell
    {
        #region Fields
        private char value;
        #endregion

        #region Properties
        public int Row { get; }
        public int Col { get; }
        public int Tile { get; }
        public char Value
        {
            get => this.value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region Events
        public EventHandler ValueChanged;
        #endregion

        #region Ctor/dtor
        internal SudokuCell(int row, int col, char value = char.MinValue)
        {
            this.Row = row;
            this.Col = col;
            this.Tile = (row / 3 * 3) + (col / 3);
            this.Value = value;
        }
        #endregion
    }
}
