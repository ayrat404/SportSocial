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
        var clearForm, closeForm, takeWatcher;
        if ($scope.comments.form === void 0 || $scope.comments.form === null) {
          $scope.comments.form = {};
        }
        closeForm = function() {
          $scope.open = false;
          return takeWatcher();
        };
        clearForm = function() {
          return $scope.comments.form.text = '';
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
          return commentsService.submit({
            text: $scope.comments.form.text,
            entityType: $scope.entityType,
            id: $scope.id
          }).then(function(res) {
            $scope.comments.list.unshift(res.data);
            clearForm();
            return closeForm();
          });
        };
        return $scope.cancel = function() {
          clearForm();
          return closeForm();
        };
      },
      templateUrl: '/template/components/comments/comment-formTpl',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return commentForm;

})();

angular.module('socialApp.controllers').directive('commentForm', ['commentsService', commentForm]);
