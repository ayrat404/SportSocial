var restorePassword;

restorePassword = (function() {
  function restorePassword($state, $location, $q, $rootScope, $http, base, mixpanel, srvcConfig) {
    var isNewPassSending, isPhoneSending, sendNewPassword, sendPhone, urlOne, urlTwo;
    urlOne = srvcConfig.baseServiceUrl + '/restore_password_one';
    urlTwo = srvcConfig.baseServiceUrl + '/restore_password_two';
    isPhoneSending = false;
    isNewPassSending = false;
    sendPhone = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(srvcConfig, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.phone && !isPhoneSending) {
          isPhoneSending = true;
          mixpanel.api('track', 'RestorePassword__phone-send', evTrackProp);
          return $http(urlOne, data).then(function(res) {
            if (res.data.success) {
              return resolve(res.data);
            } else {
              return reject(res.data);
            }
          }, function(res) {
            return reject(res);
          })["finally"](function() {
            return isPhoneSending = false;
          });
        } else {
          reject();
          if (opts.noticeShow.errors) {
            return base.notice.show({
              text: 'Restore phone validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    sendNewPassword = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(srvcConfig, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && data.phone && data.password && !isNewPassSending) {
          isNewPassSending = true;
          mixpanel.api('track', 'RestorePassword__new-password-send', evTrackProp);
          return $http(urlTwo, data).then(function(res) {
            if (res.success) {
              return resolve(res);
            } else {
              return reject(res);
            }
          }, function(res) {
            return reject(res);
          })["finally"](function() {
            return isNewPassSending = false;
          });
        } else {
          reject();
          if (opts.noticeShow.errors) {
            return base.notice.show({
              text: 'Restore new password validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      sendPhone: sendPhone,
      sendNewPassword: sendNewPassword
    };
  }

  return restorePassword;

})();

angular.module('appSrvc').service('restorePasswordService', ['$state', '$location', '$q', '$rootScope', '$http', 'base', 'mixpanel', 'srvcConfig', restorePassword]);
