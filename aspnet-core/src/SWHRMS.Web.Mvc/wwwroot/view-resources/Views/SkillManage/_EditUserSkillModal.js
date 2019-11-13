(function ($) {

    var _skillService = abp.services.app.skill;
    var _$modal = $('#SkillEditModal');
    var _$form = $('form[name=UserSkillEditForm]');
    $('select').selectpicker();

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        user.userSkills = [];
        var _$skillSelects = $("select[name='skill']");
        if (_$skillSelects) {
            for (var skillIndex = 0; skillIndex < _$skillSelects.length; skillIndex++) {
                var _$skillSelect = $(_$skillSelects[skillIndex]);
                var userSkill = {
                    skillId: _$skillSelect.attr('id'),
                    levelId: _$skillSelect.find(":selected").attr('value'),
                }

                user.userSkills.push(userSkill);
            }
        }

        console.log(user.userSkills);
        abp.ui.setBusy(_$form);
        _skillService.updateUserSkills(user).done(function () {
            _$modal.modal('hide');
            location.reload(true); //reload page to see edited skill!
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