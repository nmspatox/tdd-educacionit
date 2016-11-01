using System;

namespace PacManKata
{
    public class Maze
    {
        private class PacManPosition
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public PacManPosition(int row, int column)
            {
                this.Row = row;
                this.Column = column;
            }
        }

        private enum PacManDirections
        {
            Up = 1,
            Right,
            Down,
            Left
        }

        private int rows;
        private int columns;

        private PacManPosition pacManPosition;
        private PacManDirections pacManDirection;        

        public Maze(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            int middleRow = this.rows % 2 == 0? this.rows / 2 : (this.rows + 1) / 2;
            int middleColumn = this.columns % 2 == 0? this.columns / 2 : (this.columns+ 1) / 2;
            this.pacManPosition = new PacManPosition(middleRow, middleColumn);
            this.pacManDirection = PacManDirections.Up;
        }

        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }

        public bool IsPacManAt(int row, int column)
        {
            return pacManPosition.Row == row && pacManPosition.Column == column;
        }

        public bool IsPacManLookingDown()
        {
            return pacManDirection == PacManDirections.Down;
        }

        public bool IsPacManLookingUp()
        {
            return pacManDirection == PacManDirections.Up;
        }

        public bool IsPacManLookingLeft()
        {
            return pacManDirection == PacManDirections.Left;
        }

        public bool IsPacManLookingRight()
        {
            return pacManDirection == PacManDirections.Right;
        }

        public bool IsEmptyAt(int row, int column)
        {
            return !IsPacManAt(row, column);
        }

        public void PacManDown()
        {
            pacManDirection = PacManDirections.Down;
        }

        public void PacManUp()
        {
            pacManDirection = PacManDirections.Up;
        }

        public void PacManLeft()
        {
            pacManDirection = PacManDirections.Left;
        }

        public void PacManRight()
        {
            pacManDirection = PacManDirections.Right;
        }

        public void Tick()
        {
            switch (pacManDirection)
            {
                case PacManDirections.Up:
                    pacManPosition.Row--;
                    ValidateRowPosition();
                    break;
                case PacManDirections.Down:
                    pacManPosition.Row++;
                    ValidateRowPosition();
                    break;
                case PacManDirections.Right:
                    pacManPosition.Column++;
                    ValidateColumnPosition();
                    break;                
                case PacManDirections.Left:
                    pacManPosition.Column--;
                    ValidateColumnPosition();
                    break;
            }
        }

        private void ValidateRowPosition()
        {
            if (pacManPosition.Row > this.rows)
            {
                pacManPosition.Row = 1;
            }
            else if (pacManPosition.Row < 1)
            {
                pacManPosition.Row = this.rows;
            }
        }

        private void ValidateColumnPosition()
        {
            if (pacManPosition.Column > this.columns)
            {
                pacManPosition.Column = 1;
            }
            else if (pacManPosition.Column < 1)
            {
                pacManPosition.Column = this.columns;
            }
        }

        public void Print()
        {
        }
    }    
}
