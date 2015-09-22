var Tape;

Tape = (function() {
  function Tape($q, $http, base, srvcConfig) {
    var getList, url;
    url = srvcConfig.baseServiceUrl + '/tape';
    getList = function(data) {
      return $q(function(resolve, reject) {
        return $http.get(url, {
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
    return {
      getList: getList
    };
  }

  return Tape;

})();

angular.module('appSrvc').service('tapeService', ['$q', '$http', 'base', 'srvcConfig', Tape]);
