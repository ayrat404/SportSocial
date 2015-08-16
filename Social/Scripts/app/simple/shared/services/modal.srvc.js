'use strict';

// mixpanel
// ---------------
angular.module('shared')
    .factory('modalSrvc',[
        '$q',
        '$http',
        function ($q, $http) {
            var baseUrl = 'Scripts/templates/modals/';

            var preset = {
                policy: {
                    classname: 'fs-modal--fill'
                }
            }

            // show preset modals
            // ---------------
            function show(prop) {
                if (preset[prop.name] != undefined) {
                    // todo create loader
                    getRemoteModal(prop.name).then(function (res) {
                        return bootbox.dialog(angular.extend({
                            message: res,
                            className: '.fs-modal ' + preset[prop.name].classname
                            }, prop));
                    }).finally(function() {
                        // todo remove loader
                    });
                }
            }

            // get remote modal content
            // ---------------
            function getRemoteModal(name) {
                return $q(function(resolve, reject) {
                    $http({
                        method: 'GET',
                        url: baseUrl + name + '.html'
                    }).success(function (res) {
                        resolve(res);
                    }).error(function(res) {
                        reject();
                    });
                });
            }

            // ---------------
            return {
                show: show
            }
        }
    ]);
