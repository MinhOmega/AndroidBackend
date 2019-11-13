(function ($) {
    var _userService = abp.services.app.user;
    var _$table = $('#jtable').DataTable({
        responsive: true,
        columnDefs: [
            { width: '10%', targets: 'all' }
        ],
        autoWidth: false
    });

    $('#RefreshButton').click(function () {
        refreshUserList();
    });

    _$table.on('click', '.delete-user', function ()  {
        var userId = $(this).attr("data-user-id");
        var userName = $(this).attr('data-user-name');

        deleteUser(userId, userName);
    });

    _$table.on('click', '.edit-user', function (e)  {
        var userId = $(this).attr("data-user-id");

        e.preventDefault();
        location.href = '../Users/' + userId + '/Edit';
        //$.ajax({
        //    url: abp.appPath + 'Users/EditUserModal?userId=' + userId,
        //    type: 'POST',
        //    contentType: 'application/html',
        //    success: function (content) {
        //        $('#UserEditModal div.modal-content').html(content);
        //    },
        //    error: function (e) { }
        //});
    });

    $('.create-user').click(function (e) {
        e.preventDefault();
        location.href = '../Users/Create';
        //$.ajax({
        //    url: abp.appPath + 'Users/Create',
        //    type: 'POST',
        //    contentType: 'application/html',
        //    success: function (content) {
        //        $('#UserCreateModal div.modal-content').html(content);
        //    },
        //    error: function (e) { }
        //});
    });

    function refreshUserList() {
        location.reload(true); //reload page to see new user!
    }

    function deleteUser(userId, userName) {
        abp.message.confirm(
            abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'SWHRMS'), userName),
            function (isConfirmed) {
                if (isConfirmed) {
                    _userService.delete({
                        id: userId
                    }).done(function () {
                        refreshUserList();
                    });
                }
            }
        );
    }
})(jQuery);