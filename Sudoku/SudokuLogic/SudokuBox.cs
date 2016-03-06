using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.SudokuLogic
{
    class SudokuBox
    {
        #region Текущая таблица
        private Cell[,] cells;

        public Cell[,] Cells
        {
            get { return cells; }
            set { cells = value; }
        }
        #endregion
        #region Решение
        private Cell[,] solution;

        public Cell[,] Solution
        {
            get { return solution; }
            set { solution = value; }
        }
        #endregion
        #region Начальная таблица
        private Cell[,] startBox;

        public Cell[,] StartBox
        {
            get { return startBox; }
            set { startBox = value; }
        }
        #endregion

        public SudokuBox(Cell[,] _cells, Cell[,] _solution, Cell[,] _startBox)
        {
            cells = _cells;
            solution = _solution;
            startBox = _startBox;
        }
    }
}
