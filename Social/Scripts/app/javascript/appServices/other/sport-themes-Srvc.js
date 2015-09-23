(function(){
var SportThemes;

SportThemes = (function() {
  function SportThemes(srvcConfig, base, RequestConstructor) {
    var facade, rqst, url;
    url = srvcConfig.baseServiceUrl + '/sport_themes';
    rqst = {
      get: new RequestConstructor.klass('get', url, function(data) {
        if (!data || !data.query) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Search theme string error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      })
    };
    facade = {
      get: rqst.get["do"]
    };
    return facade;
  }

  return SportThemes;

})();

angular.module('appSrvc').service('sportThemesService', ['srvcConfig', 'base', 'RequestConstructor', SportThemes]);

})();