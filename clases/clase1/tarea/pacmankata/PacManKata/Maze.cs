using System;

namespace PacManKata
{
    public class Maze
    {
        private class PacmanPosition
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public PacmanPosition(int row, int column)
            {
                this.Row = row;
                this.Column = column;
            }
        }

        private enum PacmanDirections
        {
            Up = 1,
            Right,
            Down,
            Left
        }

        private int rows;
        private int columns;

        private PacmanPosition pacmanPosition;
        private PacmanDirections pacmanDirection;        

        public Maze(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            int middleRow = this.rows % 2 == 0? this.rows / 2 : (this.rows + 1) / 2;
            int middleColumn = this.columns % 2 == 0? this.columns / 2 : (this.columns+ 1) / 2;
            this.pacmanPosition = new PacmanPosition(middleRow, middleColumn);
            this.pacmanDirection = PacmanDirections.Up;
        }

        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }

        public bool IsPacManAt(int row, int column)
        {
            return pacmanPosition.Row == row && pacmanPosition.Column == column;
        }

        public bool IsPacManLookingDown()
        {
            return pacmanDirection == PacmanDirections.Down;
        }

        public bool IsPacManLookingUp()
        {
            return pacmanDirection == PacmanDirections.Up;
        }

        public bool IsPacManLookingLeft()
        {
            return pacmanDirection == PacmanDirections.Left;
        }

        public bool IsPacManLookingRight()
        {
            return pacmanDirection == PacmanDirections.Right;
        }

        public bool IsEmptyAt(int row, int column)
        {
            return !IsPacManAt(row, column);
        }

        public void PacManDown()
        {
            pacmanDirection = PacmanDirections.Down;
        }

        public void PacManUp()
        {
            pacmanDirection = PacmanDirections.Up;
        }

        public void PacManLeft()
        {
            pacmanDirection = PacmanDirections.Left;
        }

        public void PacManRight()
        {
            pacmanDirection = PacmanDirections.Right;
        }

        public void Tick()
        {
            switch (pacmanDirection)
            {
                case PacmanDirections.Up:
                    pacmanPosition.Row--;
                    ValidateRowPosition();
                    break;
                case PacmanDirections.Down:
                    pacmanPosition.Row++;
                    ValidateRowPosition();
                    break;
                case PacmanDirections.Right:
                    pacmanPosition.Column++;
                    ValidateColumnPosition();
                    break;                
                case PacmanDirections.Left:
                    pacmanPosition.Column--;
                    ValidateColumnPosition();
                    break;
            }
        }

        private void ValidateRowPosition()
        {
            if (pacmanPosition.Row > this.rows)
            {
                pacmanPosition.Row = 1;
            }
            else if (pacmanPosition.Row < 1)
            {
                pacmanPosition.Row = this.rows;
            }
        }

        private void ValidateColumnPosition()
        {
            if (pacmanPosition.Column > this.columns)
            {
                pacmanPosition.Column = 1;
            }
            else if (pacmanPosition.Column < 1)
            {
                pacmanPosition.Column = this.columns;
            }
        }

        public void Print()
        {
        }
    }    
}
