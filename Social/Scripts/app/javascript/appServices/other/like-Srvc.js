var Like;

Like = (function() {
  function Like($q, $http, $location, $rootScope, base, srvcConfig) {
    var set, url;
    url = srvcConfig.baseServiceUrl + '/like';
    set = function(data) {
      return $q(function(resolve, reject) {
        if (data && data.entityType && data.id && typeof data.current === 'boolean') {
          data.actionType = data.current ? 'remove' : 'like';
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
              text: 'Like validation error',
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

  return Like;

})();

angular.module('appSrvc').service('likeService', ['$q', '$http', '$location', '$rootScope', 'base', 'srvcConfig', Like]);
