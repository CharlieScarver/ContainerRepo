// 1h13m -> 1h31m
// 33/100 -> 100/100
function solve(arr) {

    var newPlayer = {
        name: '',
        matchesWon: 0,
        matchesLost: 0,
        setsWon: 0,
        setsLost: 0,
        gamesWon: 0,
        gamesLost: 0
    },
        players = [],
        newPlayer1 = {},
        newPlayer2 = {},
        regExRes = [],
        names = [],
        sets = [],
        games = [],
        setsWonThisMatch = [0, 0],
        player1Exists = false,
        player2Exists = false;

    for (var ind in arr) {
        newPlayer1 = {
            name: '',
            matchesWon: 0,
            matchesLost: 0,
            setsWon: 0,
            setsLost: 0,
            gamesWon: 0,
            gamesLost: 0
        };
        player1Exists = false;
        newPlayer2 = {
            name: '',
            matchesWon: 0,
            matchesLost: 0,
            setsWon: 0,
            setsLost: 0,
            gamesWon: 0,
            gamesLost: 0
        };
        player2Exists = false;
        setsWonThisMatch[0] = 0;
        setsWonThisMatch[1] = 0;

        regExRes = /(.+)(?=:)/g.exec(arr[ind]);
        //console.log(regExRes[0].trim().split('vs.').map(function (el) { return el.trim(); }));
        names = regExRes[0].trim().split('vs.').map(function (el) { return el.trim(); });
        names = names.map(function (el) { return el.split(/\s+/g).join(' ')});
        //console.log(names);

        for (var playerIndex in players) {
            if(players[playerIndex].name === names[0]) {
                player1Exists = true;
                newPlayer1 = players[playerIndex];
            }
            if(players[playerIndex].name === names[1]) {
                player2Exists = true;
                newPlayer2 = players[playerIndex];
            }
        }

        newPlayer1.name = names[0];
        //console.log(names[0]);
        newPlayer2.name = names[1];

        regExRes = /:(.+)/g.exec(arr[ind]);
        sets = regExRes[1].trim().split(/\s+/g);
        //console.log(sets);
        for (var setIndex in sets) {
            games = sets[setIndex].split('-');
            newPlayer1['gamesWon'] += parseInt(games[0]);
            newPlayer1['gamesLost'] += parseInt(games[1]);

            newPlayer2['gamesWon'] += parseInt(games[1]);
            newPlayer2['gamesLost'] += parseInt(games[0]);

            if(parseInt(games[0]) > parseInt(games[1])) {
                newPlayer1.setsWon += 1;
                setsWonThisMatch[0] += 1;
                newPlayer2.setsLost += 1;

            } else if(parseInt(games[0]) < parseInt(games[1])) {
                newPlayer1.setsLost += 1;
                newPlayer2.setsWon += 1;
                setsWonThisMatch[1] += 1;
            }
        }

        if(setsWonThisMatch[0] > setsWonThisMatch[1]) {
            newPlayer1.matchesWon += 1;
            newPlayer2.matchesLost += 1;
        } else if(setsWonThisMatch[0] < setsWonThisMatch[1]) {
            newPlayer1.matchesLost += 1;
            newPlayer2.matchesWon += 1;
        }

        if(!player1Exists && !player2Exists) {
            players.push(newPlayer1, newPlayer2);
        } else if(!player1Exists && player2Exists) {
            players.push(newPlayer1);
        } else if(player1Exists && !player2Exists) {
            players.push(newPlayer2);
        }
    }

    //console.log(players);
    players = players.sort(function (obj1, obj2) {
        if(obj1.matchesWon !== obj2.matchesWon) {
            return obj2.matchesWon - obj1.matchesWon;
        } else if(obj1.setsWon !== obj2.setsWon) {
            return obj2.setsWon - obj1.setsWon;
        } else if(obj1.gamesWon !== obj2.gamesWon) {
            return obj2.gamesWon - obj1.gamesWon;
        } else {
            return obj1.name.localeCompare(obj2.name);
        }
    });

//    console.log(players);
    console.log(JSON.stringify(players));
    //regExRes = /:(.+)/g.exec(" Novak    Djokovic vs.  Roger Federer : 6-3 6-3");
    //console.log(regExRes[0]);

}

solve([" Novak    Djokovic vs.  Roger Federer : 6-3 6-3",
        'Roger  Federer     vs.         Novak   Djokovic :  6-2      6-3',
        'Rafael Nadal vs. Andy Murray : 4-6 6-2 5-7',
        'Andy Murray vs. David Ferrer : 6-4 7-6',
        'Tomas Bedrych vs. Kei Nishikori : 4-6 6-4 6-3 4-6 5-7',
        'Grigor Dimitrov vs. Milos Raonic : 6-3 4-6 7-6 6-2',
        'Pete Sampras vs. Andre Agassi : 2-1',
        'Boris Beckervs.Andre       Agassi:2-1'
]);
console.log();
