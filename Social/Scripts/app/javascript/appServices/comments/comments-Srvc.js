var Comments;

Comments = (function() {
  function Comments($q, $http, $location, $rootScope, base, servicesDefault) {
    var remove, submit, url, valid;
    url = servicesDefault.baseServiceUrl + '/comment';
    valid = {
      minText: 50
    };
    submit = function(data) {
      return $q(function(resolve, reject) {
        if (data) {
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
              text: 'Comment submit validate error',
              type: 'danger'
            });
          }
        }
      });
    };
    remove = function(itemId) {
      return $q(function(resolve, reject) {
        if (itemId && typeof itemId === 'number') {
          return $http["delete"](url, {
            params: {
              id: itemId
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
              text: 'Comment delete: itemId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      submit: submit,
      remove: remove
    };
  }

  return Comments;

})();

angular.module('appSrvc').service('commentsService', ['$q', '$http', '$location', '$rootScope', 'base', 'servicesDefault', Comments]);
