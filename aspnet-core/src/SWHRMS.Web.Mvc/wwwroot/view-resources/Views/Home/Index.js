$(function () {

    var _userService = abp.services.app.user;
    var _$form = $('form[name=UserEditForm]');
    var _$formChangePass = $('form[name=UserChangePassword]');

    _$formChangePass.validate({
        rules: {
            Password: "required",
            NewPassword: "required",
            ConfirmNewPassword: {
                equalTo: "#NewPassword"
            }
        }
    });

    function save() {

        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        user.metaInfos = [];
        var _$metaInfos = $("input[name='meta']");
        if (_$metaInfos) {
            for (var infoIndex = 0; infoIndex < _$metaInfos.length; infoIndex++) {
                var _$metaInfo = $(_$metaInfos[infoIndex]);
                //console.log(_$metaInfo);
                var metaInfo = {
                    userMetaInfoName: _$metaInfo.attr('id'),
                    infoDetail: _$metaInfo.val()
                }
                user.metaInfos.push(metaInfo);
            }
        }
        //console.log(user.metaInfos);
        _userService.updateLimited(user).done(function () {
            //location.href = '../';
            location.reload(true); //reload page to see edited user!
        }).always(function () {
        });
    }

    $(".edit-user-skill").click(function (e) {
        var userId = $(this).attr("data-user-skill-id");

        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'Home/EditUserSkillModal?userId=' + userId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#UserSkillEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    //Handle save button click
    $('.save-button').click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle change password button click.
    $('.change-button').click(function (e) {
        e.preventDefault();

        if (!_$formChangePass.valid()) {
            return;
        }

        var changeModel = {
            currentPassword: $('#Password').val(),
            newPassword: $('#NewPassword').val()
        }
        _userService.changePassword(changeModel).done(function () {
            //location.href = '../';
            location.reload(true); //reload page to see edited user!
        }).always(function () {
        });
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    $.AdminBSB.input.activate(_$form);

});