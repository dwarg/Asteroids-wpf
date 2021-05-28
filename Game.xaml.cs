using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using asteroids.Models;

namespace asteroids{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window{
        public Ship ship;
        public List<Rock> rocks = new List<Rock>();
        public DispatcherTimer dispatcher;
        public int cycles = 0;
        public int result = 0;
        public int level = 1;
        public bool isGameOver = false;
        public ResultsManager resultsManager;
        public List<Result> results;
        public bool shouldBeSaveResult { get; set; }

        public Game(){
            InitializeComponent();
            this.cycles = 0;
            this.result = 0;
            this.level = 1;
            this.ship = new Ship();
            this.dispatcher = new DispatcherTimer();
            this.dispatcher.Interval = TimeSpan.FromMilliseconds(10);
            this.dispatcher.Tick += renderStage;
            this.dispatcher.Start();
            this.shouldBeSaveResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e){
            this.KeyDown += new KeyEventHandler(moveShip);
            this.resultsManager = new ResultsManager();
            this.results = this.resultsManager.GetResults();
        }

        private void startAgain(object sender, RoutedEventArgs e){
            foreach (var rock in this.rocks){
                this.Board.Children.Remove(rock.rock);
            }

            this.checkAndSaveResult();
            this.results = this.resultsManager.GetResults();
            this.isGameOver = false;
            this.cycles = 0;
            this.result = 0;
            this.level = 1;
            this.ship = new Ship();
            this.rocks = new List<Rock>();
            this.dispatcher.Start();
        }

        public void exitGame(object sender, RoutedEventArgs e){
            this.checkAndSaveResult();
            this.resultsManager.SaveResults(this.results);
            this.Close();
        }

        private void checkAndSaveResult(){
            if (this.shouldBeSaveResult){
                var newResult = new Result();
                newResult.Points = this.result;
                newResult.Nickname = Nickname.Text;
                this.results.Add(newResult);
                this.resultsManager.SaveResults(this.results);
            }
        }

        private void renderStage(object sender, EventArgs e){
            this.cycles++;

            if (this.cycles % 100 == 0){
                this.result += 10;
                if (this.result % 100 == 0 && this.result > 0) this.level++;

                for (var i = 0; i < this.level; i++){
                    var newRock = Rock.randomSizeAndImage();
                    BitmapImage newSource = new BitmapImage(newRock.image);
                    Image newImage = new Image();
                    newImage.Source = newSource;
                    newImage.Width = newRock.width;
                    newImage.Height = newRock.width;
                    this.Board.Children.Add(newImage);
                    this.rocks.Add(new Rock(newImage, newRock.size, newRock.width));
                }
            };

            shipObject.SetValue(Canvas.LeftProperty, ship.xPosition);

            if (this.rocks != null && this.rocks.Count > 0){
                for (var i = this.rocks.Count - 1; i >= 0; i--) {
                    if (rocks[i].checkCollision(this.ship.xPosition, this.ship.shipSize)){
                        this.gameOver();
                        return;
                    }

                    rocks[i].changePosition();
                    if (rocks[i].isOutsideBoard()){
                        this.rocks.RemoveAt(i);
                    }
                }
            }

            this.updateDataContext();
        }

        public void updateDataContext(){
            DataContext = new{
                Result = this.result,
                IsGameOver = this.isGameOver,
                ShouldBeSaveResult = this.shouldBeSaveResult
            };
        }

        private void gameOver(){
            var lastResult = this.results.OrderBy(x => x.Points).First();

            if (lastResult.Points < this.result){
                this.shouldBeSaveResult = true;
            }

            this.dispatcher.Stop();
            this.isGameOver = true;
            this.updateDataContext();
        }

        private void moveShip(object sender, KeyEventArgs e){
            if (e.Key == Key.Left){
                this.ship.moveLeft();
            }

            if (e.Key == Key.Right){
                this.ship.moveRight();
            }
        }
    }
}
