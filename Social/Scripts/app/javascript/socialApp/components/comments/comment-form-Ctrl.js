(function(){
var CommentForm;

CommentForm = (function() {
  function CommentForm($scope, commentsService) {
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
    $scope.cancel = function() {
      return closeForm();
    };
  }

  return CommentForm;

})();

angular.module('socialApp.controllers').controller('commentFormController', ['$scope', 'commentsService', CommentForm]);

})();