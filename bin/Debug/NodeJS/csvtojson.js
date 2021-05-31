let nodePath = process.argv[0];
let appPath = process.argv[1];
let filePath = process.argv[2];
const converter = require('json-2-csv');
const fs = require('fs');

let csv = fs.readFileSync(filePath).toString();


converter.csv2json(csv, (err, json) => {
    if (err) {
        throw err;
    }
    console.log(json);
    fs.writeFileSync('test.json', JSON.stringify(json));
});
