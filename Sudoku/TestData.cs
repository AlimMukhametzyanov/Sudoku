using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class TestData
    {
        public string ReadFromFile(string path)
        {
            string result = "";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                while(!sr.EndOfStream)
                {
                    result += sr.ReadLine();
                }
            }
            return result;
        }
    }
}
