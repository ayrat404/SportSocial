(function(){
var commentList;

commentList = (function() {
  function commentList() {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        comments: '=ngModel',
        id: '@',
        entityType: '@'
      },
      controller: 'commentListController',
      templateUrl: '/template/components/comments/comment-listTpl',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return commentList;

})();

angular.module('socialApp.directives').directive('commentList', [commentList]);

})();