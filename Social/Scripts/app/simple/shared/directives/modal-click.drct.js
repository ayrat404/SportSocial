'use strict';

angular.module('shared')
    .directive('modalClick', [
        'modalSrvc',
        function (modalSrvc) {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {
                    var modalItem = attrs.modalClick;
                    element.on('click', function () {
                        modalSrvc.show({ name: modalItem });
                    });
                }
            }
        }
    ]);