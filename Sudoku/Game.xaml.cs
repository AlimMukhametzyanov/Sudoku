using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

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

        string current_game = MainParams.current_game;
        int id = MainParams.id;

        SqlMethods sqlMethods = new SqlMethods();

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        TimeSpan initialTime;

        string timer = "";

        public Game()
        {
            InitializeComponent();
            this.Title = MainParams.name;

            dt.Tick += new EventHandler(tickTimer);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);

            initialTime = formPastTime();

            sw.Start();
            dt.Start();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
            }  

            sqlMethods.SaveGame(current_game, id, timer);

            var result = MessageBox.Show("Игра сохранена!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
                sw.Start();
            }
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            if (sw.IsRunning)
            {
                sw.Stop();
            }  

            var result = MessageBox.Show("Вы хотите сохранить игру и выйти из нее?", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.Yes)
            {
                sqlMethods.SaveGame(current_game, id, timer);

                this.Close();

                foreach (Window window in App.Current.Windows)
                {
                    if (window.Visibility == Visibility.Hidden)
                    {
                        window.Show();
                    }
                }
            }
            else
            {
                sw.Start();
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

        private void tickTimer(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                var ts = initialTime.Add(sw.Elapsed);
                timer = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                lTime.Content = timer;
            }  
        }

        private TimeSpan formPastTime()
        {
            string time = MainParams.time;

            if (String.IsNullOrEmpty(time))
                return new TimeSpan(0,0,0,0,0);

            var mass = time.Split(':');
            return new TimeSpan(0, 0, int.Parse(mass[0]), int.Parse(mass[1]), int.Parse(mass[2]));
        }
    }
}
