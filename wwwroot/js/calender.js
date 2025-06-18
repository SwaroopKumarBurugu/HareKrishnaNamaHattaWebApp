document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        events: [
            { title: 'Janmashtami', start: '2025-09-05' },
            { title: 'Bhajan Night', start: '2025-09-10' }
        ]
    });
    calendar.render();
});
