using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public int id;
        static public string solution;
        static public string current_game;
        public string difficulty;
        static public string name;

        SqlMethods sqlMethods = new SqlMethods();

        public MainWindow()
        {
            InitializeComponent();

            //Тестовые данные
            solution = "1,2,3,4,5,6,7,8,9;";
            current_game = "1,*,*,4,5,6,*,*,9;";
            difficulty = "simple";
            name = null;
        }


        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Start start = new Start();
            start.ShowDialog();

        }

        private void btnLastGame_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            sqlMethods.LoadLastGame(ref solution, ref current_game, ref id);

            //переход к Game.xaml
            Game g = new Game();
            g.ShowDialog();
            this.Close();
        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            List<GameInfo> list = sqlMethods.LoadAllGames();

            if (list == null)
            {
                MessageBox.Show("Нет сохраненных игр:(");
            }
            else
            {
                this.Hide();
                SavedGames sv = new SavedGames();

                Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string str = String.Format("{0} | {1} | {2} | {3}", list[i]._name, list[i]._lastAlteration, list[i]._difficulty, list[i]._timePassed);
                        sv.cmbSetOfGames.Items.Add(str);
                    }
                    sv.cmbSetOfGames.SelectedIndex = 0;
                });

                sv.ShowDialog();
            }
        }
    }
}
