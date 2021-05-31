// require json-2-csv module
let nodePath = process.argv[0];
let appPath = process.argv[1];
let filePath = process.argv[2];
const converter = require('json-2-csv');
const fs = require('fs');

// read JSON from a file
const todos = JSON.parse(fs.readFileSync('todos.json'));
//console.log(todos)
// convert JSON array to CSV string
converter.json2csv(todos, (err, csv) => {
    if (err) {
        throw err;
    }

    // print CSV string
    console.log(csv);

    // write CSV to a file
    fs.writeFileSync(filePath, csv);

});