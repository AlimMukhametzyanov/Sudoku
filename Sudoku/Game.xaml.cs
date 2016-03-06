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
        string current_game = MainWindow.current_game;
        int id = MainWindow.id;
        
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
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            //Test data
            time = 37;

            sqlMethods.SaveGame(current_game, id, time);
        }
    }
}
