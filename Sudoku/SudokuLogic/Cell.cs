using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.SudokuLogic
{
    public class Cell
    {
        #region Значение клетки
        private int? number;

        public int? Number
        {
            get { return number; }
            set
            {
                if (value > 9 || value == 0)
                { return; }
                else
                { number = value; }
            }
        }
        #endregion

        #region Истинное начение клетки
        private int? trueNumber;

        public int? TrueNumber
        {
            get { return trueNumber; }
            set
            {
                if (value > 9 || value == 0)
                { trueNumber = null; }
                else
                { trueNumber = value; }
            }
        }
        #endregion

        #region Возможные значения, добавляемые пользователем
        private int?[] probNumbers;

        public int?[] ProbNumbers
        {
            get { return probNumbers; }
            set { probNumbers = value; }
        }
        
	
        #endregion
        #region Конструктор
        public Cell(int? _number, int? _trueNumber, int?[] _probNumbers)
        {
            number = _number;
            trueNumber = _trueNumber;
            probNumbers = _probNumbers;
        }
        #endregion
        public void SetValue(int? newNumber)
        {
            Number = newNumber;
        }
    }
}
