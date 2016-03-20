using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Sudoku
{
    public static class FileReader
    {
        static Random rand = new Random();
        static string solution, game;
        static public void getFile()
        {
            int a = rand.Next(1, 6);
            string str = String.Format("../..{0}", a);
            FileStream fin = new FileStream(str, FileMode.Open);
            StreamReader sin = new StreamReader(fin, Encoding.Default);//для чтения файла
            solution = sin.ReadLine();//считываем весь файл
            game = sin.ReadLine();
            sin.Close();
            fin.Close();
        }

        static public int[,] GetGame()
        {
            if (solution == null)
                getFile();
            string[] separators = new string[] { ";" };
            string[] separators2 = new string[] { ";" };
            bool check;
            string[] rows = game.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int[,] cells = new int[9, 9];
            string[] row;
            for (int i = 0; i < 9; i++)
            {
                row = rows[i].Split(separators2, StringSplitOptions.RemoveEmptyEntries);
                for (int ii = 0; ii < 9; ii++)
                {
                    check = int.TryParse(row[i], out cells[i, ii]);
                    if (check == false)
                    { cells[i, ii] = 0; }
                }
            }
            return cells;
        }

        static public int[,] GetSolution()
        {
            if (game == null)
                getFile();
            string[] separators = new string[] { ";" };
            string[] separators2 = new string[] { ";" };

            string[] rows = game.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int[,] cells = new int[9, 9];
            string[] row;
            for (int i = 0; i < 9; i++)
            {
                row = rows[i].Split(separators2, StringSplitOptions.RemoveEmptyEntries);
                for (int ii = 0; ii < 9; ii++)
                {
                    int.TryParse(row[i], out cells[i, ii]);

                }
            }
            return cells;
        }
    }
}
