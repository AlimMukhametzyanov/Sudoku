﻿using System;
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
            this.Hide();
            Start start = new Start();
            start.Show();
        }

        private void btnLastGame_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            string solution = null;
            string current_game = null;
            int id = -1;

            if (sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref id) == false)
                MessageBox.Show("Нет сохраненных игр", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                sqlMethods.LoadConcreteGame(ref solution, ref current_game, ref id);

            MainParams.solution = solution;
            MainParams.current_game = current_game;
            MainParams.id = id;

            Game g = new Game();
            g.Show();
        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            List<GameInfo> list = sqlMethods.LoadAllGames();

            if (list == null)
            {
                MessageBox.Show("Нет сохраненных игр:(");
            }
            else
            {
                this.Hide();

                SavedGames sv = new SavedGames();

                Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string str = String.Format("{0} | {1} | {2} | {3}", list[i]._name, list[i]._lastAlteration, list[i]._difficulty, list[i]._timePassed);
                        sv.cmbSetOfGames.Items.Add(str);
                    }
                    sv.cmbSetOfGames.SelectedIndex = 0;
                });

                sv.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти из игры?", "Sudoku", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                e.Cancel = true;
            else
            {
                App.Current.Shutdown();
            }
        }
    }
}
