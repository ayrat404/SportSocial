(function(){
var Complain;

Complain = (function() {
  function Complain($q, $http, $location, $rootScope, base, srvcConfig, RequestConstructor) {
    var facade, submitRqst, url;
    url = srvcConfig.baseServiceUrl + '/complain';
    submitRqst = new RequestConstructor.klass('post', url, function(data) {
      if (!data || !data.entityId || !data.userId || !data.type || !data.text) {
        if (srvcConfig.noticeShow.errors) {
          base.notice.show({
            text: 'Complain submit validate error',
            type: 'danger'
          });
        }
        return false;
      }
      return true;
    });
    facade = {
      submit: submitRqst["do"]
    };
    return facade;
  }

  return Complain;

})();

angular.module('appSrvc').service('complainService', ['$q', '$http', '$location', '$rootScope', 'base', 'srvcConfig', 'RequestConstructor', Complain]);

})();