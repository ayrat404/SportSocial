(function(){
var registration;

registration = (function() {
  function registration($state, $location, $q, $rootScope, base, mixpanel, servicesDefault) {
    var isSending, registerFirst, url;
    url = servicesDefault.baseServiceUrl + '/registration';
    isSending = false;
    registerFirst = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.imgId && !isSending) {
          isSending = true;
          mixpanel.api('track', 'Registration__1-step__send', evTrackProp);
          return $http.post(url, data).then(function(res) {
            if (res.success) {
              resolve(res);
            } else {
              reject(res);
            }
            if (opts.showNotice) {
              return base.notice.response(res);
            }
          }, function(res) {
            reject(res);
            if (opts.showNotice) {
              return base.notice.show({
                text: 'Registration first step server error',
                type: 'danger'
              });
            }
          })["finally"](function() {
            return isSending = false;
          });
        } else {
          reject();
          if (opts.showNotice) {
            return base.notice.show({
              text: 'Registration first step validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      registerFirst: registerFirst
    };
  }

  return registration;

})();

angular.module('appSrvc').service('registrationService', ['$state', '$location', '$q', '$rootScope', 'base', 'mixpanel', 'servicesDefault', registration]);

})();