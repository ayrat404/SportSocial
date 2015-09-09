var LikesRow;

LikesRow = (function() {
  function LikesRow($scope, $rootScope, likeService) {
    $scope.like = function() {
      return likeService.set({
        id: $scope.id,
        type: $scope.type,
        current: $scope.likes.isLiked
      }).then(function(res, newStatus) {
        $scope.likes.isLiked = newStatus;
        if (newStatus) {
          return $scope.likes.list.unshift({
            id: $rootScope.user.id,
            fullName: $rootScope.user.fullName,
            avatar: $rootScope.user.avatar
          });
        }
      });
    };
  }

  return LikesRow;

})();

angular.module('socialApp.controllers').controller('likesRowController', ['$scope', '$rootScope', 'likeService', LikesRow]);
