var Achievement;

Achievement = (function() {
  function Achievement($q, $http, base, servicesDefault) {
    var cancelTemp, getCards, getTemp, post, put, saveTemp, urlCards, urlTemp;
    urlTemp = servicesDefault.baseServiceUrl + '/achievement/temp';
    urlCards = servicesDefault.baseServiceUrl + '/achievement/cards';
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
    getCards = function() {
      return $q(function(resolve, reject) {
        return $http.get(urlCards).then(function(res) {
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
      saveTemp: saveTemp,
      getTemp: getTemp,
      cancelTemp: cancelTemp,
      getCards: getCards
    };
    return {};
  }

  return Achievement;

})();

angular.module('appSrvc').service('achievementService', ['$q', '$http', 'base', 'servicesDefault', Achievement]);
