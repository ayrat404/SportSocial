var Profile;

Profile = (function() {
  function Profile($q, $rootScope, $http, base, mixpanel, servicesDefault) {
    var getInfo, url;
    url = servicesDefault.baseServiceUrl + '/profile';
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
