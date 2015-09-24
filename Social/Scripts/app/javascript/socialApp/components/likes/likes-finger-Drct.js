(function(){
var LikesInFinger;

LikesInFinger = (function() {
  function LikesInFinger() {
    return {
      restrict: 'E',
      require: 'ngModel',
      replace: true,
      scope: {
        likes: '=ngModel',
        id: '@',
        entityType: '@'
      },
      controller: 'likesInFingerController',
      templateUrl: '/template/components/likes/likes-fingerTpl'
    };
  }

  return LikesInFinger;

})();

angular.module('socialApp.directives').directive('likesInFinger', [LikesInFinger]);

})();