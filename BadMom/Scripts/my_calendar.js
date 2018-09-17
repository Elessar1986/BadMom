$(document).ready(function () {

    


    $("#calendar").fullCalendar({
        locale: 'ru',
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek'
        },
        buttonText: {
            today: 'Сегодня',
            month: 'Месяц',
            week: 'Неделя'
        },  
        defaultDate: new Date(),
        contentHeight: 600,
        timeFormat: 'H(:mm)',
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: getEventsUrl,
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                id: data.Id,
                                title: data.Title,
                                description: data.Description,
                                start: moment(data.DateStart),
                                end: data.DateEnd != null ? moment(data.DateEnd) : null,
                                backgroundColor: data.EventType.Color != null ? data.EventType.Color : "#327e7e",
                                borderColor: data.EventType.Color != null ? data.EventType.Color : "#327e7e"
                            });
                    });

                    callback(events);
                }
            });
        },  
        dayClick: function(date, jsEvent, view) {
            AddEventModal(date);
        },
        eventClick: function (calEvent, jsEvent, view) {
            ShowEventModal(calEvent, jsEvent, view);
        },
        editable: false 
    });

    function ShowEventModal(calEvent, jsEvent, view) {
        $.ajax({
            url: showEventUrl,
            type: "GET",
            data: { id: calEvent.id },
            success: function (result) {
                $("#showEventDiv").html(result);
                $("#showEventModal").modal("show");

            }
        });
    };

    function AddEventModal(date) {
        console.log(date.format());
        $.ajax({
            url: addEventUrl,
            type: "GET",
            data: { dateStart: date.format()},
            success: function (result) {
                $("#addEventDiv").html(result);
                $("#addEventModal").modal("show");

            }
        });
    };





})

