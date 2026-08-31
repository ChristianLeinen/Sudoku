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
        private SudokuViewModel viewModel = new SudokuViewModel();
        public SudokuControl()
        {
            InitializeComponent();

            this.sudokuTile1.Init(this.viewModel, 0);
            this.sudokuTile2.Init(this.viewModel, 1);
            this.sudokuTile3.Init(this.viewModel, 2);
            this.sudokuTile4.Init(this.viewModel, 3);
            this.sudokuTile5.Init(this.viewModel, 4);
            this.sudokuTile6.Init(this.viewModel, 5);
            this.sudokuTile7.Init(this.viewModel, 6);
            this.sudokuTile8.Init(this.viewModel, 7);
            this.sudokuTile9.Init(this.viewModel, 8);
        }
    }
}
