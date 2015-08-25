(function(){
var modal;

modal = (function() {
  function modal($http, $modal, base, servicesDefault) {
    var baseUrl, getRemoteModal, modals, show;
    baseUrl = 'Scripts/templates/modals/';
    modals = {
      supportSubmit: {
        tplName: 'support/support-submit',
        controller: 'supportSubmitModalController',
        classname: 'fs-modal--transparent'
      },
      supportSuccess: {
        tplName: 'support/support',
        controller: 'supportSuccessModalController',
        classname: 'fs-modal--transparent'
      },
      loginSubmit: {
        tplName: 'auth/login-submit',
        controller: 'loginSubmitModalController',
        classname: 'fs-modal--transparent fs-modal--xs-content'
      },
      restorePasswordSubmitPhone: {
        tplName: 'auth/restore-password-submit-phone',
        controller: 'restorePasswordSubmitPhoneModalController',
        classname: 'fs-modal--transparent fs-modal--xs-content'
      },
      restorePasswordSubmitNewData: {
        tplName: 'auth/restore-password-submit-new-data',
        controller: 'restorePasswordSubmitNewModalController',
        classname: 'fs-modal--transparent fs-modal--xs-content'
      },
      policy: {
        tplName: 'policy'
      }
    };
    getRemoteModal = function(name) {
      return $http({
        method: 'GET',
        url: baseUrl + name + '.html'
      });
    };
    show = function(prop) {
      if (modals[prop.name] !== void 0) {
        return getRemoteModal(modals[prop.name].tplName).then(function(res) {
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
        })["finally"](function() {});
      } else if (servicesDefault.showNotice) {
        return base.notice.show({
          text: 'Modal "' + prop.name + ' not exist',
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

angular.module('shared').service('modalService', ['$http', '$modal', 'base', 'servicesDefault', modal]);

})();