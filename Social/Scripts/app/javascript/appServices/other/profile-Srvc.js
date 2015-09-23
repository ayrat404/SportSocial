(function(){
var Profile;

Profile = (function() {
  function Profile(base, mixpanel, srvcConfig, RequestConstructor) {
    var avatarUrl, facade, rqst, url;
    url = srvcConfig.baseServiceUrl + '/profile';
    avatarUrl = srvcConfig.baseServiceUrl + '/profile/avatar';
    rqst = {
      getInfo: new RequestConstructor.klass('get', url, function(data) {
        if (!data.id) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Get profile data: userId variable error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      removeAvatar: new RequestConstructor.klass('delete', avatarUrl)
    };
    facade = {
      getInfo: rqst.getInfo["do"],
      removeAvatar: rqst.removeAvatar["do"]
    };
    return facade;
  }

  return Profile;

})();

angular.module('appSrvc').service('profileService', ['base', 'mixpanel', 'srvcConfig', 'RequestConstructor', Profile]);

})();