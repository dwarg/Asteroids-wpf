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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace asteroids
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void startGame(object sender, RoutedEventArgs e)
        {
            var game = new Game();
            this.Hide();
            game.Show();
            game.Closed += onClosingChildWindow;
        }

        private void showResults(object sender, RoutedEventArgs e)
        {
            var results = new Results();
            this.Hide();
            results.Show();
            results.Closed += onClosingChildWindow;
        }

        private void exitGame(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void onClosingChildWindow(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
