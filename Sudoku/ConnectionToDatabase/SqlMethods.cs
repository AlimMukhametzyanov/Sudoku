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

        //Absolute and relative connectionString
        SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Game.mdf;Integrated Security=True");
        //SqlConnection sc = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Source\Repos\Sudoku\Sudoku\Game.mdf;Integrated Security=True");

        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        private int FindIdOfLastRow()
        {
            int id = 0;
            try
            {
                sc.Open();
                cmd.Connection = sc;

                cmd.CommandText = "SELECT MAX (Id) FROM Solution";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (dr.IsDBNull(0))
                    {
                        id = 0;
                    }
                    else
                    {
                        id = int.Parse(dr[0].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dr.Close();
                sc.Close();
            }

            return id;
        }

        public int ReturnIdOfLastRow()
        {
            return FindIdOfLastRow();
        }

        public void OnCreateNewGame(string solution, string game, string name, out int id)
        {
            id = 0;

            try
            {
                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = "INSERT INTO Solution (solution) VALUES ('" + solution + "')";
                cmd.ExecuteNonQuery();
                sc.Close();

                id = FindIdOfLastRow();

                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = "INSERT INTO Game (name, game, data_of_creation, last_alteration, time, solution_id) VALUES (@name, @game, @date1, @date2, @time, @sol_id)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@game", game);
                cmd.Parameters.AddWithValue("@date1", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@date2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@time", 0);
                cmd.Parameters.AddWithValue("@sol_id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sc.Close();
            }
        }

        public bool LoadConcreteGame(ref string solution, ref string game, ref string name, ref string time, ref int id)
        {
            //проверка на наличие игр базе
            if (FindIdOfLastRow() == 0)
                return false;

            //если нужно выбрать последнюю игру
            if (id == -1)
                id = FindIdOfLastRow();
            try
            {
                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = "SELECT * FROM Solution WHERE Id = " + id;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                    solution = dr["solution"].ToString();

                dr.Close();
                sc.Close();

                sc.Open();
                cmd.CommandText = "SELECT * FROM Game WHERE Id = " + id;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    game = dr["game"].ToString();
                    id = int.Parse(dr["Id"].ToString());
                    name = dr["name"].ToString();
                    time = dr["time"].ToString();
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dr.Close();
                sc.Close();
            }

            return true;
        }

        public List<GameInfo> LoadAllGames()
        {
            List<GameInfo> list = new List<GameInfo>();

            if (FindIdOfLastRow() == 0)
            {
                return null;
            }
            else
            {
                try
                {
                    sc.Open();
                    cmd.Connection = sc;
                    cmd.CommandText = "SELECT Id, name, data_of_creation, last_alteration, time FROM Game";
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            list.Add(new GameInfo(int.Parse(dr["Id"].ToString()), dr["name"].ToString(),
                                dr["last_alteration"].ToString(),
                                dr["time"].ToString()));
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    dr.Close();
                    sc.Close();
                }
            }

            return list;
        }

        public void SaveGame(string game, int id, string time)
        {
            try
            {
                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = "UPDATE Game SET game = '" + game + "', last_alteration = '" + DateTime.Now + "', time = '" + time + "' WHERE id = '" + id + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sc.Close();
            }
        }

        public void DeleteGame(int id)
        {
            try
            {
                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = @"DELETE FROM Game WHERE Id = '" + id + "' DELETE FROM Solution WHERE Id = '" + id + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sc.Close();
            }
        }

        public void ClearAllData()
        {
            try
            {
                sc.Open();
                cmd.Connection = sc;
                cmd.CommandText = @"TRUNCATE TABLE Game   
                                ALTER TABLE Game DROP CONSTRAINT [FK_Game_ToSolution]
                                TRUNCATE TABLE Solution
                                ALTER TABLE Game ADD CONSTRAINT [FK_Game_ToSolution] FOREIGN KEY ([solution_id]) REFERENCES [dbo].[Solution] ([Id])";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sc.Close();
            }
        }
    }
}
