using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public class SudokuViewModel : INotifyPropertyChanged
    {
        #region Fields
        private SudokuCell selectedCell;
        #endregion

        #region Properties
        public SudokuBoard Board { get; } = new SudokuBoard();
        public SudokuCell SelectedCell
        {
            get => this.selectedCell;
            set
            {
                if (this.selectedCell != value)
                {
                    this.selectedCell = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedCell)));
                }
            }
        }

        public IEnumerable<SudokuCell> InvalidCells { get; private set; } = Enumerable.Empty<SudokuCell>();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Ctor/dtor
        public SudokuViewModel()
        {
            this.Board.Init(Program.SEED);
            this.SelectedCell = this.Board.GetCell(0, 0);

            this.Board.CellValueChanged += Board_CellValueChanged;
            this.SelectedCell.Value = '9';
        }

        private void Board_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            this.InvalidCells = this.Board.GetInvalidCells().ToArray();
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.InvalidCells)));
        }
        #endregion

        #region Public methods
        public void MoveSelection(Keys direction)
        {
            switch (direction)
            {
                case Keys.Left:
                    {
                        var col = this.SelectedCell.Col - 1;
                        if (col > -1)
                        {
                            this.SelectedCell = this.Board.GetCell(this.SelectedCell.Row, col);
                        }
                    }
                    break;
                case Keys.Right:
                    {
                        var col = this.SelectedCell.Col + 1;
                        if (col < SudokuBoard.SIZE)
                        {
                            this.SelectedCell = this.Board.GetCell(this.SelectedCell.Row, col);
                        }
                    }
                    break;
                case Keys.Up:
                    {
                        var row = this.SelectedCell.Row - 1;
                        if (row > -1)
                        {
                            this.SelectedCell = this.Board.GetCell(row, this.SelectedCell.Col);
                        }
                    }
                    break;
                case Keys.Down:
                    {
                        var row = this.SelectedCell.Row + 1;
                        if (row < SudokuBoard.SIZE)
                        {
                            this.SelectedCell = this.Board.GetCell(row, this.SelectedCell.Col);
                        }
                    }
                    break;
                case Keys.PageUp:
                    if(this.SelectedCell.Row > 0)
                    {
                        this.SelectedCell = this.Board.GetCell(0, this.SelectedCell.Col);
                    }
                    break;
                case Keys.PageDown:
                    if (this.SelectedCell.Row < SudokuBoard.SIZE - 1)
                    {
                        this.SelectedCell = this.Board.GetCell(SudokuBoard.SIZE - 1, this.SelectedCell.Col);
                    }
                    break;
                case Keys.Home:
                    if(this.SelectedCell.Col > 0)
                    {
                        this.SelectedCell = this.Board.GetCell(this.SelectedCell.Row, 0);
                    }
                    break;
                case Keys.End:
                    if (this.SelectedCell.Col < SudokuBoard.SIZE - 1)
                    {
                        this.SelectedCell = this.Board.GetCell(this.SelectedCell.Row, SudokuBoard.SIZE - 1);
                    }
                    break;
            }
        }
        #endregion
    }
}
