$(function () {

    var _userService = abp.services.app.user;
    var _$form = $('form[name=UserEditForm]');

    function save() {

        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        user.roleNames = [];
        var _$roleCheckboxes = $("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                user.roleNames.push(_$roleCheckbox.val());
            }
        }
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
        _userService.update(user).done(function () {
            location.href = '../';
            //location.reload(true); //reload page to see edited user!
        }).always(function () {
        });
    }

    //Handle save button click
    $('.save-button').click(function (e) {
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

});