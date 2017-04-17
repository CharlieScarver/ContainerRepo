// 1h17m
// 100/100
function solve(arr) {

    var directions = arr[0].split(', '),
        matrix = [],
        rabbit = {
            row: 0,
            cell: 0,
            lettuce: 0,
            cabbage: 0,
            turnip: 0,
            carrots: 0,
            wallHits: 0,
            visitedCells: []
        };

    for (var i = 1; i < arr.length; i++) {
        matrix[i-1] = arr[i].split(', ');
    }

    for (var ind in directions) {
        //console.log(directions[ind]);

        if(directions[ind].toLowerCase() === 'right') {
            if(!hitsWall(rabbit.row, rabbit.cell+1)) {

                rabbit.cell += 1;
                //console.log(rabbit.row, rabbit.cell);
                visitCell(rabbit.row, rabbit.cell);

            } else {
                rabbit.wallHits += 1;
                //console.log('hit' + directions[ind]);
            }
        } else if (directions[ind].toLowerCase() === 'left') {
            if(!hitsWall(rabbit.row, rabbit.cell-1)) {

                rabbit.cell -= 1;
                //console.log(rabbit.row, rabbit.cell);
                visitCell(rabbit.row, rabbit.cell);

            } else {
                rabbit.wallHits += 1;
                //console.log('hit');
            }
        } else if (directions[ind].toLowerCase() === 'up') {
            if(!hitsWall(rabbit.row-1, rabbit.cell)) {

                rabbit.row -= 1;
                //console.log(rabbit.row, rabbit.cell);
                visitCell(rabbit.row, rabbit.cell);

            } else {
                rabbit.wallHits += 1;
                // console.log('hit');
            }
        } else if (directions[ind].toLowerCase() === 'down') {
            if(!hitsWall(rabbit.row+1, rabbit.cell)) {

                rabbit.row += 1;
                //console.log(rabbit.row, rabbit.cell);
                visitCell(rabbit.row, rabbit.cell);

            } else {
                rabbit.wallHits += 1;
               // console.log('hit');
            }
        }
    }

    //console.log(matrix);
    console.log('{"&":' + rabbit.lettuce +
        ',"*":' + rabbit.cabbage +
        ',"#":' + rabbit.turnip +
        ',"!":' + rabbit.carrots +
        ',"wall hits":' + rabbit.wallHits +
        '}');
    printVisitedCells();


    function hitsWall(x, y) {
        var rowExists = !(matrix[x] === undefined);
        if(!rowExists) {
            return true;
        } else {
            return matrix[x][y] === undefined;
        }
    }

    function visitCell(x, y) {
        var splitted = matrix[x][y].split(''),
            canBeFood = false;

        for (var index = 0; index < splitted.length; index++) {

            canBeFood = splitted[index] === '{' &&
                splitted[index+2] === '}';

            if(canBeFood) {
                switch (splitted[index+1]) {
                    case '&':
                        rabbit.lettuce += 1;
                        splitted[index] = '';
                        splitted[index+1] = '@';
                        splitted[index+2] = '';
                        break;
                    case '*':
                        rabbit.cabbage += 1;
                        splitted[index] = '';
                        splitted[index+1] = '@';
                        splitted[index+2] = '';
                        break;
                    case '#':
                        rabbit.turnip += 1;
                        splitted[index] = '';
                        splitted[index+1] = '@';
                        splitted[index+2] = '';
                        break;
                    case '!':
                        rabbit.carrots += 1;
                        splitted[index] = '';
                        splitted[index+1] = '@';
                        splitted[index+2] = '';
                        break;
                }
            }
        }

        matrix[x][y] = splitted.join('');
        rabbit.visitedCells.push(matrix[x][y]);
    }

    function printVisitedCells() {
        var output = '';

        if(rabbit.visitedCells.length > 0) {
            for (var i = 0; i < rabbit.visitedCells.length; i++) {
                output += rabbit.visitedCells[i];
                if(i < rabbit.visitedCells.length - 1) {
                    output += '|';
                }
            }
        } else {
            output = 'no';
        }
        console.log(output);
    }
}

solve(['right, up, up, down',
    'asdf, as{#}aj{g}dasd, kjldk{}fdffd, jdflk{#}jdfj',
    'tr{X}yrty, zxx{*}zxc, mncvnvcn, popipoip',
    'poiopipo, nmf{X}d{X}ei, mzoijwq, omcxzne'
]);
console.log();
solve(['up, right, left, down',
    'as{!}xnk'
]);
