'use strict';

// modal service
// ---------------
angular.module('shared')
    .factory('modalSrvc', [
        '$http',
        '$modal',
        'base',
        'servicesDefault',
        function ($http, $modal, base, servicesDefault) {
            var baseUrl = 'Scripts/templates/modals/';

            var modals = {
                supportSubmit: {
                    tplName: 'support/support-submit',
                    controller: 'SupportSubmitModalCtrl',
                    classname: 'fs-modal--transparent'
                },
                supportSuccess: {
                    tplName: 'support/support-success',
                    controller: 'SupportSuccessModalCtrl',
                    classname: 'fs-modal--transparent'
                },
                loginSubmit: {
                    tplName: 'auth/login-submit',
                    controller: 'LoginSubmitModalCtrl',
                    classname: 'fs-modal--transparent fs-modal--xs-content'
                },
                restorePasswordSubmitPhone: {
                    tplName: 'auth/restore-password-submit-phone',
                    controller: 'RestorePasswordSubmitPhoneModalCtrl',
                    classname: 'fs-modal--transparent fs-modal--xs-content'
                },
                restorePasswordSubmitNewData: {
                    tplName: 'auth/restore-password-submit-new-data',
                    controller: 'RestorePasswordSubmitNewModalCtrl',
                    classname: 'fs-modal--transparent fs-modal--xs-content'
                },
                policy: {
                    tplName: 'policy'
                }
            }

            // show preset modals
            // ---------------
            function show(prop) {
                // todo loader
                if (modals[prop.name] != undefined) {
                    getRemoteModal(modals[prop.name].tplName).then(function(res) {
                        $modal.open({
                            template: res,
                            controller: modals[prop.name] != undefined ? modals[prop.name].controller : null,
                            windowClass: ['fs-modal', modals[prop.name] != undefined ? modals[prop.name].classname : null].join(' '),
                            resolve: {
                                modalData: function () { return prop.data; }
                            }
                        });
                    }).finally(function() {
                        // todo loader
                    });
                } else {
                    if (servicesDefault.showNotice) base.notice.show({ text: 'Modal "' + prop.name + '" not exist', type: 'danger' });
                }
            }

            // get remote modal content
            // ---------------
            function getRemoteModal(name) {
                return $http({
                    method: 'GET',
                    url: baseUrl + name + '.html'
                });
            }

            // ---------------
            return {
                show: show
            }
        }
    ]);
