var registration;

registration = (function() {
  function registration($state, $location, $q, $rootScope, $http, base, mixpanel, servicesDefault) {
    var isSending, registerFirst, registerTwo, urlFirst, urlTwo;
    urlFirst = servicesDefault.baseServiceUrl + '/register/step1';
    urlTwo = servicesDefault.baseServiceUrl + '/register/step2';
    isSending = false;
    registerFirst = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.imgId && data.name && data.lastName && data.birthday && data.sportTime && data.phone && data.gender && !isSending) {
          isSending = true;
          mixpanel.api('track', 'Registration__1-step__send', evTrackProp);
          return $http.post(urlFirst, data).then(function(res) {
            if (res.data.success) {
              return resolve(res.data);
            } else {
              return reject(res.data);
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
        debugger;
        if (data && data.imgId && data.name && data.lastName && data.birthday && data.sportTime && data.phone && data.gender && data.password && data.passwordRepeat && data.password === data.passwordRepeat && data.code && !isSending) {
          mixpanel.api('track', 'Registration__2-step__send', evTrackProp);
          return $http.post(urlTwo, data).then(function(res) {
            if (res.data.success) {
              return resolve(res.data);
            } else {
              return reject(res.data);
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

angular.module('appSrvc').service('registrationService', ['$state', '$location', '$q', '$rootScope', '$http', 'base', 'mixpanel', 'servicesDefault', registration]);
