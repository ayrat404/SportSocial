var GlobalLoader;

GlobalLoader = (function() {
  function GlobalLoader($rootScope) {
    var add, remove, states;
    states = [];
    add = function(stateName) {
      if (states.indexOf(stateName === -1)) {
        states.push(stateName);
      }
      if ($rootScope.loader !== true) {
        return $rootScope.loader = true;
      }
    };
    remove = function(stateName) {
      var index;
      index = states.indexOf(stateName);
      if (index !== -1) {
        states.splice(index, 1);
        if (!states.length) {
          return $rootScope.loader = false;
        }
      }
    };
    return {
      add: add,
      remove: remove
    };
  }

  return GlobalLoader;

})();

angular.module('shared').service('globalLoaderService', ['$rootScope', GlobalLoader]);
