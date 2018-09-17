$('.input-daterange').datepicker({
    format: 'dd.mm.yyyy',
    language: "ru"
});


$('.bs-datepicker').datepicker({
    format: 'dd.mm.yyyy',
    language: "ru"
});

$('.bs-datepicker').removeAttr("data-val-date");



function OnSuccess() {

    $("#form0")[0].reset();
}

function OnBegin() {
    return confirm("Вы уверены? (Удаляться все расходы и доходы связанные с этим ресурсом)!");
}

function InfoSuccess() {

    $("#ResourseInfoModal").modal("show");
}