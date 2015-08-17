'use strict';

angular.module('shared')
    .factory('remoteTemplateSrvc', [
        '$http',
        '$q',
        function ($http, $q) {

            var baseUrl = '/Scripts/templates/common/';

            return {
                get: function (template) {
                    return $q(function(resolve, reject) {
                        $http({
                            method: 'GET',
                            url: baseUrl + template + '.html'
                        }).success(function (res) {
                            resolve(res);
                        }).error(function () {
                            reject();
                        });
                    });
                }
            }
        }
    ]);