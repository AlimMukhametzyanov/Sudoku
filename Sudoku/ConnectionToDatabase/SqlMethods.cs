﻿using System;
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

        //Absolute and relative connectionString
        SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Game.mdf;Integrated Security=True");
        //SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Source\Repos\Sudoku\Sudoku\Game.mdf;Integrated Security=True");

        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        private int FindLastID()
        {
            int id = 0;
            cmd.Connection = sc;

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

            return id;
        }

        public void OnCreateNewGame(string solution, string game, string name, string difficulty, out int id)
        {
            sc.Open();
            cmd.Connection = sc;

            id = FindLastID() + 1;

            cmd.CommandText = "INSERT INTO Solution (Id, solution) VALUES ('" + id + "', '" + solution + "')";
            cmd.ExecuteNonQuery();

            if (String.IsNullOrEmpty(name))
            {
                cmd.CommandText = "INSERT INTO Game (Id, name, game, data_of_creation, last_alteration, time, difficulty, solution_id) VALUES ('" + id + "','" + "game_" + id + "','" + game + "','" + DateTime.Now + "','" + DateTime.Now + "','" + 0 + "','" + difficulty + "','" + id + "')";
            }
            else
            {
                cmd.CommandText = "INSERT INTO Game (Id, name, game, data_of_creation, last_alteration, solution_id, time) VALUES ('" + id + "', '" + name + "', '" + game + "','" + DateTime.Now + "','" + DateTime.Now + "','" + 0 + "','" + difficulty + "','" + id + "')";
            }

            cmd.ExecuteNonQuery();

            MessageBox.Show("Insert is done!");
            sc.Close();
        }

        public void LoadLastGame(ref string solution, ref string game, ref int id)
        {
            sc.Open();
            cmd.Connection = sc;

            if (FindLastID() == 0)
            {
                MessageBox.Show("Не удалось найти сохраненной игры:(");
            }
            else
            {
                cmd.CommandText = "SELECT * FROM Solution WHERE Id = " + FindLastID();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                    solution = dr["solution"].ToString();

                dr.Close();

                cmd.CommandText = "SELECT * FROM Game WHERE Id = " + FindLastID();
                dr = cmd.ExecuteReader();

                string result = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result += String.Format("{0}\r\n {1}\r\n {2}\r\n {3}\r\n {4}\r\n {5}\r\n {6}\r\n {7}\r\n", dr["Id"], dr["name"], dr["game"], dr["data_of_creation"], dr["last_alteration"], dr["time"], dr["difficulty"], dr["solution_id"]);
                        MessageBox.Show(result);
                        game = dr["game"].ToString();
                        id = int.Parse(dr["Id"].ToString());

                    }
                }
            }

            dr.Close();
            sc.Close();
        }

        public List<GameInfo> LoadAllGames()
        {
            sc.Open();
            cmd.Connection = sc;

            List<GameInfo> list = new List<GameInfo>();

            if (FindLastID() == 0)
            {
                sc.Close();
                return null;
            }
            else
            {
                cmd.CommandText = "SELECT name, data_of_creation, last_alteration, difficulty, time FROM Game";
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Add(new GameInfo(dr["name"].ToString(),
                            dr["last_alteration"].ToString(),
                            dr["difficulty"].ToString(),
                            int.Parse(dr["time"].ToString())));
                    }
                }
            }

            sc.Close();
            return list;
        }

        public void SaveGame(string game, int id, int time)
        {
            sc.Open();
            cmd.Connection = sc;
            cmd.CommandText = "UPDATE Game SET game = '" + game + "', last_alteration = '" + DateTime.Now + "', time = '" + time + "' WHERE id = '"+id+"'";
            cmd.ExecuteNonQuery();
            sc.Close();
        }
    }
}