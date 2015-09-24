(function(){
var commentForm;

commentForm = (function() {
  function commentForm() {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        comments: '=ngModel',
        id: '@',
        entityType: '@'
      },
      controller: 'commentFormController',
      templateUrl: '/template/components/comments/comment-formTpl',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return commentForm;

})();

angular.module('socialApp.directives').directive('commentForm', [commentForm]);

})();