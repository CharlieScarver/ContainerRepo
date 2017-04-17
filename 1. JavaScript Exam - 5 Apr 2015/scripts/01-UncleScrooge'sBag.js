// 1h13m -> 2h01m -> 2h23m
// 75/100 -> 75/100 -> 75
function solve (arr) {

    var splittedStr = [],
        type = '',
        amount = '',
        isValid = false,
        bag = {
            gold: 0,
            silver: 0,
            bronze: 0
        },
        floatAmount = 0.0,
        sliced = '',
        multiplier = 10;

    for (var i = 0; i < arr.length; i++) {
        splittedStr = arr[i].split(/\s/);
        type = splittedStr[0];
        amount = splittedStr[1];
        floatAmount = parseFloat(amount);
        isValid = (type.toLowerCase() === 'coin' || type.toLowerCase() === 'coins') &&
            !isNaN(parseInt(amount)) &&
            Math.ceil(((floatAmount < 1.0) ? floatAmount : (floatAmount % Math.floor(floatAmount))) * 100000000000000) === 0 &&
            parseInt(amount) >= 0 &&
            /([^\d\.])/g.exec(amount) === null &&
            parseInt(amount) <= 100000;


        //parseFloat(amount) * 10 % 10 === 0 &&
        //    amount.indexOf('.') === -1;

        if(isValid) {
            amount = parseInt(amount);

            if(amount >= 100) {
                amount = amount.toString();
                //console.log(amount);
                sliced = amount.slice(0, -2);
                multiplier = 1;

                for(var j = sliced.length - 1; j > -1; j--) {
                    bag.gold += parseInt(sliced[j]) * parseInt(multiplier);
                    multiplier *= 10;
                }

                //bag.gold += Math.floor(amount / 100);
                //console.log(amount.slice(slicedForGold.length - 1, -1));
                bag.silver += parseInt(amount.slice(sliced.length, -1));
                bag.bronze += parseInt(amount.slice(sliced.length + 1));

            } else if(amount >= 10 && amount <= 99) {
                amount = amount.toString();
                //console.log(amount);
                sliced = amount.slice(0, -1);


                bag.silver += parseInt(sliced);
                bag.bronze += parseInt(amount.slice(1));

            } else if(amount >= 0 && amount <= 9) {
                bag.bronze += amount;
            }
        }
    }

    console.log('gold : ' + bag.gold);
    console.log('silver : ' + bag.silver);
    console.log('bronze : ' + bag.bronze);

}

solve(['coin 1','coin 2','coin 5','coin 10','coin 20','coin 50','coin 100','coin 200','coin 500','cigars 1']);
console.log();
solve(['coin one','coin two','coin five','coin ten','coin twenty','coin fifty','coin hundred','cigars 1']);
console.log();
solve(['coin 1','coin two','coin 5','coin 10.50','coin 20','coin 50','coin hundred','cigars 1']);
console.log();
solve(['coInS 111.111','Coins 9.0','coin 1111','coin 01.0','coins 1p1.0','coin 1.0000000000000001']);
console.log(parseInt('1*4'));
