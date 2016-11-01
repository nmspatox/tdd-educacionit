function Maze(rows, columns){
	let PacManDirections = { up: 1, right: 2, down: 3, left: 4 };
	let middleRow = rows % 2 === 0? rows / 2 : (rows + 1) / 2;
    let middleColumn = columns % 2 === 0? columns / 2 : (columns+ 1) / 2;

	this.rows = rows;
	this.columns = columns;
	this.pacManPosition = { row: middleRow, column: middleColumn };	
    this.pacManDirection = PacManDirections.up;

	this.isPacManAt = (row, column) => this.pacManPosition.row == row && this.pacManPosition.column == column;
	this.isPacManLookingDown = () => this.pacManDirection == PacManDirections.down;
	this.isPacManLookingUp = () => this.pacManDirection == PacManDirections.up;
	this.isPacManLookingLeft = () => this.pacManDirection == PacManDirections.left;
	this.isPacManLookingRight = () => this.pacManDirection == PacManDirections.right;
	this.isEmptyAt = (row, column) => !this.isPacManAt(row, column);
	this.pacManDown = () => this.pacManDirection = PacManDirections.down;
	this.pacManUp = () => this.pacManDirection = PacManDirections.up;
	this.pacManLeft = () => this.pacManDirection = PacManDirections.left;
	this.pacManRight = () => this.pacManDirection = PacManDirections.right;
	this.tick = doTick;
	this.print = () => {};

	function doTick(){
		switch(this.pacManDirection){
			case PacManDirections.up:
                this.pacManPosition.row--;
                validatePosition('row','rows');
                break;
            case PacManDirections.down:
                this.pacManPosition.row++;
                validatePosition('row','rows');
                break;
            case PacManDirections.right:
                this.pacManPosition.column++;
                validatePosition('column', 'columns');
                break;                
            case PacManDirections.left:
                this.pacManPosition.column--;
                validatePosition('column', 'columns');
                break;
		}
	}

	let validatePosition = (pacManProp, mazeProp) => {
        if (this.pacManPosition[pacManProp] > this[mazeProp])
        {
            this.pacManPosition[pacManProp] = 1;
        }
        else if (this.pacManPosition[pacManProp] < 1)
        {
            this.pacManPosition[pacManProp] = this[mazeProp];
        }
    }	
}