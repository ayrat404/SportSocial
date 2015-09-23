(function(){
var Like;

Like = (function() {
  function Like(base, srvcConfig, RequestConstructor) {
    var facade, rqst, url, validate;
    url = srvcConfig.baseServiceUrl + '/like';
    validate = function(data) {
      if (!data || !data.entityType || !data.id || typeof data.current !== 'boolean') {
        if (srvcConfig.noticeShow.errors) {
          base.notice.show({
            text: 'Like validation error',
            type: 'danger'
          });
        }
        return false;
      }
      return true;
    };
    rqst = {
      post: new RequestConstructor.klass('post', url)
    };
    facade = {
      set: function(data) {
        if (validate(data)) {
          data.actionType = data.current ? 'remove' : 'like';
          return rqst.post["do"];
        }
      }
    };
    return facade;
  }

  return Like;

})();

angular.module('appSrvc').service('likeService', ['base', 'srvcConfig', 'RequestConstructor', Like]);

})();