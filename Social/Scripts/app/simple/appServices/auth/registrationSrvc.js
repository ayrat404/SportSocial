angular.module('appSrvc').service('registrationSrvc', [
  '$state',
  '$location',
  '$q',
  '$rootScope',
  'base',
  'mixpanel',
  function ($state, $location, $q, $rootScope, base, mixpanel) {
      var defaults, register, url;
      defaults = { showNotices: true };
      url = '/test/registration';
      register = function (data) {
      };
      return { register: register };
  }
]);