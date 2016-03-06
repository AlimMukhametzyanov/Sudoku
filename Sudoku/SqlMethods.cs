using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Sudoku
{
    class SqlMethods
    {
        public SqlMethods()
        {
            string strPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            strPath = strPath.Substring(6, strPath.Length - 15);
            AppDomain.CurrentDomain.SetData("DataDirectory", strPath);
        }

        SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Game.mdf;Integrated Security=True");

        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        private int FindLastID()
        {
            int id = 0;
            cmd.Connection = sc;

            cmd.CommandText = "SELECT * FROM Solution";

            //узнаем количество строк в таблице, чтобы определить id новой записи
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    id++;
                }
            }

            dr.Close();

            return id;
        }

        public void OnCreateNewGame(string solution, string game, string difficulty, out int id)
        {
            sc.Open();
            cmd.Connection = sc;

            id = FindLastID() + 1;

            //сохранение решения
            cmd.CommandText = "INSERT INTO Solution (Id, solution) VALUES ('" + id + "', '" + solution + "')";
            cmd.ExecuteNonQuery();

            //сохранение текущей игры
            cmd.CommandText = "INSERT INTO Game (Id, game, data_of_creation, last_alteration, solution_id, time) VALUES ('"+id+"','"+game+"','"+DateTime.Now+"','"+DateTime.Now+"','"+id+"','"+0+"')";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Insert is done!");
            sc.Close();
        }

        public void LoadLastGame(ref string solution, ref string game, ref int id)
        {
            sc.Open();
            cmd.Connection = sc;

            cmd.CommandText = "SELECT Id, game, data_of_creation, last_alteration, solution_id, time FROM Game WHERE Id = " + FindLastID();
            dr = cmd.ExecuteReader();

            string result = "";

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //result += String.Format("{0} {1} {2} {3} {4} {5}\r\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                    result += String.Format("{0}\r\n {1}\r\n {2}\r\n {3}\r\n {4}\r\n", dr["Id"], dr["game"], dr["data_of_creation"], dr["last_alteration"], dr["solution_id"], dr["time"]);
                }
            }

            MessageBox.Show(result);

            sc.Close();
        }

        public void LoadAllGames()
        {

        }

        public void SaveGame(string game, string id)
        {

        }
    }
}
