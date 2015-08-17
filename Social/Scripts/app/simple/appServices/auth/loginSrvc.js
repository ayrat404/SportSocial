angular.module('appSrvc').service('loginSrvc', [
  '$state',
  '$location',
  '$q',
  '$rootScope',
  'base',
  'mixpanel',
  function ($state, $location, $q, $rootScope, base, mixpanel) {
      var defaults, logIn, logOut, url;
      defaults = { showNotices: true };
      url = '/test/auth';
      logIn = function (data) {
      };
      logOut = function () {
      };
      return {
          logIn: logIn,
          logOut: logOut
      };
  }
]);