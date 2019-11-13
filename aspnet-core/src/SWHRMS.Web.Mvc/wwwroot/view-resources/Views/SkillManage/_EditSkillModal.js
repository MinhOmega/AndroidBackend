(function ($) {

    var _skillService = abp.services.app.skill;
    var _$modal = $('#SkillEditModal');
    var _$form = $('form[name=SkillEditForm]');

    $('#cp1').colorpicker({
        inline: true,
        container: true,
        extensions: [
            {
                name: 'swatches',
                options: {
                    colors: {
                        'tetrad1': '#000',
                        'tetrad2': '#000',
                        'tetrad3': '#000',
                        'tetrad4': '#000'
                    },
                    namesAsValues: false
                }
            }
        ]
    }).on('colorpickerChange colorpickerCreate', function (e) {
        var colors = e.color.generate('tetrad');

        colors.forEach(function (color, i) {
            var colorStr = color.string(),
                swatch = e.colorpicker.picker
                    .find('.colorpicker-swatch[data-name="tetrad' + (i + 1) + '"]');

            swatch
                .attr('data-value', colorStr)
                .attr('title', colorStr)
                .find('> i')
                .css('background-color', colorStr);
        });
    });

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var skill = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js

        abp.ui.setBusy(_$form);
        _skillService.update(skill).done(function () {
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