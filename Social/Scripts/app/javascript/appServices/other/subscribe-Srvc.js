(function(){
var Subscribe;

Subscribe = (function() {
  function Subscribe($q, $http, base, srvcConfig) {
    var set, url;
    url = srvcConfig.baseServiceUrl + '/subscribe';
    set = function(data) {
      return $q(function(resolve, reject) {
        if (data && data.id && typeof data.current === 'boolean') {
          data.actionType = data.current ? 'unsubscribe' : 'subscribe';
          return $http.post(url, data).then(function(res) {
            if (res.data.success) {
              return resolve(!data.current);
            } else {
              return reject(res.data);
            }
          }, function(res) {
            return reject(res);
          });
        } else {
          reject();
          if (srvcConfig.noticeShow.errors) {
            return base.notice.show({
              text: 'Subscribe validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      set: set
    };
  }

  return Subscribe;

})();

angular.module('appSrvc').service('subscribeService', ['$q', '$http', 'base', 'srvcConfig', Subscribe]);

})();