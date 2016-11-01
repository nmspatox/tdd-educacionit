function FizzBuzz(){
	// associative array to emulate dictionary
	let dic = { '3': 'Fizz', '5': 'Buzz'};

	this.get = i => {
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