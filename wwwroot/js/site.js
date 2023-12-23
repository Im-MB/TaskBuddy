// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Scroll
function scrollToHomeSection(){
    $('html, body').animate({
        scrollTop: $('#home').offset().top
    }, 'slow');


    $('ul li.title').removeClass('layout_focus'); 
    $('#' + sectionId + 'Link').addClass('layout_focus');
}
function scrollToAboutSection() {
    $('html, body').animate({
        scrollTop: $('#about').offset().top
    }, 'slow');


    $('ul li.title').removeClass('layout_focus');
    $('#' + sectionId + 'Link').addClass('layout_focus');
}

function scrollToFeaturesSection() {
    $('html, body').animate({
        scrollTop: $('#features').offset().top
    }, 'slow');


    $('ul li.title').removeClass('layout_focus'); // Remove focus from all links
    $('#' + sectionId + 'Link').addClass('layout_focus');
}

function scrollToPartnersSection() {
    $('html, body').animate({
        scrollTop: $('#partners').offset().top
    }, 'slow');


    $('ul li.title').removeClass('layout_focus'); // Remove focus from all links
    $('#' + sectionId + 'Link').addClass('layout_focus');
}

//====================================================================
const calendarDays = document.getElementById('calendar-days');
const monthYear = document.getElementById('month-year');
let currentDate = new Date();

function displayCalendar() {
    const lastDay = new Date(
        currentDate.getFullYear(),
        currentDate.getMonth() + 1,
        0
    ).getDate();

    const firstDayIndex = new Date(
        currentDate.getFullYear(),
        currentDate.getMonth(),
        1
    ).getDay();

    const lastDayIndex = new Date(
        currentDate.getFullYear(),
        currentDate.getMonth() + 1,
        0
    ).getDay();

    const nextDays = 7 - lastDayIndex - 1;

    const months = [
        'January',
        'February',
        'March',
        'April',
        'May',
        'June',
        'July',
        'August',
        'September',
        'October',
        'November',
        'December'
    ];

    monthYear.innerHTML = `${months[currentDate.getMonth()]} ${currentDate.getFullYear()}`;

    let days = '';

    for (let i = firstDayIndex; i > 0; i--) {
        days += `<div class="day other">${lastDay - i + 1}</div>`;
    }

    for (let i = 1; i <= lastDay; i++) {
        if (
            i === new Date().getDate() &&
            currentDate.getMonth() === new Date().getMonth()
        ) {
            days += `<div class="day today">${i}</div>`;
        } else {
            days += `<div class="day">${i}</div>`;
        }
    }


    

    for (let i = 1; i <= nextDays; i++) {
        days += `<div class="day other">${i}</div>`;
    }



    calendarDays.innerHTML = days;
}

function prevMonth() {
    currentDate.setMonth(currentDate.getMonth() - 1);
    displayCalendar();
}

function nextMonth() {
    currentDate.setMonth(currentDate.getMonth() + 1);
    displayCalendar();
}

displayCalendar();

//--------------------------------------------------------------

document.querySelectorAll('input[type="checkbox"]').forEach(checkbox => {
    checkbox.addEventListener('change', async event => {
        const taskId = event.target.value;

        try {
            const response = await fetch(`/Tache/UpdateStatus/${taskId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ taskId }) 
            });

            if (response.ok) {
                location.reload();
            } else {
                console.error('Failed to update task status');
            }
        } catch (error) {
            console.error('Error updating task status:', error);
        }
    });
});
