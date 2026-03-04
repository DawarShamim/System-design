// L - Liskov Substitution Principle (LSP)

// Bad Implementation Example:
class BirdBad {
  fly() { }
}

class Duck extends BirdBad {
  fly() {console.log('Duck is flying'); }
}

class Penguin extends BirdBad {
  fly() { throw new Error('Penguins cannot fly'); }
}

function makeBirdFly(bird: BirdBad) {
  bird.fly();
}

makeBirdFly(new Duck());
// This function is unneccessarily attached to Penguin
//makeBirdFly(new Penguin());



// Actual Implementation
interface Bird { }

interface FlyingBird extends Bird {
  fly(): void;
}

interface WalkingBird extends Bird {
  walk(): void;
}

class Pigeon implements FlyingBird, WalkingBird {
  fly() {
    console.log('Pigeon is flying');
  }
  walk() {
    console.log('Pigeon is walking');
  }
}

class Kiwi implements WalkingBird {
  walk() {
    console.log('Kiwi is walking');
  }
}

function makeFly(bird: FlyingBird) {
  bird.fly();
}

function makeWalk(bird: WalkingBird) {
  bird.walk();
}

const kiwi = new Kiwi();
const pigeon = new Pigeon();

makeFly(pigeon);      // Duck flies
makeWalk(pigeon);     // Duck walks
makeWalk(kiwi);  // Penguin walks