var Like;

Like = (function() {
  function Like($q, $http, $location, $rootScope, base, servicesDefault) {
    var set, url;
    url = servicesDefault.baseServiceUrl + '/like';
    set = function(data) {
      return $q(function(resolve, reject) {
        if (data && data.entityType && data.id && typeof data.current === 'boolean') {
          data.actionType = data.current ? 'dislike' : 'like';
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
          if (servicesDefault.noticeShow.errors) {
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

angular.module('appSrvc').service('likeService', ['$q', '$http', '$location', '$rootScope', 'base', 'servicesDefault', Like]);
