(function(){
var login;

login = (function() {
  function login($state, $location, $q, $rootScope, base, mixpanel, servicesDefault) {
    var isSending, logIn, url;
    url = servicesDefault.baseServiceUrl + '/login';
    isSending = false;
    logIn = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.phone && data.password && !isSending) {
          mixpanel.api('track', 'Login__send', evTrackProp);
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
                text: 'Login server error',
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
              text: 'Login validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      logIn: logIn
    };
  }

  return login;

})();

angular.module('appSrvc').service('loginService', ['$state', '$location', '$q', '$rootScope', 'base', 'mixpanel', 'servicesDefault', login]);

})();