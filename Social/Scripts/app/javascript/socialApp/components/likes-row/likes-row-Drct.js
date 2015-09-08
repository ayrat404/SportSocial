var LikesInRow;

LikesInRow = (function() {
  function LikesInRow() {
    return {
      restrict: 'E',
      require: 'ngModel',
      replace: true,
      scope: {
        likes: '=ngModel',
        id: '@',
        type: '@',
        opts: '@'
      },
      controller: 'likesRowController',
      templateUrl: '/template/components/likes-rowTpl',
      link: function(scope, element, attrs, ngModel) {
        var defaults;
        defaults = {
          rowCount: 4,
          showLink: true,
          imageSize: 50
        };
        return scope.o = angular.extend(defaults, eval('(' + scope.opts + ')'));
      }
    };
  }

  return LikesInRow;

})();

angular.module('socialApp.controllers').directive('likesInRow', [LikesInRow]);
