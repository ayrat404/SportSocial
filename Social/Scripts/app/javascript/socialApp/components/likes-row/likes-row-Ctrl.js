var LikesRow;

LikesRow = (function() {
  function LikesRow($scope, $rootScope, likeService) {
    $scope.like = function() {
      return likeService.set({
        id: $scope.id,
        entityType: $scope.type,
        current: $scope.likes.isLiked
      }).then(function(res, newStatus) {
        var i, j, l, len, ref, results;
        $scope.likes.isLiked = newStatus;
        if (newStatus) {
          return $scope.likes.list.unshift({
            id: $rootScope.user.id,
            fullName: $rootScope.user.fullName,
            avatar: $rootScope.user.avatar
          });
        } else {
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
  }

  return LikesRow;

})();

angular.module('socialApp.controllers').controller('likesRowController', ['$scope', '$rootScope', 'likeService', LikesRow]);
