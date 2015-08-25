(function(){
var support;

support = (function() {
  function support($state, $location, $q, $rootScope, $http, base, mixpanel, servicesDefault) {
    var isSending, sendQuestion, url;
    url = servicesDefault.baseServiceUrl + '/support';
    isSending = false;
    sendQuestion = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (data && base.validate.email(data.email) && data.name && data.problem && !isSending) {
          mixpanel.api('track', 'Support__send', evTrackProp);
          isSending = true;
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
                text: 'Support server error',
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

angular.module('appSrvc').service('supportService', ['$state', '$location', '$q', '$rootScope', '$http', 'base', 'mixpanel', 'servicesDefault', support]);

})();