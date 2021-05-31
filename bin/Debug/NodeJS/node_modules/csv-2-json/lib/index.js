var fs = require('fs');

module.exports = {
	toJson :  function(path,options){
		options || 
		(options = {
				filename:"result.json",
				indent: 3,
				delimiter:','
		})

		var content = readFileIfExist(path);
		if(!content || typeof content !== "string"){
			throw new Error("Invalid CSV Data");
		}
		content = content.split(/[\n\r]+/gi);
		var Columns = content.shift().split(options.delimiter),
			jsonObject = [];

			content.forEach(function(item){
				if(item) {
					item = item.split(options.delimiter);
					var hashItem = {}
					Columns.forEach(function(c,i){
						hashItem[c] = item[i];
					})
					jsonObject.push(hashItem);
				}
			});
		return saveToFile(jsonObject,options);
	}
}

var readFileIfExist = function(path){
	if(fs.statSync(path)){
		return fs.readFileSync(path,'utf8')
	}
	return null
}

var saveToFile = function(data, options){
	if(typeof data == "object"){
		data = JSON.stringify(data, null,options.indent)
	}
	fs.writeFileSync(options.filename,data,"utf8");
}

