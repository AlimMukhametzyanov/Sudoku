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
        
        static public string GetGame()
        {
            if (solution == null)
                getFile();
            return game;
        }

        static public string GetSolution()
        {
            if (game == null)
                getFile();
            return solution;
        }
    }
}
