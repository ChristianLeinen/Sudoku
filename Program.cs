using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    internal static class Program
    {
        internal const string SEED = @"123456789
        456789123
        789123456
        234567891
        567891234
        891234567
        345678912
        678912345
        912345678";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var board = Board.FromString(SEED);
            //var complete = board.IsComplete;
            //var valid = board.IsValid();

            //board = SudokuGenerator.Generate();
            //complete = board.IsComplete;
            //valid = board.IsValid();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
