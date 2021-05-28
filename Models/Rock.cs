using System;
using System.Windows.Controls;

namespace asteroids.Models
{
    public class Rock{
        public RockSizes size;
        public Image rock;
        public double xPosition;
        public double yPosition;
        public int width;
        public static Random random = new Random();

        public Rock(Image rock, RockSizes size, int width){
            this.rock = rock;
            this.size = size;
            this.width = width;
            this.xPosition = (double)random.Next(0, 990 - this.width);
            this.yPosition = -this.width;
        }

        public bool checkCollision(double shipPosition, double shipSize){
            var shipHalfSize = shipSize / 2;

            if (shipPosition > this.xPosition - shipHalfSize){
                if (shipPosition < this.xPosition + this.width + shipHalfSize){
                    if (this.yPosition + this.width > 470){
                        return true;
                    }
                }
            }
            return false;
        }

        public void changePosition(){
            this.yPosition += 5;
            this.rock.SetValue(Canvas.LeftProperty, this.xPosition);
            this.rock.SetValue(Canvas.TopProperty, this.yPosition);
        }

        public bool isOutsideBoard(){
            if (this.yPosition > 570) return true;
            else return false;
        }

        public static RandomRock randomSizeAndImage(){
            var random = new Random();
            int rockSize = random.Next(0, 4);
            int width;
            RockSizes size;

            switch (rockSize){
                case 1:
                    size = RockSizes.Small;
                    width = 50;
                    break;
                case 2:
                    size = RockSizes.Medium;
                    width = 100;
                    break;
                case 3:
                    size = RockSizes.Large;
                    width = 200;
                    break;
                default:
                    size = RockSizes.Medium;
                    width = 100;
                    break;
            }

            int rockImage = random.Next(0, 4);
            Uri image;

            switch (rockImage){
                case 1:
                    image = new Uri("Assets/asteroid1.png", UriKind.Relative);
                    break;
                case 2:
                    image = new Uri("Assets/asteroid2.png", UriKind.Relative);
                    break;
                case 3:
                    image = new Uri("Assets/doge.png", UriKind.Relative);
                    break;
                case 4:
                    image = new Uri("Assets/ufo.png", UriKind.Relative);
                    break;
                default:
                    image = new Uri("Assets/asteroid1.png", UriKind.Relative);
                    break;
            }

            return new RandomRock{
                size = size,
                image = image,
                width = width
            };
        }
    }
}
