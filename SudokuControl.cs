using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class SudokuControl : UserControl
    {
        public SudokuViewModel ViewModel { get; } = new SudokuViewModel();

        public SudokuControl()
        {
            InitializeComponent();

            this.sudokuTile1.Init(this.ViewModel, 0);
            this.sudokuTile2.Init(this.ViewModel, 1);
            this.sudokuTile3.Init(this.ViewModel, 2);
            this.sudokuTile4.Init(this.ViewModel, 3);
            this.sudokuTile5.Init(this.ViewModel, 4);
            this.sudokuTile6.Init(this.ViewModel, 5);
            this.sudokuTile7.Init(this.ViewModel, 6);
            this.sudokuTile8.Init(this.ViewModel, 7);
            this.sudokuTile9.Init(this.ViewModel, 8);
        }
    }
}
