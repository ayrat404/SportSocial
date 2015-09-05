var Journal;

Journal = (function() {
  function Journal($q, $http, $location, $rootScope, base, servicesDefault) {
    var remove, save, submit, url, valid, validate;
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
    submit = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (validate(data)) {
          return $http.post(url, data).then(function(res) {
            if (res.data.success) {
              resolve(res.data);
            } else {
              reject(res.data);
            }
            if (servicesDefault.noticeShow.errors) {
              return base.notice.response(res);
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
    save = function(data, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (validate(data)) {
          return $http.put(url, data).then(function(res) {
            if (res.data.success) {
              resolve(res.data);
            } else {
              reject(res.data);
            }
            if (servicesDefault.noticeShow.errors) {
              return base.notice.response(res);
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
    remove = function(itemId, prop) {
      var evTrackProp, opts;
      opts = angular.extend(servicesDefault, prop);
      evTrackProp = {
        url: $location.path(),
        title: $rootScope.title
      };
      return $q(function(resolve, reject) {
        if (itemId && typeof itemId === 'number') {
          return $http["delete"](url, {
            params: {
              id: itemId
            }
          }).then(function(res) {
            if (res.data.success) {
              resolve(res.data);
            } else {
              reject(res.data);
            }
            if (servicesDefault.noticeShow.errors) {
              return base.notice.response(res);
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
    return {
      submit: submit,
      save: save,
      remove: remove
    };
  }

  return Journal;

})();

angular.module('appSrvc').service('journalService', ['$q', '$http', '$location', '$rootScope', 'base', 'servicesDefault', Journal]);
