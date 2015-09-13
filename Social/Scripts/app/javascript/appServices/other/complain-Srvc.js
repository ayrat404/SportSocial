var Complain;

Complain = (function() {
  function Complain($q, $http, $location, $rootScope, base, servicesDefault) {
    var submit, url;
    url = servicesDefault.baseServiceUrl + '/complain';
    submit = function(data) {
      return $q(function(resolve, reject) {
        if (data && data.entityId && data.userId && data.type && data.text) {
          return $http.post(url, data).then(function(res) {
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

angular.module('appSrvc').service('complainService', ['$q', '$http', '$location', '$rootScope', 'base', 'servicesDefault', Complain]);
