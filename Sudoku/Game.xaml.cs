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

        public Game()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Test data
            current_game = "9,*,8,7,*,1,*,3,2;";
            id = 2;
            int time = 37;

            sqlMethods.SaveGame(current_game, id, time);
            MessageBox.Show("Update is completed");
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
