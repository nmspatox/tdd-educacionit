QUnit.module( "FizzBuzzTests" );
QUnit.test( "SeMuestraNumero", function( assert ) {
	let numero = 2;
	let obj = new FizzBuzz();
	let result = obj.get(numero);

	// actual, expected
	assert.equal(result, numero.toString(), "Passed!" );
});


QUnit.test( "SeMuestraFizz", function( assert ) {
	let numero = 3;
	let obj = new FizzBuzz();
	let result = obj.get(numero);

	// actual, expected
	assert.equal(result, 'Fizz', "Passed!" );
});

QUnit.test( "SeMuestraBuzz", function( assert ) {
	let numero = 5;
	let obj = new FizzBuzz();
	let result = obj.get(numero);

	// actual, expected
	assert.equal(result, 'Buzz', "Passed!" );
});

QUnit.test( "SeMuestraFizzBuzz", function( assert ) {
	let numero = 15;
	let obj = new FizzBuzz();
	let result = obj.get(numero);

	// actual, expected
	assert.equal(result, 'FizzBuzz', "Passed!" );
});