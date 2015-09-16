var Achievement;

Achievement = (function() {
  function Achievement($q, $http, base, servicesDefault) {
    var cancelTemp, getById, getTemp, post, put, saveTemp, urlBase, urlTemp;
    urlTemp = servicesDefault.baseServiceUrl + '/achievement/temp';
    urlBase = servicesDefault.baseServiceUrl + '/achievement';
    post = function(data) {
      return $q(function(resolve, reject) {
        return $http.post(urlTemp, data).then(function(res) {
          if (res.data.success) {
            return resolve(res.data);
          } else {
            return reject(res.data);
          }
        }, function(res) {
          return reject(null);
        });
      });
    };
    put = function(data) {
      return $q(function(resolve, reject) {
        return $http.put(urlTemp, data).then(function(res) {
          if (res.data.success) {
            return resolve(res.data);
          } else {
            return reject(res.data);
          }
        }, function(res) {
          return reject(null);
        });
      });
    };
    saveTemp = function(data) {
      if (data.id === -1) {
        return post(data);
      } else {
        return put(data);
      }
    };
    getTemp = function() {
      return $q(function(resolve, reject) {
        return $http.get(urlTemp).then(function(res) {
          if (res.data.success) {
            return resolve(res.data);
          } else {
            return reject(res.data);
          }
        }, function(res) {
          return reject(null);
        });
      });
    };
    cancelTemp = function() {
      return $q(function(resolve, reject) {
        return $http["delete"](urlTemp).then(function(res) {
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
    getById = function(id) {
      return $q(function(resolve, reject) {
        if (id) {
          return $http.get(urlBase + '/' + id).then(function(res) {
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
              text: 'Achievement item get: itemId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    return {
      saveTemp: saveTemp,
      getTemp: getTemp,
      cancelTemp: cancelTemp,
      getById: getById
    };
  }

  return Achievement;

})();

angular.module('appSrvc').service('achievementService', ['$q', '$http', 'base', 'servicesDefault', Achievement]);
