(function(){
var LikesInRow;

LikesInRow = (function() {
  function LikesInRow($scope, $rootScope, $timeout, likeService) {
    $scope.like = function() {
      if (!$scope.loading) {
        $scope.loading = true;
        return likeService.set({
          id: $scope.id,
          entityType: $scope.entityType,
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
        })["finally"](function(res) {
          return $timeout(function() {
            return $scope.loading = false;
          });
        });
      }
    };
    $rootScope.$on('changeAvatar', function(event, newAvatar) {
      var index, item, j, len, ref, results;
      ref = $scope.likes.list;
      results = [];
      for (index = j = 0, len = ref.length; j < len; index = ++j) {
        item = ref[index];
        if (item.id === $rootScope.user.id) {
          $scope.likes.list[index].avatar = newAvatar;
          break;
        } else {
          results.push(void 0);
        }
      }
      return results;
    });
  }

  return LikesInRow;

})();

angular.module('socialApp.controllers').controller('likesInRowController', ['$scope', '$rootScope', '$timeout', 'likeService', LikesInRow]);

})();