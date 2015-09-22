var SportThemes;

SportThemes = (function() {
  function SportThemes($http, $q, srvcConfig, base) {
    var get, url;
    url = srvcConfig.baseServiceUrl + '/sport_themes';
    get = function(search) {
      return $q(function(resolve, reject) {
        if (search && search.length) {
          return $http.get(url, {
            params: {
              query: search
            }
          }).then(function(res) {
            if (res.data.success && res.data.data.length) {
              return resolve(res.data);
            } else {
              return reject(res.data);
            }
          }, function(res) {
            return reject(res);
          });
        } else {
          if (srvcConfig.noticeShow.errors) {
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

angular.module('appSrvc').service('sportThemesService', ['$http', '$q', 'srvcConfig', 'base', SportThemes]);
