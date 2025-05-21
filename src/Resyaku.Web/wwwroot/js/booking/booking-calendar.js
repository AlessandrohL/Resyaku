import client from "../api/api-client.js"

const calendarEl = document.querySelector('#calendar')

const calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    dayMaxEvents: true,
    views: {
        timeGridWeek: {
            eventMaxStack: 2,
        }
    },
    navLinks: true,
    headerToolbar: {
        left: 'today prev,next',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    },
    slotLabelFormat: {
        hour: 'numeric',
        minute: '2-digit',
        hour12: true
    },
    locale: 'es',
    loading: (isLoading) => {
        const calendarLoader = document.getElementById('calendar-loader-container')
        calendarLoader.style.display = isLoading ? 'flex' : 'none'
    },
    events: getBookingEvents,
    eventClick: ({ el: element, event }) => {
        if (element._tippy) {
            element._tippy.show()
            return
        }

        tippy(element, {
            content: createPopoverTemplate(event),
            allowHTML: true,
            trigger: 'click',
            interactive: true,
            theme: 'light-border',
            onHidden(instance) {
                instance.destroy()
            },
            appendTo: document.body,
            zIndex: 9999
        })

        element._tippy.show()
    },
    themeSystem: 'bootstrap5'
})

document.addEventListener('DOMContentLoaded', () => {
    calendar.render()
})

async function getBookingEvents(info, success, failure) {
    const params = {
        startDate: info.startStr,
        endDate: info.endStr
    }

    try {
        const response = await client.get('/bookings/events', { params })
        const mappedBookings = response.data.map(booking => ({
            id: booking.reference,
            title: `#${booking.reference} ${booking.customerName}`,
            start: `${booking.date}T${booking.startTime}`,
            end: `${booking.date}T${booking.endTime}`,
            extendedProps: {
                reference: booking.reference,
                partySize: booking.partySize,
                tables: booking.tables,
                status: booking.status,
                customerName: booking.customerName,
                customerEmail: booking.customerEmail,
                customerDni: booking.customerDni
            },
            textColor: '#343a40',
            backgroundColor: getBgColorByStatus(booking.status)
        }))

        success(mappedBookings)
    } catch (error) {
        failure(error)
    }
}

function createPopoverTemplate(eventInfo = {}) {
    const {
        reference,
        customerName,
        customerDni,
        customerEmail,
        partySize,
        tables
    } = eventInfo.extendedProps

    return `
      <div class="popover-content">
        <div class="popover-title">
          Reserva: <a href="#">${reference}</a>
        </div>
        <div class="popover-divider"></div>
        <div class="popover-details">
          <div><strong>Cliente:</strong> ${customerName}</div>
          <div><strong>DNI:</strong> ${customerDni}</div>
          <div><strong>Email:</strong> ${customerEmail}</div>
          <div><strong>Comensales:</strong> ${partySize}</div>
          <div><strong>Mesas:</strong> ${tables.join(', ')}</div>
          <div><strong>Fecha:</strong> ${eventInfo.start.toLocaleDateString()}</div>
          <div><strong>De:</strong> ${eventInfo.start.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</div>
          <div><strong>A:</strong> ${eventInfo.end.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}</div>
        </div>
      </div>
    `
}

function getBgColorByStatus(bookingStatus) {
    const statusColors = {
        Pending: '#FDE68A',
        Confirmed: '#93C5FD',
        Completed: '#86EFAC',
        Cancelled: '#FCA5A5',
        NoShow: '#D1D5DB'
    }

    return statusColors[bookingStatus] ?? statusColors.NoShow
}