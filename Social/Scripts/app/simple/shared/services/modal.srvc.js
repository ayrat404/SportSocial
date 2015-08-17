'use strict';

// mixpanel
// ---------------
angular.module('shared')
    .factory('modalSrvc', [
        '$q',
        '$http',
        '$compile',
        '$modal',
        function ($q, $http, $compile, $modal) {
            var baseUrl = 'Scripts/templates/modals/';

            var modals = {
                support: {
                    controller: 'SupportModalCtrl',
                    classname: 'fs-modal--transparent'
                }
            }

            // show preset modals
            // ---------------
            function show(prop) {
                // todo loader
                getRemoteModal(prop.name).then(function (res) {
                    var modalInstance = $modal.open({
                        template: res,
                        controller: modals[prop.name] != undefined ? modals[prop.name].controller : null,
                        windowClass: ['fs-modal', modals[prop.name] != undefined ? modals[prop.name].classname : null].join(' ')
                    });
                }).finally(function () {
                    // todo loader
                });
            }

            // get remote modal content
            // ---------------
            function getRemoteModal(name) {
                return $q(function (resolve, reject) {
                    $http({
                        method: 'GET',
                        url: baseUrl + name + '.html'
                    }).success(function (res) {
                        resolve(res);
                    }).error(function (res) {
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
