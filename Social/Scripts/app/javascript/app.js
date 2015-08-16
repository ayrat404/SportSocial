(function(){
var tmplUrl, tmplView;
angular.module('app', [
  'ui.router',
  'ngCookies',
  'shared',
  'appSrvc',
  'socialApp'
].config([
  '$stateProvider',
  '$locationProvider',
  '$httpProvider',
  function ($stateProvider, $locationProvider, $httpProvider) {
  },
  tmplUrl = '/Scripts/templates/',
  tmplView = function (viewUrl) {
    return tmplUrl + viewUrl + '.html';
  }
]));
}).call(this);