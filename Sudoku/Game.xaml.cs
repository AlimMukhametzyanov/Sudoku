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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        SqlMethods sqlMethods = new SqlMethods();

        string current_game = MainParams.current_game;
        int id = MainParams.id;

        int time;

        public Game()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Test data
            time = 37;

            sqlMethods.SaveGame(current_game, id, time);

            MessageBox.Show("Игра сохранена!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы хотите сохранить игру и выйти из нее?", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                return;
            else
            {
                this.Close();

                //Test data
                time = 37;

                sqlMethods.SaveGame(current_game, id, time);

                foreach (Window window in App.Current.Windows)
                {
                    if (window.Visibility == Visibility.Hidden)
                    {
                        window.Show();
                    }
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
