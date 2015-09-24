(function(){
var SubscribersInRow;

SubscribersInRow = (function() {
  function SubscribersInRow($rootScope) {
    return {
      restrict: 'E',
      require: 'ngModel',
      replace: true,
      scope: {
        subscribers: '=ngModel',
        id: '@',
        opts: '@'
      },
      controller: 'subscribersInRowController',
      templateUrl: '/template/components/subscribers/subscribers-rowTpl',
      link: function(scope, element, attrs, ngModel) {
        var defaults;
        defaults = {
          rowCount: 4,
          showLink: true,
          imageSize: 50
        };
        scope.o = defaults;
        return scope.isOwner = +$rootScope.user.id === +scope.id;
      }
    };
  }

  return SubscribersInRow;

})();

angular.module('socialApp.directives').directive('subscribersInRow', ['$rootScope', SubscribersInRow]);

})();