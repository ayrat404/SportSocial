angular.module('appSrvc').service('restorePasswordSrvc', [
  '$state',
  '$location',
  '$q',
  '$rootScope',
  '$http',
  'base',
  'mixpanel',
  'servicesDefault',
  function (
      $state,
      $location,
      $q,
      $rootScope,
      $http,
      base,
      mixpanel,
      servicesDefault) {

      var url = servicesDefault.baseServiceUrl + '/restorePassword',
          isPhoneSending = false,
          isNewPassSending = false;

      // ({ phone: x })
      // ---------------
      function sendPhone(data, prop) {
          var opts = angular.extend(servicesDefault, prop),
              evTrackProp = {
                  url: $location.path(),
                  title: $rootScope.title
              };
          return $q(function (resolve, reject) {
              if (data &&
                  data.phone &&
                  !isPhoneSending) {
                  isPhoneSending = true;
                  mixpanel.api('track', 'RestorePassword__phone-send', evTrackProp);
                  send('phone', data).then(function(res) {
                      if (res.success) {
                          resolve(res);
                      } else {
                          reject(res);
                      }
                      if (opts.showNotice)
                          base.notice.response(res);
                  }, function(res) {
                      reject(res);
                      if (opts.showNotice)
                          base.notice.show({
                              text: 'Restore phone server error',
                              type: 'danger'
                          });
                  }).finally(function() {
                      isPhoneSending = false;
                  });
              } else {
                  if (opts.showNotice)
                      base.notice.show({
                          text: 'Restore phone validation error',
                          type: 'danger'
                      });
                  reject();
              }
          });
      }

      // ({ password: x, passwordRepeat: x, code: x })
      // ---------------
      function sendNewPassword(data, prop) {
          var opts = angular.extend(servicesDefault, prop),
              evTrackProp = {
                  url: $location.path(),
                  title: $rootScope.title
              };
          return $q(function (resolve, reject) {
              if (data &&
                  data.phone &&
                  data.password &&
                  !isNewPassSending) {
                  isNewPassSending = true;
                  mixpanel.api('track', 'RestorePassword__new-password-send', evTrackProp);
                  send('new', data).then(function (res) {
                      if (res.success) {
                          resolve(res);
                      } else {
                          reject(res);
                      }
                      if (opts.showNotice)
                          base.notice.response(res);
                  }, function (res) {
                      reject(res);
                      if (opts.showNotice)
                          base.notice.show({
                              text: 'Restore new password server error',
                              type: 'danger'
                          });
                  }).finally(function () {
                      isNewPassSending = false;
                  });
              } else {
                  if (opts.showNotice)
                      base.notice.show({
                          text: 'Restore new password validation error',
                          type: 'danger'
                      });
                  reject();
              }
          });
      }

      // ---------------
      function send(action, data) {
          return $http.post([url, action].join('/'), data);
      }

      // ---------------
      return {
          sendPhone: sendPhone,
          sendNewPassword: sendNewPassword
      }

  }
]);