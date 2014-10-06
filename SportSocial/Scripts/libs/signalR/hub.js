(function ($) {
    $.ajax({
        url: "/signalR/hubs",
        dataType: "script",
        async: false
    });
}(jQuery));