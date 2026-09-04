using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuCell : INotifyPropertyChanged
    {
        #region Constants
        public const char EMPTY = char.MinValue;
        #endregion

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
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
                }
            }
        }
        public bool IsEmpty => this.value == EMPTY;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
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
