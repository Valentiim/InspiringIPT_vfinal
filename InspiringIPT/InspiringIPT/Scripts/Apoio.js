$('.carousel').carousel({
    interval: 5000 //changes the speed
})

$(function () {
    $('#datetimepicker1').datetimepicker({
        format: 'DD/MM/YYYY',
        minDate: moment(),
        useCurrent: false
    });
    $('#datetimepicker2').datetimepicker({
        format: 'DD/MM/YYYY',
        minDate: moment(),
        useCurrent: false
    });
    $("#datetimepicker1").on("dp.change", function (e) {
        $('#datetimepicker2').data("DateTimePicker").minDate(e.date);

    });
    $("#datetimepicker2").on("dp.change", function (e) {
        $('#datetimepicker1').data("DateTimePicker").maxDate(e.date);
    });
});
$(window).load(function () {
    $('#myModal').modal('show');
});