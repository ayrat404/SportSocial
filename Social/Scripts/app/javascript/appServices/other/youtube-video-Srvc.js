var YoutubeVideo;

YoutubeVideo = (function() {
  function YoutubeVideo($http, $q, srvcConfig, base) {
    var getVideoInfo, url;
    url = srvcConfig.baseServiceUrl + '/youtube';
    getVideoInfo = function(data) {
      return $q(function(resolve, reject) {
        if (data && typeof data.link === 'string' && data.type) {
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
          return reject();
        }
      });
    };
    return {
      getVideoInfo: getVideoInfo
    };
  }

  return YoutubeVideo;

})();

angular.module('appSrvc').service('youtubeVideoService', ['$http', '$q', 'srvcConfig', 'base', YoutubeVideo]);
