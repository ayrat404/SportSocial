'use strict';

angular.module('appSrvc')
    .factory('registrationSrvc', [
        '$state',
        '$location',
        '$q',
        '$rootScope',
        'base',
        'mixpanel',
        function (
            $state,
            $location,
            $q,
            $rootScope,
            base,
            mixpanel) {

            var defaults = {
                    showNotices: true
                },
                url = '/test/registration';

            // ---------------
            function register(data) {
                
            }
            // ---------------
            return {
                register: register
            }

        }
    ]);
