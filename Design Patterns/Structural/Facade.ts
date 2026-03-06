class RoomService {
  checkAvailability(roomType: string) {
    console.log(`Checking availability for ${roomType}`)
    return true
  }
}

class PaymentService {
  processPayment(amount: number) {
    console.log(`Processing payment of $${amount}`)
  }
}

class NotificationService {
  sendConfirmation() {
    console.log("Booking confirmation sent")
  }
}

class HotelBookingFacade {
  private roomService = new RoomService()
  private paymentService = new PaymentService()
  private notificationService = new NotificationService()

  bookRoom(roomType: string, amount: number) {
    if (this.roomService.checkAvailability(roomType)) {
      this.paymentService.processPayment(amount)
      this.notificationService.sendConfirmation()
      console.log("Room booked successfully")
    }
  }
}

const booking = new HotelBookingFacade()
booking.bookRoom("Deluxe", 200)