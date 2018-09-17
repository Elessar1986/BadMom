

$('.bs-datepicker').datepicker({
    format: 'dd.mm.yyyy',
    language: "ru"
});

$('.bs-datepicker').removeAttr("data-val-date");


function AddEventModal() {
    $("#addEventModal").modal("show");
}

//function AddEventModal(date, AdvertId) {
//    //console.log(date.format('DD.MM.YYYY'))
//    $.ajax({
//        url: addEventUrl,
//        type: "GET",
//        data: {
//            dateStart: date,
//            advertId: AdvertId
//        },
//        success: function (result) {
//            $("#addEventDiv").html(result);
//            $("#addEventModal").modal("show");

//        }
//    });
//};

