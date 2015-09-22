var Complain;

Complain = (function() {
  function Complain($q, $http, $location, $rootScope, base, srvcConfig) {
    var submit, url;
    url = srvcConfig.baseServiceUrl + '/complain';
    submit = function(data) {
      return $q(function(resolve, reject) {
        if (data && data.entityId && data.userId && data.type && data.text) {
          return $http.post(url, data).then(function(res) {
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
              text: 'Complain submit validate error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      submit: submit
    };
  }

  return Complain;

})();

angular.module('appSrvc').service('complainService', ['$q', '$http', '$location', '$rootScope', 'base', 'srvcConfig', Complain]);
