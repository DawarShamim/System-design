class SeatType {
  constructor(
    public seatClass: string,
    public price: number,
    public baggage: number
  ) {}
}

class SeatFactory {
  private static seatTypes: Map<string, SeatType> = new Map()

  static getSeatType(seatClass: string, price: number, baggage: number) {
    const key = seatClass

    if (!this.seatTypes.has(key)) {
      this.seatTypes.set(key, new SeatType(seatClass, price, baggage))
    }

    return this.seatTypes.get(key)!
  }
}

class Seat {
  constructor(
    public seatNumber: string,
    public passenger: string | null,
    public seatType: SeatType
  ) {}

  book(passenger: string) {
    this.passenger = passenger
    console.log(`${passenger} booked seat ${this.seatNumber}`)
  }
}


const economy = SeatFactory.getSeatType("Economy", 200, 20)
const business = SeatFactory.getSeatType("Business", 800, 40)

const seat1 = new Seat("1A", null, business)
const seat2 = new Seat("1B", null, business)
const seat3 = new Seat("10A", null, economy)

seat1.book("Alice")
seat3.book("Bob")