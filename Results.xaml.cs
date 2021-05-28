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
using asteroids.Models;

namespace asteroids
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public List<Result> ResultsList { get; set; }

        private ResultsManager resultsManager { get; set; }

        public Results()
        {
            InitializeComponent();
            this.resultsManager = new ResultsManager();
            this.ResultsList = this.resultsManager.GetResults();

            DataContext = new {
                ResultsList = this.ResultsList
            };
        }
    }
}
