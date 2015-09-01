var modal;

modal = (function() {
  function modal($http, $modal, base, globalLoaderService, servicesDefault, templateUrl) {
    var modalBaseUrl, modals, show;
    modalBaseUrl = function(path) {
      return templateUrl + '/modals/' + path;
    };
    modals = {
      supportSubmit: {
        tplName: modalBaseUrl('support/support-submit'),
        controller: 'supportSubmitModalController',
        classname: 'fs-modal--transparent'
      },
      supportSuccess: {
        tplName: modalBaseUrl('support/support'),
        controller: 'supportSuccessModalController',
        classname: 'fs-modal--transparent'
      },
      loginSubmit: {
        tplName: modalBaseUrl('auth/login-submit'),
        controller: 'loginSubmitModalController',
        classname: 'fs-modal--transparent fs-modal--xs-content'
      },
      restorePasswordSubmitPhone: {
        tplName: modalBaseUrl('auth/restore-password-submit-phone'),
        controller: 'restorePasswordSubmitPhoneModalController',
        classname: 'fs-modal--transparent fs-modal--xs-content'
      },
      restorePasswordSubmitNewData: {
        tplName: modalBaseUrl('auth/restore-password-submit-new-data'),
        controller: 'restorePasswordSubmitNewModalController',
        classname: 'fs-modal--transparent fs-modal--xs-content'
      },
      aboutAchievement: {
        tplName: modalBaseUrl('achievement/about'),
        controller: 'aboutAchievementModalController',
        classname: 'fs-modal--transparent'
      },
      journalSubmit: {
        tplName: templateUrl + '/journal/submit',
        controller: 'journalModalSubmitController',
        classname: 'fs-modal--transparent'
      },
      apiModal: {
        tplName: modalBaseUrl('dev/api')
      },
      policy: {
        tplName: modalBaseUrl('policy')
      }
    };
    show = function(prop) {
      globalLoaderService.add('load-modal');
      if (modals[prop.name] !== void 0) {
        return $http.get(modals[prop.name].tplName).then(function(res) {
          $modal.open({
            template: res.data,
            controller: modals[prop.name] !== void 0 ? modals[prop.name].controller : null,
            windowClass: ['fs-modal', modals[prop.name] !== void 0 ? modals[prop.name].classname : null].join(' '),
            resolve: {
              modalData: function() {
                return prop.data;
              }
            }
          });
        })["finally"](function() {
          return globalLoaderService.remove('load-modal');
        });
      } else if (servicesDefault.noticeShow.errors) {
        return base.notice.show({
          text: 'Modal "' + prop.name + '" not exist',
          type: 'danger'
        });
      }
    };
    return {
      show: show
    };
  }

  return modal;

})();

angular.module('shared').service('modalService', ['$http', '$modal', 'base', 'globalLoaderService', 'servicesDefault', 'templateUrl', modal]);
