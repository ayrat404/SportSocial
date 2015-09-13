var LikesInRow;

LikesInRow = (function() {
  function LikesInRow($rootScope, likeService) {
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
      controller: function($scope) {
        return $scope.like = function() {
          return likeService.set({
            id: $scope.id,
            entityType: $scope.type,
            current: $scope.likes.isLiked
          }).then(function(newStatus) {
            var i, j, l, len, ref, results;
            $scope.likes.isLiked = newStatus;
            if (newStatus) {
              $scope.likes.count++;
              return $scope.likes.list.unshift({
                id: $rootScope.user.id,
                fullName: $rootScope.user.fullName,
                avatar: $rootScope.user.avatar
              });
            } else {
              $scope.likes.count--;
              ref = $scope.likes.list;
              results = [];
              for (i = j = 0, len = ref.length; j < len; i = ++j) {
                l = ref[i];
                if (l.id === $rootScope.user.id) {
                  $scope.likes.list.splice(i, 1);
                  break;
                } else {
                  results.push(void 0);
                }
              }
              return results;
            }
          });
        };
      },
      templateUrl: '/template/components/likes/likes-rowTpl',
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

angular.module('socialApp.directives').directive('likesInRow', ['$rootScope', 'likeService', LikesInRow]);
