(function ($) {
    var _skillService = abp.services.app.skill;
    var _$tableSkills = $('#jtable1').DataTable({
        responsive: true,
        columnDefs: [
            { width: '10%', targets: 'col-level' }
        ],
        autoWidth: false
    });
    var _$tableUsers = $('#jtable2').DataTable({
        responsive: true,
        columnDefs: [
            { width: '10%', targets: 'col-skill' }
        ],
        autoWidth: false
    });

    getDonut();

    function deleteSkill(skillId) {
        abp.message.confirm(
            abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'SWHRMS'), abp.localization.localize('ThisSkill', 'SWHRMS')),
            function (isConfirmed) {
                if (isConfirmed) {
                    _skillService.delete({
                        id: skillId
                    }).done(function () {
                        var tr = $('#row_' + skillId);
                        _$tableSkills.row(tr).remove().draw();
                    });
                }
            }
        );
    }

    //Handle request button click
    _$tableSkills.on('click', '.delete-skill', function () {
        var skillId = $(this).attr("data-skill-id");

        deleteSkill(skillId);
    });

    $(".create-skill").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'SkillsManager/CreateSkillModal',
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#SkillCreateModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    _$tableSkills.on('click', '.edit-skill', function (e) {
        var skillId = $(this).attr("data-skill-id");

        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'SkillsManager/EditSkillModal?skillId=' + skillId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#SkillEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    _$tableUsers.on('click', '.edit-user-skill', function (e) {
        var userId = $(this).attr("data-user-skill-id");

        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'SkillsManager/EditUserSkillModal?userId=' + userId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#UserSkillEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });
    // Get data for donut chart.
    function getDonut() {
        $.ajax({
            url: abp.appPath + 'SkillsManager/DonutChart',
            type: 'GET',
            contentType: 'application/html',
            success: function (content) {
                var donutData = content.result.datas;
                var donutColors = content.result.colors;
                Morris.Donut({
                    element: 'donut_chart',
                    data: donutData,
                    colors: donutColors,
                    formatter: function (y) {
                        return y + ' ' + abp.localization.localize('Employees', 'SWHRMS')
                    }
                });
            },
            error: function () { }
        });
    }
})(jQuery);

