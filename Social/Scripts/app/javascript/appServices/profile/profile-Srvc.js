var Profile;

Profile = (function() {
  function Profile($q, $rootScope, $http, base, mixpanel, servicesDefault) {
    var getInfo, url;
    url = servicesDefault.baseServiceUrl + '/profile';
    getInfo = function(userId) {
      return $q(function(resolve, reject) {
        if (userId && typeof userId === 'number') {
          return $http.get(url + userId, {
            params: {
              id: userId
            }
          }).then(function(res) {
            if (res.data.success) {
              return resolve(res.data);
            } else {
              reject(res.data);
              if (servicesDefault.noticeShow.errors) {
                return base.notice.response(res);
              }
            }
          }, function(res) {
            return reject(res);
          });
        } else {
          reject();
          if (servicesDefault.noticeShow.errors) {
            return base.notice.show({
              text: 'Get profile data: userId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      getInfo: getInfo
    };
  }

  return Profile;

})();

angular.module('appSrvc').service('profileService', ['$q', '$rootScope', '$http', 'base', 'mixpanel', 'servicesDefault', Profile]);
