(function(){
var Comments;

Comments = (function() {
  function Comments(base, srvcConfig, RequestConstructor) {
    var facade, rqst, url;
    url = srvcConfig.baseServiceUrl + '/comment';
    rqst = {
      post: new RequestConstructor.klass('post', url, function(data) {
        if (!data) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Comment submit validate error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      remove: new RequestConstructor.klass('delete', url, function(data) {
        if (!data || !data.id) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Comment delete: itemId variable error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      })
    };
    facade = {
      submit: rqst.post["do"],
      remove: rqst.remove["do"]
    };
    return facade;
  }

  return Comments;

})();

angular.module('appSrvc').service('commentsService', ['base', 'srvcConfig', 'RequestConstructor', Comments]);

})();