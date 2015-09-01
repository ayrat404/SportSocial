var registration;

registration = (function() {
  function registration($state, $location, $q, $rootScope, base, mixpanel, servicesDefault) {
    var isSending, registerFirst, registerTwo, urlFirst, urlTwo;
    urlFirst = servicesDefault.baseServiceUrl + '/register_one';
    urlTwo = servicesDefault.baseServiceUrl + '/register_two';
    isSending = false;
    registerFirst = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.imgId && data.name && data.sername && data.birthday && data.sprotTime && data.phone && data.gender && !isSending) {
          isSending = true;
          mixpanel.api('track', 'Registration__1-step__send', evTrackProp);
          return $http.post(urlFirst, data).then(function(res) {
            if (res.success) {
              resolve(res);
            } else {
              reject(res);
            }
            if (opts.noticeShow.errors) {
              return base.notice.response(res);
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
              text: 'Registration first step validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    registerTwo = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.imgId && data.name && data.sername && data.birthday && data.sprotTime && data.phone && data.gender && data.password && data.repeatPassword && data.password === data.repeatPassword && data.code && !isSending) {
          mixpanel.api('track', 'Registration__2-step__send', evTrackProp);
          return $http.post(urlTwo, data).then(function(res) {
            if (res.success) {
              return resolve(res);
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
              text: 'Registration two step validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      registerFirst: registerFirst,
      registerTwo: registerTwo
    };
  }

  return registration;

})();

angular.module('appSrvc').service('registrationService', ['$state', '$location', '$q', '$rootScope', 'base', 'mixpanel', 'servicesDefault', registration]);
