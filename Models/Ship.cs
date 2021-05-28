using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asteroids.Models{

    public class Ship{
        public double xPosition { get; set; }
        public double shipSize = 100;
        const int moveDistance = 10;

        public Ship(){
            this.xPosition = 445.0;
        }

        public void moveLeft(){
            if (this.xPosition - moveDistance == 0) return;
            this.xPosition -= moveDistance;
        }

        public void moveRight(){
            if (this.xPosition + moveDistance == 500) return;
            this.xPosition += moveDistance;
        }
    }
}
