(function(){
var app;

app = angular.module('appSrvc', []).constant('srvcConfig', {
  version: '0.0.1',
  storeName: 'srvc',
  noticeShow: {
    errors: true
  },
  baseServiceUrl: '/api'
});

})();