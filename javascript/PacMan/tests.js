let maze = null;

QUnit.module( "MazeTests", {
	beforeEach: (assert) => {
		maze = new Maze(5,5);
	},
   	afterEach: (assert) => {  
   		let pacmans = 0; 
		for(let row=1; row<=maze.rows;row++){
			for(let col=1; col<=maze.columns;col++){
				if (maze.isPacManAt(row,col))pacmans++;
			}
		}

		// actual, expected
		assert.equal(pacmans, 1, "onlyOnePacMan");
	}
});

QUnit.test( "PacManLooksUpInCenterOfMaze", (assert) => {
	// actual, expected
	assert.ok(maze.isPacManAt(3,3), "isPacManAt(3,3)");
});

QUnit.test( "PacManMovesUp", function(assert) {
	maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(2,3), "isPacManAt(2,3)");
	assert.ok(maze.isPacManLookingUp(), "isPacManLookingUp");
	assert.ok(maze.isEmptyAt(3, 3), "isEmptyAt(3, 3)");
});

QUnit.test( "PacManWrapsAroundWhenAtTop", (assert) => {
	maze.tick();
    maze.tick();
    maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(5, 3), 'isPacManAt(5, 3)');
	assert.ok(maze.isPacManLookingUp(), 'isPacManLookingUp');
	assert.ok(maze.isEmptyAt(3, 3), 'isEmptyAt(3, 3)');
	assert.ok(maze.isEmptyAt(2, 3), 'isEmptyAt(2, 3)');
	assert.ok(maze.isEmptyAt(1, 3), 'isEmptyAt(1, 3)');	
});

QUnit.test( "TickLeftTickCheck", (assert) => {
	maze.tick();
    maze.pacManLeft();
    maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(2, 2), 'isPacManAt(2, 2)');
	assert.ok(maze.isPacManLookingLeft(), 'isPacManLookingLeft');
});

QUnit.test( "PacManMovesUpAndBackAgain", (assert) => {
	maze.pacManDown();
    maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(4, 3), 'isPacManAt(4, 3)');
	assert.ok(maze.isPacManLookingDown(), 'isPacManLookingDown');
});

QUnit.test( "PacManMovesLeftAndBackAgain", (assert) => {
	maze.pacManLeft();
    maze.tick();
    maze.pacManRight();
    maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 3), 'isPacManAt(3, 3)');
	assert.ok(maze.isPacManLookingRight(), 'isPacManLookingRight');
});

QUnit.test( "PacManChangesDirectionLeft", (assert) => {
	maze.pacManLeft();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 3), 'isPacManAt(3, 3)');
	assert.ok(maze.isPacManLookingLeft(), 'isPacManLookingLeft');
});

QUnit.test( "PacManMovesLeft", (assert) => {
	maze.pacManLeft();
	maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 2), 'isPacManAt(3, 2)');
	assert.ok(maze.isPacManLookingLeft(), 'isPacManLookingLeft');
	assert.ok(maze.isEmptyAt(3, 3), 'isEmptyAt(3, 3)');
});

QUnit.test( "PacManWrapsAroundLeft", (assert) => {
	maze.pacManLeft();
	maze.tick();
	maze.tick();
	maze.tick();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 5), 'isPacManAt(3, 5)');
	assert.ok(maze.isPacManLookingLeft(), 'isPacManLookingLeft');
	assert.ok(maze.isEmptyAt(3, 3), 'isEmptyAt(3, 3)');
	assert.ok(maze.isEmptyAt(3, 2), 'isEmptyAt(3, 2)');
	assert.ok(maze.isEmptyAt(3, 1), 'isEmptyAt(3, 1)');
});

QUnit.test( "PacManChangesDirectionDown", (assert) => {
	maze.pacManDown();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 3), 'isPacManAt(3, 3)');
	assert.ok(maze.isPacManLookingDown(), 'isPacManLookingDown');
});

QUnit.test( "PacManChangesDirectionRight", (assert) => {
	maze.pacManRight();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 3), 'isPacManAt(3, 3)');
	assert.ok(maze.isPacManLookingRight(), 'isPacManLookingRight');
});


QUnit.test( "PacManChangesDirectionUp", (assert) => {
	maze.pacManDown();
    maze.pacManUp();
	// actual, expected
	assert.ok(maze.isPacManAt(3, 3), 'isPacManAt(3, 3)');
	assert.ok(maze.isPacManLookingUp(), 'isPacManLookingUp');
});
