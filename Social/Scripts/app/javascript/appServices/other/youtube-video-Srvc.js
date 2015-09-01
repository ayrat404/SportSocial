var YoutubeVideo;

YoutubeVideo = (function() {
  function YoutubeVideo($http, $q, servicesDefault, base) {
    var getVideoInfo;
    getVideoInfo = function(link, prop) {
      if (link && typeof link === 'string' && link.length > 0) {
        return $q(function(resolve, reject) {
          return $http.get(servicesDefault.baseServiceUrl + '/youtube', {
            link: link
          }).then(function(res) {
            if (res.success) {
              return resolve(res);
            } else {
              return reject(res);
            }
          }, function(res) {
            return reject(res);
          });
        });
      } else {
        return reject();
      }
    };
    return {
      getVideoInfo: getVideoInfo
    };
  }

  return YoutubeVideo;

})();

angular.module('appSrvc').service('youtubeVideoService', ['$http', '$q', 'servicesDefault', 'base', YoutubeVideo]);
