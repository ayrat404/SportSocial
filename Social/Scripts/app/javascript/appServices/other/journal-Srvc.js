var Journal;

Journal = (function() {
  function Journal($q, $http, $location, $rootScope, base, servicesDefault) {
    var getById, remove, save, submit, url, valid, validate;
    url = servicesDefault.baseServiceUrl + '/journal';
    valid = {
      minText: 50
    };
    validate = function(data) {
      if (data && data.text && data.text.length >= valid.minText) {
        return true;
      }
      return false;
    };
    submit = function(data) {
      return $q(function(resolve, reject) {
        if (validate(data)) {
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
          if (servicesDefault.noticeShow.errors) {
            return base.notice.show({
              text: 'Journal submit validation error',
              type: 'danger'
            });
          }
        }
      });
    };
    save = function(data) {
      return $q(function(resolve, reject) {
        if (validate(data)) {
          return $http.put(url, data).then(function(res) {
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
              text: 'Journal save validation error',
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
              return reject(res.data);
            }
          }, function(res) {
            return reject(res);
          });
        } else {
          reject();
          if (servicesDefault.noticeShow.errors) {
            return base.notice.show({
              text: 'Journal delete: itemId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    getById = function(itemId) {
      return $q(function(resolve, reject) {
        if (itemId) {
          return $http.get(url + '/' + itemId).then(function(res) {
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
              text: 'Journal item get: itemId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      submit: submit,
      save: save,
      remove: remove,
      getById: getById
    };
  }

  return Journal;

})();

angular.module('appSrvc').service('journalService', ['$q', '$http', '$location', '$rootScope', 'base', 'servicesDefault', Journal]);
