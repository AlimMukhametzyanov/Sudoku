using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<GameInfo> list;

        public SavedGames()
        {
            InitializeComponent();

            list = sqlMethods.LoadAllGames();

            for (int i = 0; i < list.Count; i++)
            {
                cmbSetOfGames.Items.Add(String.Format("{0} | {1} | {2} | {3}", list[i]._id, list[i]._name, list[i]._lastAlteration, list[i]._timePassed));
            }
            cmbSetOfGames.SelectedIndex = list.Count-1;

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string solution = null;
            string current_game = null;
            string time = null;

            var elements = cmbSetOfGames.SelectedItem.ToString().Split(new char[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int id = int.Parse(elements[0]);
            string name = elements[1];

            this.Close();

            sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref name, ref time, ref id);

            MainParams.solution = solution;
            MainParams.current_game = current_game;
            MainParams.id = id;
            MainParams.name = name;
            MainParams.time = time;

            Game game = new Game();
            game.Show();
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить эту игру?", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.Yes)
            {
                var elements = cmbSetOfGames.SelectedItem.ToString().Split(new char[] { '|', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(elements[0]);

                sqlMethods.DeleteGame(id);
                cmbSetOfGames.Items.Remove(id);

                this.Close();

                SavedGames sv = new SavedGames();
                sv.Show();
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
