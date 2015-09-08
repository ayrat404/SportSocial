var LikesRow;

LikesRow = (function() {
  function LikesRow($scope) {
    $scope.like = function() {
      return console.log('like ' + $scope.type + ' with id=' + $scope.id);
    };
  }

  return LikesRow;

})();

angular.module('socialApp.controllers').controller('likesRowController', ['$scope', LikesRow]);
