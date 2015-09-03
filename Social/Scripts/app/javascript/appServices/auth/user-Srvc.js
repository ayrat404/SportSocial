var user;

user = (function() {
  function user(store) {
    var get, set;
    set = function(user) {
      store.set('user', user);
      return user;
    };
    get = function() {
      user = store.get('user');
      if (user === void 0 || user === null) {
        return {};
      } else {
        return user;
      }
    };
    return {
      get: get,
      set: set
    };
  }

  return user;

})();

angular.module('appSrvc').service('userService', ['store', user]);
