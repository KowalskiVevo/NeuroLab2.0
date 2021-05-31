&copy; ,Kailash Yogeshwar, Licensed under MIT-License
# csv-2-json (Node.js)
  Npm module to convert your CSV file to JSON file with custom options to specify file   name and identation level to be use in Json file.
# Install
```bash
npm install csv-2-json --save
```
----------
# Features
----------
* Converts and saves your csv data into JSON format with custom options to specify file name and indentation to use in JSON file.
* Specify Custom delimiter options to parse csv data

# Example Usage
----------
```javascript
/*
  with default options
  sample.csv 
  name, age, score
  Kailash, 24, 36
  Rajesh, 26, 40
  Ajinkya, 33, 49
 */

var csvtojson = require('csv-2-json');
csvtojson.toJson('./sample.csv')

/*
[
   {
      "name": "kailash",
      "age": "24",
      "score": "36"
   },
   {
      "name": "Rajesh",
      "age": "26",
      "score": "50"
   },
   {
      "name": "Ajinkya",
      "age": "30",
      "score": "54"
   }
]
*/
```

# With Custom Options
--------------------
```javascript
/*
 name, age, score
 kailash, 24, 36
 Rajesh, 26, 50
 Ajinkya, 30, 54
*/
```
# Example
``` javascript
var csvtojson = require('csv-2-json');
var options = {
 'filename':'report.json',
 'indent': 4
}

csvtojson.toJson('./sample.csv',options);
/*
[
    {
        "name": "kailash",
        "age": "24",
        "score": "36"
    },
    {
        "name": "Rajesh",
        "age": "26",
        "score": "50"
    },
    {
        "name": "Ajinkya",
        "age": "30",
        "score": "54"
    }
]
*/
```
----------
# Custom Delimiter
```javascript
  /*
     Sample Data
     name       age score
     Kailash    24  36
     Rajesh     26  50
     Ajinkya    30  54
  */
  var csvjson = require('csv-2-json');
  var options = {
  filename: "result.json",
  indent: 4,
  delimiter: "\t"
  }
  
  csvjson.toJson('./sample.csv',options);
  /*
    Result
    [
    {
        "name": "kailash",
        "age": "24",
        "score": "36"
    },
    {
        "name": "Rajesh",
        "age": "26",
        "score": "50"
    },
    {
        "name": "Ajinkya",
        "age": "30",
        "score": "54"
    }
]
  */