(function(){
var registration;

registration = (function() {
  function registration($state, $location, $q, $rootScope, base, mixpanel) {
    var defaults, register, url;
    defaults = {
      showNotices: true
    };
    url = '/test/registration';
    register = function(data) {};
    return {
      register: register
    };
  }

  return registration;

})();

angular.module('appSrvc').service('registrationService', ['$state', '$location', '$q', '$rootScope', 'base', 'mixpanel', registration]);

})();