// designed upfront to keep two hierarchies intentionally separate so they can evolve independently.
type AlertData = {
  service: string,
  error: string,
  time: string,
  severity: string
}

type WeeklyReportData = {
  week: string,
  orders: number,
  revenue: string,
  newUsers: number,
  churn: number
}

type PromotionData = {
  promoTitle: string,
  message: string,
  code: string,
  expiry: string
}

type PaymentData = {
  amount: string,
  orderId: string,
  success: boolean,
  reason?: string
}

interface INotificationChannel {
    send(recipient: string, subject: string, body: string): void
}

abstract class A_Notification<T> {
  constructor(protected channel: INotificationChannel) { }

  setChannel(channel: INotificationChannel): void {
    this.channel = channel
  }

  abstract send(recipient: string, data: T): void
}


class SlackChannel implements INotificationChannel {
    send(to: string, subject: string, body: string): void {
        console.log(`[SLACK] Channel: ${to}`)
        console.log(`        *${subject}*`)
        console.log(`        ${body}\n`)
    }
}

class SMSChannel implements INotificationChannel {
    send(to: string, _subject: string, body: string): void {
        // SMS has no subject — silently ignored
        const truncated = body.length > 160 ? body.slice(0, 157) + "..." : body
        console.log(`[SMS] To: ${to} | ${truncated}\n`)
    }
}

class EmailChannel implements INotificationChannel {
    send(to: string, subject: string, body: string): void {
        console.log(`[EMAIL] To: ${to}`)
        console.log(`        Subject: ${subject}`)
        console.log(`        Body: ${body}\n`)
    }
}

class AlertNotification extends A_Notification<AlertData> {
  send(recipient: string, data: AlertData): void {

    const subject = `🚨 ALERT: ${data.service} is DOWN`

    const body =
      `Service: ${data.service}\n` +
      `Error: ${data.error}\n` +
      `Time: ${data.time}\n` +
      `Severity: ${data.severity}`

    this.channel.send(recipient, subject, body)
  }
}

class WeeklyReportNotification extends A_Notification<WeeklyReportData> {
  send(recipient: string, data: WeeklyReportData): void {

    const subject = `📊 Weekly Report — ${data.week}`

    const body =
      `Total Orders: ${data.orders}\n` +
      `Revenue: $${data.revenue}\n` +
      `New Users: ${data.newUsers}\n` +
      `Churn: ${data.churn}%`

    this.channel.send(recipient, subject, body)
  }
}


class PromotionNotification extends A_Notification<PromotionData> {
  send(recipient: string, data: PromotionData): void {

    const subject = `🎉 ${data.promoTitle}`

    const body =
      `${data.message}\n` +
      `Use code: ${data.code}\n` +
      `Expires: ${data.expiry}`

    this.channel.send(recipient, subject, body)
  }
}


class PaymentNotification extends A_Notification<PaymentData> {
  send(recipient: string, data: PaymentData): void {

    const subject = data.success
      ? `✅ Payment Confirmed`
      : `❌ Payment Failed`

    const body =
      `Amount: $${data.amount}\n` +
      `Order: ${data.orderId}\n` +
      `Status: ${data.success ? 'Success' : 'Failed — ' + data.reason}`

    this.channel.send(recipient, subject, body)
  }
}


const opsAlert = new AlertNotification(new SlackChannel())

opsAlert.send("#ops-alerts", {
  service: "payment-api",
  error: "Connection timeout",
  time: "2026-03-07 14:32 UTC",
  severity: "CRITICAL"
})

const promo = new PromotionNotification(new SMSChannel())

promo.send("#customer",{
  promoTitle: "30% Off",
  message: "Get 30% off on all items",
  code: "New30",
  expiry: "2026-05-07 14:30:00"
})
