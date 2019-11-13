(function ($) {

    var _absenceService = abp.services.app.absence;
    var _$modal = $('#AbsenceEditModal');
    var _$form = $('form[name=AbsenceEditForm]');


    $('select').selectpicker();
    $('.my-datepicker').datepicker({
        language: 'en',
        autoClose: true
    });

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var absence = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        absence.Status = 0;
        var _$startTimeOfDay = $("#editstarttod").val();
        var _$endTimeOfDay = $("#editendtod").val();

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
        _absenceService.updateLimited(absence).done(function () {
            _$modal.modal('hide');
            location.reload(true); //reload page to see edited user!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }

    //Handle save button click
    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

})(jQuery);