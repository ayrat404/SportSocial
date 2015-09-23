(function(){
var Subscribe;

Subscribe = (function() {
  function Subscribe(base, srvcConfig, RequestConstructor) {
    var facade, rqst, url, validate;
    url = srvcConfig.baseServiceUrl + '/subscribe';
    validate = function(data) {
      if (!data || !data.id || typeof data.current !== 'boolean') {
        if (srvcConfig.noticeShow.errors) {
          base.notice.show({
            text: 'Subscribe validation error',
            type: 'danger'
          });
        }
        return false;
      }
      return true;
    };
    rqst = {
      set: new RequestConstructor.klass('post', url)
    };
    facade = {
      set: function(data) {
        if (validate(data)) {
          data.actionType = data.current ? 'remove' : 'like';
          return rqst.set["do"];
        }
      }
    };
    return facade;
  }

  return Subscribe;

})();

angular.module('appSrvc').service('subscribeService', ['base', 'srvcConfig', 'RequestConstructor', Subscribe]);

})();