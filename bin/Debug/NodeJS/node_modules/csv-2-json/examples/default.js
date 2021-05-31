/* *
/*   Example using default options for creating JSON file from CSV
/*
/* */
var csvtojson = require('./../../csv-2-json');
var options = {
	filename:"report.json",
	indent: 4,
	delimiter: ","
}
csvtojson.toJson('./sample.csv', options);