'use strict';

angular.module('shared')
    .factory('RequestsErrorHandler', [
        '$q',
        'helpersSrvc',
        function ($q, helpersSrvc) {
            return {
                // optional method
                //'request': function (config) {
                //    // do something on success
                //    return config;
                //},

                //// optional method
                //'requestError': function (rejection) {
                //    // do something on error
                //    helpersSrvc.notice.show({ text: response, type: 'success', delay: 12000 });
                //    return $q.reject(rejection);
                //},



                //// optional method
                'response': function (response) {
                    // do something on success
                    helpersSrvc.notice.show({ text: response, type: 'success', delay: 12000 });
                    return response;
                },

                // optional method
                'responseError': function (rejection) {
                    // do something on error
                    var msg = '<div>Response Status: ' + rejection.status + '</div>' + rejection.data;
                    helpersSrvc.notice.show({ text: msg, type: 'danger', delay: 12000 });
                    return $q.reject(rejection);
                }
            };
        }
    ]);
