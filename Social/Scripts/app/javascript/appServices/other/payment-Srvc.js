(function(){
var Payment;

Payment = (function() {
  function Payment(base, srvcConfig, RequestConstructor) {
    var baseUrl, facade, payUrl, rqst;
    baseUrl = srvcConfig.baseServiceUrl + '/payment';
    payUrl = srvcConfig.baseServiceUrl + '/payment/pay';
    rqst = {
      getInfo: new RequestConstructor.klass('get', baseUrl),
      init: new RequestConstructor.klass('post', payUrl)
    };
    facade = {
      getInfo: rqst.getInfo["do"],
      init: rqst.init["do"]
    };
    return facade;
  }

  return Payment;

})();

angular.module('appSrvc').service('paymentService', ['base', 'srvcConfig', 'RequestConstructor', Payment]);

})();