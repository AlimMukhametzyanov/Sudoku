using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Sudoku.SudokuLogic;
namespace Sudoku.Tests
{
    [TestClass]
    public class SudokuCellTests
    {
        private Cell GetTestCell()
        {
            return new Cell(1,3,new uint?[]{1,2,3});
        }

        [TestMethod]
        public void Can_Substitute_Number()
        {
 
        }


    }
}
