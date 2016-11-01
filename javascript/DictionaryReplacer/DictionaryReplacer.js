function DictionaryReplacer(){
	this.replace = (text, dic)=>{
		let result = '';

		if (dic && Object.keys(dic).length){
			result = text;
			for(let key in dic){
				result = result.replace('$' + key + '$', dic[key]); 
			}	
		}
		
		return result;
	};
}