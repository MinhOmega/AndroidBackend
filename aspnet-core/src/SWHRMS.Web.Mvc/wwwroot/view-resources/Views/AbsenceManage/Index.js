(function ($) {

    var _absenceService = abp.services.app.absence;
    var _$tableCard = $('#absenceManageTable');
    var _$calendar = $('#absenceCalendar');
    var _$userTable = $('#userAbsenceTable');
    var _$table = $('#jtable').DataTable({
        "order": [],
        responsive: true,
        columnDefs: [
            { width: '20%', targets: 0 }
        ],
        fixedColumns: true,
        autoWidth: false
    });

    //API/service/AJAX controller calls
    // Delete selected absence request.
    function deleteAbsence(absenceId) {
        var tr = $('#row_' + absenceId);
        abp.message.confirm(
            abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'SWHRMS'), abp.localization.localize('ThisAbsence', 'SWHRMS')),
            function (isConfirmed) {
                if (isConfirmed) {
                    _absenceService.delete({
                        id: absenceId
                    }).done(function () {
                        //$('#row_' + absenceId).remove();
                        _$table.row(tr).remove().draw();
                        refreshAbsenceCalendar();
                        refreshUserAbsenceTable();
                        //refreshAbsenceList();
                    });
                }
            }
        );
    }
    // Approve absence request.
    function approveAbsence(absenceId) {
        var tr = $('#row_' + absenceId);
        abp.ui.setBusy(_$tableCard);
        $.ajax({
            url: abp.appPath + 'AbsencesManager/AbsenceApproval?absenceId=' + absenceId + "&status=" + 1,
            type: 'POST',
            contentType: 'application/html',
            success: function () {
                //var childTr = tr.next("tr.child");
                tr.find('.absence-status').html('<i class="material-icons" style="color:green;">check_box</i>');
                //childTr.find('.absence-status').html('<i class="material-icons" style="color:green;">check_box</i>');
                _$table.row(tr)
                    .invalidate()
                    .draw();
                refreshAbsenceCalendar();
                refreshUserAbsenceTable();
            },
            error: function () { }
        }).done(function () {
            abp.ui.clearBusy(_$tableCard);
        });
    }
    // Disapprove absence request.
    function disapproveAbsence(absenceId) {
        var tr = $('#row_' + absenceId);
        abp.ui.setBusy(_$tableCard);
        $.ajax({
            url: abp.appPath + 'AbsencesManager/AbsenceApproval?absenceId=' + absenceId + "&status=" + 2,
            type: 'POST',
            contentType: 'application/html',
            success: function () {
                //var childTr = tr.next("tr.child");
                tr.find('.absence-status').html('<i class="material-icons" style="color:red;">check_box</i>');
                //childTr.find('.absence-status').html('<i class="material-icons" style="color:red;">check_box</i>');
                _$table.row(tr)
                    .invalidate()
                    .draw();
                refreshAbsenceCalendar();
                refreshUserAbsenceTable();
            },
            error: function (e) { }
        }).done(function () {
            abp.ui.clearBusy(_$tableCard);
        });
    }
    // Refresh the calendar thingy.
    function refreshAbsenceCalendar() {
        $.ajax({
            url: abp.appPath + 'AbsencesManager/AbsenceCalendar',
            type: 'GET',
            contentType: 'application/html',
            success: function (content) {
                _$calendar.html(content);
            },
            error: function (e) { }
        });
    }
    // Refresh the user absence count thingy.
    function refreshUserAbsenceTable() {
        $.ajax({
            url: abp.appPath + 'AbsencesManager/GetUserAbsenceTable',
            type: 'GET',
            contentType: 'application/html',
            success: function (content) {
                _$userTable.html(content);
            },
            error: function (e) { }
        });
    }

    function refreshPage() {
        location.reload(true); //reload page
    }

    //Handle button click
    _$table.on('click', '#delete-absence', function () {
        var userId = $(this).attr("data-absence-id");

        deleteAbsence(userId);
    });
    _$table.on('click', '#approve-absence', function (e) {
        e.preventDefault();
        var absenceId = $(this).attr("data-absence-id");

        approveAbsence(absenceId);
    });
    _$table.on('click', '#disapprove-absence', function (e) {
        e.preventDefault();
        var absenceId = $(this).attr("data-absence-id");

        disapproveAbsence(absenceId);
    });
})(jQuery);