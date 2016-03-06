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

        public void OnCreateNewGame(string solution, string game, string difficulty, out int id)
        {
            id = 1;
            sc.Open();
            cmd.Connection = sc;

            //узнать
            cmd.CommandText = "SELECT * FROM Solution";

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    id++;
                }
            }

            dr.Close();

            cmd.CommandText = "INSERT INTO Solution (Id, solution) VALUES ('" + id + "', '" + solution + "')";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Game (Id, game, data_of_creation, last_alteration, solution_id, time) VALUES ('"+id+"','"+game+"','"+DateTime.Now+"','"+DateTime.Now+"','"+id+"','"+0+"')";
            cmd.ExecuteNonQuery();
            //cmd.Clone();
            MessageBox.Show("Insert is done!");
            sc.Close();
        }
    }
}
