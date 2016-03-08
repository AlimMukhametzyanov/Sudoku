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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        SqlMethods sqlMethods = new SqlMethods();
        MainWindow main = new MainWindow();

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

            main.ShowDialog();
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            //Test data
            time = 37;

            sqlMethods.SaveGame(current_game, id, time);

            this.Close();

            main.ShowDialog();

        }
    }
}
