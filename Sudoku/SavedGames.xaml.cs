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
    /// Interaction logic for SavedGames.xaml
    /// </summary>
    public partial class SavedGames : Window
    {
        SqlMethods sqlMethods = new SqlMethods();
        public SavedGames()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string solution = null;
            string current_game = null;
            int id = cmbSetOfGames.SelectedIndex;

            this.Hide();

            Game game = new Game();
            game.ShowDialog();

            sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref id);

            MainParams.solution = solution;
            MainParams.current_game = current_game;
            MainParams.id = id;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Данное окно нельзя закрыть!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Stop);
            e.Cancel = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
