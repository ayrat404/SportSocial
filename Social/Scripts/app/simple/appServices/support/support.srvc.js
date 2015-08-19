angular.module('appSrvc').service('supportSrvc', [
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

      var url = servicesDefault.baseServiceUrl + '/support',
          isSending = false;

      // (data: { name: x, email: x, problem: x }, opts: {...})
      // ---------------
      function sendQuestion(data, prop) {
          var opts = angular.extend(servicesDefault, prop),
              evTrackProp = {
                  url: $location.path(),
                  title: $rootScope.title
              };
          return $q(function (resolve, reject) {
              if (data &&
                  base.validate.email(data.email) &&
                  data.name &&
                  data.problem &&
                  !isSending) {
                  mixpanel.api('track', 'Support__send', evTrackProp);
                  isSending = true;
                  $http.post(url, data)
                      .then(function (res) {
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
                                  text: 'Support server error',
                                  type: 'danger'
                              });
                      })
                      .finally(function() {
                      isSending = false;
                  });
              } else {
                  if (opts.showNotice)
                      base.notice.show({
                          text: 'Support validation error',
                          type: 'danger'
                      });
                  reject();
              }
          });
      }

      // ---------------
      return {
          send: sendQuestion
      }

  }
]);