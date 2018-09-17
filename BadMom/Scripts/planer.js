

if (Remind == true) {
    console.log("test!")
    $("#RemindEvent").modal("show");
}


function ShowEventDetailsModal(eventId) {
    //$("#RemindEvent").modal("hide");
    $.ajax({
        url: showEventUrl,
        type: "GET",
        data: { id: eventId },
        success: function (result) {
            $("#showEventDiv").html(result);
            $("#showEventModal").modal("show");

        }
    });
};