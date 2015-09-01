var app;

app = angular.module('appSrvc', []).constant('servicesDefault', {
  noticeShow: {
    errors: true
  },
  baseServiceUrl: '/api'
});
