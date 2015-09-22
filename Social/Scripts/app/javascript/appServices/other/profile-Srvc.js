var Profile;

Profile = (function() {
  function Profile($q, $rootScope, $http, base, mixpanel, srvcConfig) {
    var avatarUrl, getInfo, removeAvatar, url;
    url = srvcConfig.baseServiceUrl + '/profile';
    avatarUrl = srvcConfig.baseServiceUrl + '/profile/avatar';
    getInfo = function(userId) {
      return $q(function(resolve, reject) {
        if (userId) {
          return $http.get(url, {
            params: {
              id: userId
            }
          }).then(function(res) {
            if (res.data.success) {
              return resolve(res.data);
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
              text: 'Get profile data: userId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    removeAvatar = function() {
      return $q(function(resolve, reject) {
        return $http["delete"](avatarUrl).then(function(res) {
          if (res.data.success) {
            return resolve(res.data);
          } else {
            return reject(res.data);
          }
        }, function(res) {
          return reject(res);
        });
      });
    };
    return {
      getInfo: getInfo,
      removeAvatar: removeAvatar
    };
  }

  return Profile;

})();

angular.module('appSrvc').service('profileService', ['$q', '$rootScope', '$http', 'base', 'mixpanel', 'srvcConfig', Profile]);
