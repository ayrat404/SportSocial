var remoteTemplateSrvc;

remoteTemplateSrvc = (function() {
  function remoteTemplateSrvc($http, $q) {
    ({
      baseUrl: '/Scripts/templates/common/'
    });
    return {
      get: function(template) {
        return $q(function(resolve, reject) {
          return $http({
            method: 'GET',
            url: baseUrl + template + '.html'
          }).success(function(res) {
            return resolve(res);
          }).error(function() {
            return reject();
          });
        });
      }
    };
  }

  return remoteTemplateSrvc;

})();

angular.module('shared').factory('removeTemplateSrvc', ['$http', '$q', remoteTemplateSrvc]);
