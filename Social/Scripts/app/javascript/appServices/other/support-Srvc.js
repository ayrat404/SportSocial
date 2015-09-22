var support;

support = (function() {
  function support($state, $location, $q, $rootScope, $http, base, mixpanel, srvcConfig) {
    var isSending, sendQuestion, url;
    url = srvcConfig.baseServiceUrl + '/support';
    isSending = false;
    sendQuestion = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(srvcConfig, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && base.validate.email(data.email) && data.name && data.problem && !isSending) {
          mixpanel.api('track', 'Support__send', evTrackProp);
          isSending = true;
          return $http.post(url, data).then(function(res) {
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
              text: 'Support validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      send: sendQuestion
    };
  }

  return support;

})();

angular.module('appSrvc').service('supportService', ['$state', '$location', '$q', '$rootScope', '$http', 'base', 'mixpanel', 'srvcConfig', support]);
