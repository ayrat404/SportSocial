angular.module('appSrvc').service('registrationSrvc', [
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
      
      var url = servicesDefault.baseServiceUrl + '/registration',
          isSending = false;

      // (data: { name: x, sername: x, birthday: x, gender: x, sportTime: x  }, opts: {...})
      // ---------------
      function registerFirst(data, prop) {
          var opts = angular.extend(servicesDefault, prop),
              evTrackProp = {
                  url: $location.path(),
                  title: $rootScope.title
              };
          return $q(function (resolve, reject) {
              if (data && data.imgId && !isSending) {
                  isSending = true;
                  mixpanel.api('track', 'Registration__1-step__send', evTrackProp);
                  $http.post(url, data).then(function (res) {
                      if (res.success) {
                          resolve(res);
                      } else {
                          reject(res);
                      }
                      base.notice.response(res);
                  }, function (res) {
                      reject(res);
                      if (opts.showNotice)
                          base.notice.show({
                              text: 'Registration first step server error',
                              type: 'danger'
                          });
                  }).finally(function () {
                      isSending = false;
                  });
              } else {
                  if (opts.showNotice)
                      base.notice.show({
                          text: 'Registration first step validate error',
                          type: 'danger'
                      });
                  reject();
              }
          });
      }

      return {
          registerFirst: registerFirst
      }
  }
]);