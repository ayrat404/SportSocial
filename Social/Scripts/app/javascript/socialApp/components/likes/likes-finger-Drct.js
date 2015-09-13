var LikesInFinger;

LikesInFinger = (function() {
  function LikesInFinger($rootScope, likeService) {
    return {
      restrict: 'E',
      require: 'ngModel',
      replace: true,
      scope: {
        likes: '=ngModel',
        id: '@',
        type: '@'
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
      templateUrl: '/template/components/likes/likes-fingerTpl'
    };
  }

  return LikesInFinger;

})();

angular.module('socialApp.directives').directive('likesInFinger', ['$rootScope', 'likeService', LikesInFinger]);
