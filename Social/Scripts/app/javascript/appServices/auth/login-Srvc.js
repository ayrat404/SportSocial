var login;

login = (function() {
  function login($state, $location, $q, $rootScope, base, mixpanel, servicesDefault, userService) {
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
              userService.set(res.data.data.id);
              $rootScope.user = res.data.data;
              return resolve(res.data);
            } else {
              reject(res);
              if (opts.noticeShow.errors) {
                return base.notice.response(res);
              }
            }
          }, function(res) {
            return reject(res);
          })["finally"](function() {
            return isSending = false;
          });
        } else {
          reject();
          if (opts.noticeShow.errors) {
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

angular.module('appSrvc').service('loginService', ['$state', '$location', '$q', '$rootScope', 'base', 'mixpanel', 'servicesDefault', 'userService', login]);
