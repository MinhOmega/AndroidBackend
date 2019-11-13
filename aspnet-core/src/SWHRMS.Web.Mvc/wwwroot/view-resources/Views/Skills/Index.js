(function ($) {
    var _skillService = abp.services.app.skill;
    var _$tableSkills = $('#jtable1').DataTable({
        responsive: true,
        columnDefs: [
            { width: '10%', targets: 'col-level' }
        ],
        autoWidth: false
    });
    addRowCount('#jtable1');

    function addRowCount(tableAttr) {
        $(tableAttr).each(function () {
            $('th:first-child, thead td:first-child', this).each(function () {
                var tag = $(this).prop('tagName');
                $(this).before('<' + tag + '>#</' + tag + '>');
            });
            $('td:first-child', this).each(function (i) {
                $(this).before('<td>' + (i + 1) + '</td>');
            });
        });
    }


    $(".edit-user-skill").click(function (e) {
        var userId = $(this).attr("data-user-skill-id");

        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'Skills/EditUserSkillModal?userId=' + userId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                $('#UserSkillEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });
})(jQuery);

