using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Sudoku.SudokuLogic;
using System.Collections.Generic;
namespace Sudoku.Tests
{
    [TestClass]
    public class SudokuCellTests
    {
        private Cell GetTestCell()
        {
            return new Cell(1, 3, new List<int> { 1, 2, 3 });
        }

        #region Для Number
        [TestMethod]
        public void Can_Substitute_Number()
        {
            var cell = GetTestCell();
            cell.SetValue(3);
            Assert.AreEqual(3, cell.Number);
        }
        [TestMethod]
        public void Can_Not_Substitute_With_An_Unsuitable_Number()
        {
            var cell = GetTestCell();
            cell.SetValue(0);
            Assert.AreEqual(1, cell.Number);
        }
        #endregion

        #region Для TrueNumber
        [TestMethod]
        public void Can_Substitute_TrueNumber()
        {
            var cell = GetTestCell();
            cell.TrueNumber = 7;
            Assert.AreEqual(7, cell.TrueNumber);
        }
        [TestMethod]
        public void Can_Not_Substitute_With_An_Unsuitable_TrueNumber()
        {
            var cell = GetTestCell();
            cell.TrueNumber = -3;
            Assert.AreEqual(3, cell.TrueNumber);
        }
        #endregion

        #region Для ProbNumbers
        [TestMethod]
        public void Can_Add_New_Values_To_Prob_Numbers()
        {
            var cell = GetTestCell();
            int a = cell.ProbNumbers.Count;
            cell.AddProb(4);
            Assert.AreEqual(cell.ProbNumbers.Count, a + 1);
        }
        [TestMethod]
        public void Can_Not_Add_Existing_Or_Unsuitable_Values_To_Prob_Numbers()
        {
            var cell = GetTestCell();
            int a = cell.ProbNumbers.Count;
            cell.AddProb(3);
            Assert.AreEqual(3, a);
        }

        #endregion

    }
}
