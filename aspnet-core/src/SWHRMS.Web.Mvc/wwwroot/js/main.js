(function ($) {
    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);

        //Desktop notification
        Push.create("SWHRMS", {
            body: userNotification.notification.data.message,
            icon: abp.appPath + 'images/app-logo-small.png',
            timeout: 6000,
            onClick: function () {
                window.focus();
                this.close();
            }
        });
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }


})(jQuery);

$(function () {
    $('select').selectpicker();
    $('.my-datepicker').datepicker({
        language: 'en',
        autoClose: true
    });
    //$('.date').datepicker({
    //    clearButton: true,
    //    weekStart: 1,
    //    time: false,
    //    autoclose: true,
    //    orientation: "top",
    //    format: 'dd/mm/yyyy',
    //    container: '#bs_datepicker_component_container'
    //});
    $('.input-daterange input').each(function () {
        $(this).datepicker('clearDates');
    });
    $('.spinner').spinner();
});