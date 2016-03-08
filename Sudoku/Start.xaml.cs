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
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        SqlMethods sqlMethods = new SqlMethods();
        Game game = new Game();

        string current_game = MainParams.current_game;
        int id = MainParams.id;
        string solution = MainParams.solution;

        string difficulty;

        public Start()
        {
            InitializeComponent();
            cmbDifficulty.Items.Add("Простая");
            cmbDifficulty.Items.Add("Средняя");
            cmbDifficulty.Items.Add("Сложная");
            cmbDifficulty.SelectedIndex = 0;
            tbName.Text = "game_" + (sqlMethods.FindLastID() + 1).ToString();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(cmbDifficulty.SelectedItem!=null)
            {
                difficulty = cmbDifficulty.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Выберите, пожалуйста, сложность!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.Hide();

            if (String.IsNullOrEmpty(tbName.Text))
            {
                sqlMethods.OnCreateNewGame(solution, current_game, null, difficulty, out id);
                game.ShowDialog();
            }
            else
            {
                sqlMethods.OnCreateNewGame(solution, current_game, tbName.Text, difficulty, out id);
                game.ShowDialog();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Данное окно нельзя закрыть!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Stop);
            e.Cancel = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Visible: ");
        }
    }
}
