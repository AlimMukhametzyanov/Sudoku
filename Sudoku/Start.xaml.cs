using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        SqlMethods sqlMethods = new SqlMethods();

        string current_game = MainParams.current_game;
        int id;
        string solution = MainParams.solution;

        string difficulty;

        public Start()
        {
            InitializeComponent();
            cmbDifficulty.Items.Add("Простая");
            cmbDifficulty.Items.Add("Средняя");
            cmbDifficulty.Items.Add("Сложная");
            cmbDifficulty.SelectedIndex = 0;
            tbName.Text = "game_" + (sqlMethods.ReturnIdOfLastRow() + 1).ToString();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            difficulty = cmbDifficulty.SelectedItem.ToString();

            if (String.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Название для игры не может быть пустым!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Close();

                //генерация решения и игры
                //solution = Class.GetSolution();
                //current_game = Class.GenerateCurrentGame();
                sqlMethods.OnCreateNewGame(solution, current_game, tbName.Text, difficulty, out id);

                MainParams.id = id;

                Game game = new Game();
                game.Show();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            foreach (Window window in App.Current.Windows)
            {
                if (window.Visibility == Visibility.Hidden)
                {
                    window.Show();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }
    }
}
