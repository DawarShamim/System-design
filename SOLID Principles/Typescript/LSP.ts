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
interface IBird { }

interface IFlyingBird extends IBird {
  fly(): void;
}

interface IWalkingBird extends IBird {
  walk(): void;
}

class Pigeon implements IFlyingBird, IWalkingBird {
  fly() {
    console.log('Pigeon is flying');
  }
  walk() {
    console.log('Pigeon is walking');
  }
}

class Kiwi implements IWalkingBird {
  walk() {
    console.log('Kiwi is walking');
  }
}

function makeFly(bird: IFlyingBird) {
  bird.fly();
}

function makeWalk(bird: IWalkingBird) {
  bird.walk();
}

const kiwi = new Kiwi();
const pigeon = new Pigeon();

makeFly(pigeon);      // Duck flies
makeWalk(pigeon);     // Duck walks
makeWalk(kiwi);  // Penguin walks