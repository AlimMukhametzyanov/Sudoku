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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        int id;
        string solution;
        string current_game;
        string difficulty = "сложная";
            
        public TestWindow()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            TestData td = new TestData();
            SqlMethods sqlMethods = new SqlMethods();
            sqlMethods.OnCreateNewGame(td.ReadFromFile(@"../../solution.txt"), td.ReadFromFile(@"../../game.txt"), difficulty, out id);
        }

        private void btnLastGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveGame_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
