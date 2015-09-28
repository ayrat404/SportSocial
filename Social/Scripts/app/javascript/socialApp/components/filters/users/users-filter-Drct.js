(function(){
var usersFilter;

usersFilter = (function() {
  function usersFilter($timeout) {
    return {
      restrict: 'E',
      scope: {
        filter: '=',
        callback: '&',
        queryListLimit: '@'
      },
      controller: 'usersFilterController',
      controllerAs: 'uFilter',
      templateUrl: '/template/components/filters/users-filterTpl',
      link: function(scope, element, attrs, ctrl) {}
    };
  }

  return usersFilter;

})();

angular.module('socialApp.directives').directive('usersFilter', ['$timeout', usersFilter]);

})();