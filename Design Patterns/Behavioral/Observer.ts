// used for Real Updates 
interface Observer { update(data: any): void; }


interface Subject {
  attach(observer: Observer): void;
  detach(observer: Observer): void;
  notify(): void;
}


class ConcreteSubject implements Subject {
  private observers: Observer[] = [];
  private state: string = "";

  attach(observer: Observer): void { this.observers.push(observer); }

  detach(observer: Observer): void { this.observers = this.observers.filter(obs => obs !== observer); }

  setState(state: string): void {
    this.state = state;
    this.notify();
  }

  getState(): string { return this.state; }

  notify(): void { this.observers.forEach(observer => observer.update(this.state)); }
}


class ConcreteObserver implements Observer {
  constructor(private name: string) { }

  update(data: any): void { console.log(`${this.name} received update: ${data}`); }
}


const subject = new ConcreteSubject();

const observer1 = new ConcreteObserver("Observer 1");
const observer2 = new ConcreteObserver("Observer 2");

subject.attach(observer1);
subject.attach(observer2);

subject.setState("New State!");