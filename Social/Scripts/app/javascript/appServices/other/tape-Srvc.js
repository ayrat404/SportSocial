(function(){
var Tape;

Tape = (function() {
  function Tape(srvcConfig, RequestConstructor) {
    var facade, rqst, url;
    url = srvcConfig.baseServiceUrl + '/tape';
    rqst = {
      getList: new RequestConstructor.klass('get', url)
    };
    facade = {
      getList: rqst.getList["do"]
    };
    return facade;
  }

  return Tape;

})();

angular.module('appSrvc').service('tapeService', ['srvcConfig', 'RequestConstructor', Tape]);

})();