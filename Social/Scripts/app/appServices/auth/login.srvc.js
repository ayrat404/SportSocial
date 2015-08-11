'use strict';

angular.module('appSrvc')
    .factory('loginSrvc', [
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
                url = '/test/login';

            // ---------------
            function logIn(data) {
                
            }

            // ---------------
            function logOut() {
                
            }

            // ---------------
            return {
                logIn: logIn,
                logOut: logOut
            }
        }
    ]);
