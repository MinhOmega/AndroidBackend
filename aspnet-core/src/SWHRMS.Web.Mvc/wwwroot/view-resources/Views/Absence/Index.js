(function ($) {

    var _absenceService = abp.services.app.absence;
    var _$form = $('form[name=absenceBookingForm]');

    var _$table = $('#jtable').DataTable({
        responsive: true,
        columnDefs: [
            { width: '10%', targets: 'all' }
        ],
        autoWidth: false
    });

    //Widgets count
    $('.count-to').countTo();

    //Sales count to
    $('.sales-count-to').countTo({
        formatter: function (value, options) {
            return '$' + value.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, ' ').replace('.', ',');
        }
    });
    $('input[name="daterange"]').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
    });

    _$form.validate({
        rules: {
            StartDate: {
                required: true
            },
            EndDate: {
                required: true
            }
        }
    });

    function create() {
        if (!_$form.valid()) {
            return;
        }

        var absence = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        absence.Status = 0;
        var _$startTimeOfDay = $("#starttod").val();
        var _$endTimeOfDay = $("#endtod").val();

        var date = new Date(absence.StartDate.replace(/(\d{2})[/](\d{2})[/](\d{4})/, "$2/$1/$3"));
        if (_$startTimeOfDay == 0) {
            date.setHours(8);
        }
        else {
            date.setHours(13);
        }
        absence.StartDate = moment(date).format("DD/MM/YYYY HH:mm");

        date = new Date(absence.EndDate.replace(/(\d{2})[/](\d{2})[/](\d{4})/, "$2/$1/$3"));
        if (_$endTimeOfDay == 0) {
            date.setHours(12);
        }
        else {
            date.setHours(17);
        }
        absence.EndDate = moment(date).format("DD/MM/YYYY HH:mm");

        abp.ui.setBusy(_$form);
        _absenceService.create(absence).done(function () {
            location.reload(true); //reload page to see new user!
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    function deleteAbsence(absenceId) {
        abp.message.confirm(
            abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'SWHRMS'), abp.localization.localize('ThisAbsence', 'SWHRMS')),
            function (isConfirmed) {
                if (isConfirmed) {
                    _absenceService.deleteLimited({
                        id: absenceId
                    }).done(function () {
                        refreshAbsenceList();
                    });
                }
            }
        );
    }
    function refreshAbsenceList() {
        location.reload(true); //reload page to see new user!
    }

    //Handle request button click
    _$form.find(".request-button").click(function (e) {
        e.preventDefault();
        create();
    });


    _$table.on('click', '.delete-absence', function () {
        var absenceId = $(this).attr("data-absence-id");

        deleteAbsence(absenceId);
    });

    _$table.on('click', '.edit-absence', function (e) {
        var absenceId = $(this).attr("data-absence-id");

        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'Absences/EditAbsenceModal?absenceId=' + absenceId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#AbsenceEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    $.AdminBSB.input.activate(_$form);
})(jQuery);
