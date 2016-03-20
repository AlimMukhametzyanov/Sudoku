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
        SqlMethods sqlMethods = new SqlMethods();

        public MainWindow()
        {
            InitializeComponent();

            //Тестовые данные
            MainParams.solution = "1,2,3,4,5,6,7,8,9;";
            MainParams.current_game = "1,*,*,4,5,6,*,*,9;";
            MainParams.name = null;
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            MainParams.time = null;
            this.Hide();
            Start start = new Start();
            start.Show();
        }

        private void btnLastGame_Click(object sender, RoutedEventArgs e)
        {
            string solution = null;
            string current_game = null;
            int id = -1;
            string name = null;
            string time = null;

            if (sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref name, ref time, ref id) == false)
                MessageBox.Show("Нет сохраненных игр:(", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref name, ref time, ref id);

                MainParams.solution = solution;
                MainParams.current_game = current_game;
                MainParams.id = id;
                MainParams.name = name;
                MainParams.time = time;

                this.Hide();
                Game g = new Game();
                g.Show();
            }
        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            if (sqlMethods.LoadAllGames() == null)
            {
                MessageBox.Show("Нет сохраненных игр:(", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                this.Hide();

                SavedGames sv = new SavedGames();
                sv.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из игры?", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                e.Cancel = true;
            else
            {
                App.Current.Shutdown();
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu n = new Menu();
            n.ShowDialog();
        }
    }
}
