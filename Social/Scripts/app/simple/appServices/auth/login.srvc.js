angular.module('appSrvc').service('loginSrvc', [
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

      var url = servicesDefault.baseServiceUrl + '/login',
          isSending = false;

      // ({ phone: x, password: x })
      // ---------------
      function logIn(data, prop) {
          var opts = angular.extend(servicesDefault, prop),
              evTrackProp = {
                  url: $location.path(),
                  title: $rootScope.title
              };
          return $q(function (resolve, reject) {
              if (data &&
                  data.phone &&
                  data.password &&
                  !isSending) {
                  isSending = true;
                  mixpanel.api('track', 'Login__send', evTrackProp);
                  $http.post(url, data).then(function(res) {
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
                              text: 'Login server error',
                              type: 'danger'
                          });
                  }).finally(function() {
                      isSending = false;
                  });
              } else {
                  if (opts.showNotice)
                      base.notice.show({
                          text: 'Login validation error',
                          type: 'danger'
                      });
                  reject();
              }
          });
      }

      // ---------------
      return {
          logIn: logIn
      }

  }
]);