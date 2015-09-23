(function(){
var YoutubeVideo;

YoutubeVideo = (function() {
  function YoutubeVideo($http, $q, srvcConfig, RequestConstructor) {
    var facade, rqst, url;
    url = srvcConfig.baseServiceUrl + '/youtube';
    rqst = {
      getVideoInfo: new RequestConstructor.klass('post', url, function(data) {
        if (!data || !typeof data.link === 'string' || !data.type) {
          return false;
        }
        return true;
      })
    };
    facade = {
      getVideoInfo: rqst.getVideoInfo["do"]
    };
    return facade;
  }

  return YoutubeVideo;

})();

angular.module('appSrvc').service('youtubeVideoService', ['$http', '$q', 'srvcConfig', 'RequestConstructor', YoutubeVideo]);

})();