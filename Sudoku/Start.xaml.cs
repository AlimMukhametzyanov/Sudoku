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

        string current_game = MainWindow.current_game;
        int id = MainWindow.id;
        string solution = MainWindow.solution;
        string difficulty;
        string tbNameDefaultText;

        public Start()
        {
            InitializeComponent();
            cmbDifficulty.Items.Add("Простая");
            cmbDifficulty.Items.Add("Средняя");
            cmbDifficulty.Items.Add("Сложная");
            cmbDifficulty.SelectedIndex = 0;
            tbName.MaxLength = 15;
            tbNameDefaultText = "game_" + (sqlMethods.FindLastID() + 1).ToString();
            tbName.Text = tbNameDefaultText;
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

            if (String.IsNullOrEmpty(tbName.Text))
            {
                sqlMethods.OnCreateNewGame(solution, current_game, null, difficulty, out id);
                game.ShowDialog();
                //this.Visibility = Visibility.Hidden;
            }
            else
            {
                sqlMethods.OnCreateNewGame(solution, current_game, tbName.Text, difficulty, out id);
                game.ShowDialog();
                //this.Visibility = Visibility.Hidden;
            }
        }
    }
}
