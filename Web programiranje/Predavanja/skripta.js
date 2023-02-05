function Vehicle() {
    this.topSpeed = 100;
    this.speed = 10;
    this.GoFast = function(){
        this.speed = this.speed+1;
    }
}

function fourWheel() {
    this.gearBox = 5;
    this.__proto__ = new Vehicle();
    this.gear = 0;
    this.shift = function() {
        this.gear += 1;
    }
}

function Car(){
    this.__proto__ = new fourWheel();
    this.engine = 1000;
    this.Stop = function (){
        this.speed = 0;
    }
}

var auto = new Car();

console.log("Brzina automobila je " + auto.speed);
console.log("Top brzina automobila je " + auto.topSpeed);
auto.GoFast();
auto.shift();
console.log("Prenos je " + auto.gear);
console.log("Brzina automobila je " + auto.speed);
auto.Stop();
console.log("Brzina automobila je " + auto.speed);
console.log("Motor automobila je " + auto.engine);
