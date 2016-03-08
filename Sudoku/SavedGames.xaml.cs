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
    /// Interaction logic for SavedGames.xaml
    /// </summary>
    public partial class SavedGames : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

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

            this.Close();

            Game game = new Game();
            game.Show();

            sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref id);

            MainParams.solution = solution;
            MainParams.current_game = current_game;
            MainParams.id = id;
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
    }
}
