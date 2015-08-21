(function(){
var login;

login = (function() {
  function login($state, $location, $q, $rootScope, base, mixpanel) {
    var defaults, logIn, logOut, url;
    defaults = {
      showNotices: true
    };
    url = '/test/auth';
    logIn = function(data) {};
    logOut = function() {};
    return {
      logIn: logIn,
      logOut: logOut
    };
  }

  return login;

})();

angular.module('appSrvc').service('loginService', ['$state', '$location', '$q', '$rootScope', 'base', 'mixpanel', login]);

})();