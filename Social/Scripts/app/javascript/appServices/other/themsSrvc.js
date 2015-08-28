(function(){
var SportThemes;

SportThemes = (function() {
  function SportThemes($http, $q, servicesDefault, base) {
    var get, url;
    url = servicesDefault.baseServiceUrl + '/sport_themes';
    get = function(search) {
      return $q(function(resolve, reject) {
        if (search && search.length) {
          return $http.get(url, {
            query: search
          }).then(function(res) {
            if (res.success) {
              return resolve(res.data);
            } else {
              return reject(res);
            }
          }, function(res) {
            if (servicesDefault.showNotice) {
              base.notice.show({
                text: 'Search theme server error',
                type: 'danger'
              });
            }
            return reject(res);
          });
        } else {
          if (servicesDefault.showNotice) {
            base.notice.show({
              text: 'Search theme string error',
              type: 'danger'
            });
          }
          return reject();
        }
      });
    };
    return {
      get: get
    };
  }

  return SportThemes;

})();

angular.module('appSrvc').service('sportThemesService', ['$http', '$q', 'servicesDefault', 'base', SportThemes]);

})();