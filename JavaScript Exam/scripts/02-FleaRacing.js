// 51m
// 87/100
function solve (arr) {

    var jumpsAllowed = arr[0],
        trackLength = arr[1],
        fleas = [],
        newFlea = {},
        somebodyWon = false,
        winner = {},
        currFlea = {},
        line = '';

    for(var i = 2; i < arr.length; i++) {
        newFlea = {};
        newFlea['name'] = arr[i].split(', ')[0];
        newFlea['trackName'] = newFlea.name.charAt(0).toUpperCase();
        newFlea['jumpDist'] = parseInt(arr[i].split(', ')[1]);
        newFlea['trackPos'] = 0;

        fleas.push(newFlea);
    }

    for(i = 0; i < jumpsAllowed; i++) {
        for (var j in fleas) {
            currFlea = fleas[j];

            currFlea.trackPos += currFlea.jumpDist;

            //console.log(currFlea.name);
            //console.log(currFlea.trackPos);

            if(currFlea.trackPos >= trackLength - 1) {
                currFlea.trackPos = trackLength - 1;
                winner = currFlea;
                somebodyWon = true;
                break;
            }

        }
        if(somebodyWon) {
            break;
        }
    }

    if(!somebodyWon) {
        winner = currFlea;
    }

    printAudience(trackLength);
    printAudience(trackLength);

    for(var k in fleas) {
        line = new Array(fleas[k].trackPos+1).join('.') + fleas[k].trackName + new Array(trackLength - fleas[k].trackPos).join('.');
        //console.log(typeof line);
        console.log(line);
    }

    printAudience(trackLength);
    printAudience(trackLength);
    console.log('Winner: ' + winner.name);

    //console.log(new Array(6).join('.'));

    function printAudience(len) {
        var output = '';

        for(var count = 0; count < len; count++) {
            output += '#';
        }

        console.log(output);
    }
}


solve([10,19,'angel, 9','Boris, 10','Georgi, 3','Dimitar, 7']);
console.log();
solve([3,5,'cura, 1','Pepi, 1','UlTraFlea, 1','BOIKO, 1']);