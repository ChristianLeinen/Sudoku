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
    public partial class SudokuTile : UserControl
    {
        private SudokuViewModel viewModel;
        private int tile;

        public SudokuTile()
        {
            InitializeComponent();
        }

        internal void Init(SudokuViewModel viewModel, int tile)
        {
            this.viewModel = viewModel;
            this.tile = tile;

            var cells = viewModel.Board.GetTile(tile).ToArray();
            this.label1.Tag = cells[0];
            this.label1.Text = cells[0].Value.ToString();
            this.label2.Tag = cells[1];
            this.label2.Text = cells[1].Value.ToString();
            this.label3.Tag = cells[2];
            this.label3.Text = cells[2].Value.ToString();
            this.label4.Tag = cells[3];
            this.label4.Text = cells[3].Value.ToString();
            this.label5.Tag = cells[4];
            this.label5.Text = cells[4].Value.ToString();
            this.label6.Tag = cells[5];
            this.label6.Text = cells[5].Value.ToString();
            this.label7.Tag = cells[6];
            this.label7.Text = cells[6].Value.ToString();
            this.label8.Tag = cells[7];
            this.label8.Text = cells[7].Value.ToString();
            this.label9.Tag = cells[8];
            this.label9.Text = cells[8].Value.ToString();

            this.viewModel.SelectedCellChanged += this.SelectedCellChanged;

            foreach (var cell in cells)
            {
                cell.ValueChanged += this.CellValueChanged;
            }

            this.UpdateSelection();
        }

        private void UpdateSelection()
        {
            foreach (var lbl in this.tableLayoutPanel1.Controls.OfType<Label>())
            {
                if (lbl.Tag == this.viewModel.SelectedCell)
                {
                    lbl.BackColor = SystemColors.ActiveCaption;
                }
                else
                {
                    lbl.BackColor = SystemColors.Control;
                }
            }
        }

        private void SelectedCellChanged(object sender, EventArgs e) => this.UpdateSelection();

        private void CellValueChanged(object sender, EventArgs e)
        {
            if (!(sender is SudokuCell cell))
                return;
            foreach (var item in tableLayoutPanel1.Controls.OfType<Label>())
            {
                if (item.Tag == sender)
                {
                    item.Text = cell.Value.ToString();
                }
            }
        }

        private void label_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is Label lbl)
            {
                Debug.Assert(lbl.Tag is SudokuCell);
                this.viewModel.SelectedCell = lbl.Tag as SudokuCell;
            }
        }

        private void SudokuTile_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                // single step navigation
                case Keys.Left:
                case Keys.A:
                    if (e.Control)
                    {
                        Trace.WriteLine("Move by tile!");
                    }
                    this.viewModel.MoveSelection(Keys.Left);
                    break;
                case Keys.Up:
                case Keys.W:
                    this.viewModel.MoveSelection(Keys.Up);
                    break;
                case Keys.Right:
                case Keys.D:
                    this.viewModel.MoveSelection(Keys.Right);
                    break;
                case Keys.Down:
                case Keys.S:
                    this.viewModel.MoveSelection(Keys.Down);
                    break;
                // multi step navigation
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                    this.viewModel.MoveSelection(e.KeyCode);
                    break;
                // values
                case Keys.D1:
                    break;
                case Keys.D2:
                    break;
                case Keys.D3:
                    break;
                case Keys.D4:
                    break;
                case Keys.D5:
                    break;
                case Keys.D6:
                    break;
                case Keys.D7:
                    break;
                case Keys.D8:
                    break;
                case Keys.D9:
                    break;
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                case Keys.NumPad3:
                    break;
                case Keys.NumPad4:
                    break;
                case Keys.NumPad5:
                    break;
                case Keys.NumPad6:
                    break;
                case Keys.NumPad7:
                    break;
                case Keys.NumPad8:
                    break;
                case Keys.NumPad9:
                    break;
                // clear value
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.Space:
                case Keys.Back:
                case Keys.Delete:
                    this.viewModel.SelectedCell.Value = SudokuBoard.EMPTY;
                    break;
            }
        }
    }
}
