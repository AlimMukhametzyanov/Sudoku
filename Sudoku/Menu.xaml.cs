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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        SqlMethods sqlMethods = new SqlMethods();
        public Menu()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var result1 = MessageBox.Show("Вы действительно хотите очистить все сохраненные данные, в том числе игры?", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result1 == MessageBoxResult.Yes)
            {
                var result2 = MessageBox.Show("Вы уверены? Абсолютно все данные будут потеряны!", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Stop);

                if (result2 == MessageBoxResult.Yes)
                    sqlMethods.ClearAllData();
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Как же пользоваться нашей программой? Все очень просто...", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            string msg1 = "Создател этой программы - студенты второго курса факультета БиМа школы Бизнес-информатики.\r\n";
            string msg2 = "Татьяна Тепеницина 143(1)\r\nМухаметзянов Алимбек 143(2)\r\nДмитрий Елисеев 143(2)";
            MessageBox.Show(msg1 + msg2, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
