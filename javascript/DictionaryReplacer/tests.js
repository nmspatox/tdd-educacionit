QUnit.module( "DictionaryReplacerTests" );
QUnit.test( "EsNulo", function( assert ) {
	let dic = null;
	let obj = new DictionaryReplacer();
	let result = obj.replace('', dic);

	// actual, expected
	assert.equal(result, '', "Passed!" );
});

QUnit.test( "EstaVacio", function( assert ) {
	let dic = {};
	let obj = new DictionaryReplacer();
	let result = obj.replace('', dic);

	// actual, expected
	assert.equal(result, '', "Passed!" );
});

QUnit.test( "MuestraNombre", function( assert ) {
	let texto = '$nombre$';
	let dic = { 'nombre' : 'Juan' };
	let obj = new DictionaryReplacer();
	let result = obj.replace(texto, dic);

	// actual, expected
	assert.equal(result, 'Juan', "Passed!" );
});

QUnit.test( "MuestraNombreYEdad", function( assert ) {
	let texto = '$nombre$ tiene $edad$ años';
	let dic = { 
		'nombre' : 'Juan',
		'edad' : '27'
	};
	let obj = new DictionaryReplacer();
	let result = obj.replace(texto, dic);

	// actual, expected
	assert.equal(result, 'Juan tiene 27 años', "Passed!" );
});

QUnit.test( "MuestraTextoOriginal", function( assert ) {
	let texto = 'texto sin ninguna clave';
	let dic = { 
		'nombre' : 'Juan',
		'edad' : '27'
	};
	let obj = new DictionaryReplacer();
	let result = obj.replace(texto, dic);

	// actual, expected
	assert.equal(result, texto, "Passed!" );
});

