var app = angular.module('shared', ['textAngular']);

// bootstrap tooltips init
// ---------------
(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $(document).on('click', '.modal-backdrop', function() {
        bootbox.hideAll();
    });
})();



