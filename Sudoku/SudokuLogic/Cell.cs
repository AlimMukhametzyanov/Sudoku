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
                if (value > 9 || value <= 0)
                { return; }
                else
                { number = value; }
            }
        }
        #endregion

        #region Истинное значение клетки
        private int? trueNumber;

        public int? TrueNumber
        {
            get { return trueNumber; }
            set
            {
                if (value > 9 || value <= 0)
                { return; }
                else
                { trueNumber = value; }
            }
        }
        #endregion

        #region Возможные значения, добавляемые пользователем
        private List<int> probNumbers;

        public List<int> ProbNumbers
        {
            get { return probNumbers; }
            set { probNumbers = value; }
        }

        #endregion
        #region Конструктор
        public Cell(int? _number, int? _trueNumber, List<int> _probNumbers)
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

        public void AddProb(int newProb)
        {
            int a = probNumbers.Count;
            for (int i = 0; i < a; i++)
            {
                if (probNumbers[i] == newProb)
                    return;
                if (newProb <= 0 || newProb > 9)
                    return;
                else
                    probNumbers.Add(newProb);
            }
        }
    }
}
