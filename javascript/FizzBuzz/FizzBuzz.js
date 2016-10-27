function FizzBuzz(){
	// associative array to emulate dictionary
	var dic = { '3': 'Fizz', '5': 'Buzz'};

	this.get = function(i){
		let result = '';

		for(let key in dic){						
			if (i % parseInt(key) == 0)
			{				
				result += dic[key];
			}
		}

		if (result != '') 
			return result
		else
			return i.toString();
	};
}