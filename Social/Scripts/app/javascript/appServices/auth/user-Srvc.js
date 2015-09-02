var user;

user = (function() {
  function user() {
    return {};
  }

  return user;

})();

angular.module('appSrvc').service('userService', [user]);
