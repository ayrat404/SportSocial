(function(){
var user;

user = (function() {
  function user($q, $http, store, srvcConfig) {
    var get, getFilterProp, getList, set, urlBase, urlFilter;
    set = function(user) {
      store.set('user', user);
      return user;
    };
    get = function() {
      user = store.get('user');
      if (user === void 0 || user === null) {
        return {};
      } else {
        return user;
      }
    };
    urlBase = srvcConfig.baseServiceUrl + '/users';
    urlFilter = srvcConfig.baseServiceUrl + '/users/filter';
    getList = function(data) {
      return $q(function(resolve, reject) {
        return $http.get(urlBase, {
          params: data
        }).then(function(res) {
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
    getFilterProp = function() {
      return $q(function(resolve, reject) {
        return $http.get(urlFilter).then(function(res) {
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
      get: get,
      set: set,
      getFilterProp: getFilterProp,
      getList: getList
    };
  }

  return user;

})();

angular.module('appSrvc').service('userService', ['$q', '$http', 'store', 'srvcConfig', user]);

})();