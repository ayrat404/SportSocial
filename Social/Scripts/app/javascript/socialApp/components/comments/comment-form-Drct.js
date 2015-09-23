(function(){
var commentForm;

commentForm = (function() {
  function commentForm(commentsService) {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        comments: '=ngModel',
        id: '@',
        entityType: '@'
      },
      controller: function($scope) {
        var closeForm, takeWatcher;
        if ($scope.comments.form === void 0 || $scope.comments.form === null) {
          $scope.comments.form = {};
        }
        closeForm = function() {
          $scope.open = false;
          $scope.comments.form.text = '';
          return takeWatcher();
        };
        takeWatcher = function() {
          var textWatcher;
          return textWatcher = $scope.$watch('comments.form.text', function(oldVal, newVal) {
            if (newVal && newVal.length > 3 && oldVal !== newVal) {
              $scope.open = true;
              return textWatcher();
            }
          });
        };
        takeWatcher();
        $scope.submit = function() {
          var data;
          data = {
            entityType: $scope.entityType,
            entityId: $scope.id
          };
          if ($scope.comments.form.isAnswer) {
            data.commentType = 'answer';
            data.commentForId = $scope.comments.commentModel.id;
            data.text = $scope.comments.form.text.substr($scope.comments.commentModel.author.fullName.length + 2, $scope.comments.form.text.length - 1);
          } else {
            data.commentType = 'comment';
            data.text = $scope.comments.form.text;
          }
          return commentsService.submit(data).then(function(res) {
            $scope.comments.list.unshift(res.data);
            return closeForm();
          });
        };
        return $scope.cancel = function() {
          return closeForm();
        };
      },
      templateUrl: '/template/components/comments/comment-formTpl',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return commentForm;

})();

angular.module('socialApp.directives').directive('commentForm', ['commentsService', commentForm]);

})();