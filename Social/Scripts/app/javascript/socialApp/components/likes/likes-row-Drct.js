(function(){
var LikesInRow;

LikesInRow = (function() {
  function LikesInRow($rootScope, $timeout, likeService) {
    return {
      restrict: 'E',
      require: 'ngModel',
      replace: true,
      scope: {
        likes: '=ngModel',
        id: '@',
        entityType: '@',
        opts: '@'
      },
      controller: 'likesInRowController',
      templateUrl: '/template/components/likes/likes-rowTpl',
      link: function(scope, element, attrs, ngModel) {
        var defaults;
        defaults = {
          rowCount: 4,
          showLink: true,
          imageSize: 50
        };
        return scope.o = defaults;
      }
    };
  }

  return LikesInRow;

})();

angular.module('socialApp.directives').directive('likesInRow', ['$rootScope', '$timeout', 'likeService', LikesInRow]);

})();