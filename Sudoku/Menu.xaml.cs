﻿using System;
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
            string ms1 = "Как же пользоваться нашей программой? Все очень просто...Игровое поле имеет размер 9x9 клеток.";
            string ms2 = " Границы девяти блоков клеток 3x3 выделены более толстой линией. В некоторых клетках (их меньшинство) расположены цифры от одного до девяти.";
            string ms3 = " Необходимо проставить недостающие цифры в пустых клетках, исходя из простого правила - проставленная цифра не должна повторятся по вертикали";
            string ms4 = " (в столбце), горизонтали (в строке) и в своем квадрате. Каждое судоку имеет только одно однозначное решение.";
            MessageBox.Show(ms1+ms2+ms3+ms4, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            string msg1 = "Создатели этой программы - студенты второго курса НИУ ВШЭ.\r\n";
            string msg2 = "Татьяна Тепеницина\r\nМухаметзянов Алимбек\r\nДмитрий Елисеев";
            MessageBox.Show(msg1 + msg2, "Sudoku", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
