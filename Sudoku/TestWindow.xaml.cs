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
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        int id;
        string solution;
        string current_game;
        string difficulty;
        string name;

        SqlMethods sqlMethods = new SqlMethods();
            
        public TestWindow()
        {
            InitializeComponent();
            solution = "1,2,3,4,5,6,7,8,9;";
            current_game = "1,*,*,4,5,6,*,*,9;";
            difficulty = "simple";
            name = null;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            sqlMethods.OnCreateNewGame(solution, current_game, name, difficulty, out id);
        }

        private void btnLastGame_Click(object sender, RoutedEventArgs e)
        {
            sqlMethods.LoadLastGame(ref solution, ref current_game, ref id);
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

        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            //Test data
            current_game="9,*,8,7,*,1,*,3,2;";
            id = 2;
            int time = 37;

            sqlMethods.SaveGame(current_game, id, time);
            MessageBox.Show("Update is completed");
        }
    }
}
