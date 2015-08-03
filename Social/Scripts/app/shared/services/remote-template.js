'use strict';

angular.module('shared')
    .factory('remoteTemplateSrvc', [
        '$http',
        function ($http) {

            var baseUrl = '/static/templates/common/';

            return {
                get: function (template) {
                    var defer = $.Deferred();
                    $http({
                        method: 'GET',
                        url: baseUrl + template + '.html'
                    }).success(function (res) {
                        defer.resolve(res);
                    }).error(function(res) {
                        defer.reject();
                    });

                    return defer.promise();
                }
            }
        }
    ]);