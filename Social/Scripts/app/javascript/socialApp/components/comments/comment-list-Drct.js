var commentList;

commentList = (function() {
  function commentList(commentsService) {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        comments: '=ngModel',
        id: '@',
        entityType: '@'
      },
      controller: function($scope) {
        var formWatcher;
        $scope.commentsLimit = 1;
        formWatcher = null;
        $scope.showMore = function() {
          return $scope.commentsLimit = $scope.comments.list.length;
        };
        return $scope.answer = function(commentModel) {
          $scope.comments.form.isAnswer = true;
          $scope.comments.form.focus = !$scope.comments.form.focus;
          $scope.comments.form.text = commentModel.author.fullName + ', ';
          return formWatcher = $scope.$watch('comments.form.text', function(val) {
            if (commentModel.author.fullName !== val.substr(0, commentModel.author.fullName.length)) {
              $scope.comments.form.isAnswer = false;
              return formWatcher();
            }
          });
        };
      },
      templateUrl: '/template/components/comments/comment-listTpl',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return commentList;

})();

angular.module('socialApp.controllers').directive('commentList', ['commentsService', commentList]);
