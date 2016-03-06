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
        private uint? number;

        public uint? Number
        {
            get { return number; }
            set { number = value; }
        }
        #endregion

        #region Истинное начение клетки
        private uint? trueNumber;

        public uint? TrueNumber
        {
            get { return trueNumber; }
            set { trueNumber = value; }
        }
        #endregion

        #region Возможные значения, добавляемые пользователем
        private uint?[] probNumbers;

        public uint?[] ProbNumbers
        {
            get { return probNumbers; }
            set { probNumbers = value; }
        }
        
	
        #endregion

        public Cell(uint? _number, uint? _trueNumber, uint?[] _probNumbers)
        {
            number = _number;
            trueNumber = _trueNumber;
            probNumbers = _probNumbers;
        }
    }
}
