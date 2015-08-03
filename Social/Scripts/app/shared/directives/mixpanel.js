'use strict';

angular.module('shared')
    .directive('mixpanelEvent', [
        'mixpanel',
        '$location',
        function (
            mixpanel,
            $location) {

        return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var nt = attrs.name || attrs.title || '';
            if (nt.trim() != '') {
                angular.element(element).on(attrs.mixpanelEvent, function () {
                    mixpanel.api('track', ['on','"' + nt + '"', attrs.mixpanelEvent].join(' '), {
                        url: $location.path(),
                        title: scope.$root.title
                    });
                });
            } else {
                console.log('mixpanel event on element: invalid name or title');
            }
        }
    }
}]);