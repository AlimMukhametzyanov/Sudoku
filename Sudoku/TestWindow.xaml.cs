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
        string difficulty = "simple";

        SqlMethods sqlMethods = new SqlMethods();
            
        public TestWindow()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            sqlMethods.OnCreateNewGame("111", "2222", "", difficulty, out id);
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
                sv.ShowDialog();

                Dispatcher.Invoke(() =>
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string str = String.Format("{0} | {1} | {2} | {3}", list[i]._name, list[i]._lastAlteration, list[i]._difficulty, list[i]._timePassed);
                            sv.cmbSetOfGames.Items.Add(str);
                        }
                        sv.cmbSetOfGames.SelectedIndex = 0;
                    });
            }
        }

        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {
            sqlMethods.SaveGame(current_game, id);
        }
    }
}
