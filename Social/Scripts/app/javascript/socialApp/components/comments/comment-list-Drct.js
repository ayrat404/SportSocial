(function(){
var commentList;

commentList = (function() {
  function commentList($window, $timeout, commentsService) {
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
        $scope.commentsLimit = 5;
        formWatcher = null;
        $scope.showMore = function() {
          return $scope.commentsLimit = $scope.comments.list.length;
        };
        $scope.answer = function(commentModel) {
          $scope.comments.form.isAnswer = true;
          $scope.comments.form.focus = !$scope.comments.form.focus;
          $scope.comments.commentModel = commentModel;
          $scope.comments.form.text = commentModel.author.fullName + ', ';
          return formWatcher = $scope.$watch('comments.form.text', function(val) {
            if (commentModel.author.fullName !== val.substr(0, commentModel.author.fullName.length)) {
              $scope.comments.form.isAnswer = false;
              return formWatcher();
            }
          });
        };
        return $scope.scrollToAnswer = function(id, repeatCall) {
          var $el;
          $el = angular.element('#comment_' + id);
          if ($el.length) {
            angular.element('html, body').animate({
              scrollTop: $el.offset().top - $window.innerHeight / 2
            }, 300);
            $el.addClass('cl__row--light');
            return $timeout(function() {
              return $el.removeClass('cl__row--light');
            }, 1000);
          } else if (repeatCall !== true) {
            $scope.showMore();
            return $timeout(function() {
              return $scope.scrollToAnswer(id, true);
            });
          }
        };
      },
      templateUrl: '/template/components/comments/comment-listTpl',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return commentList;

})();

angular.module('socialApp.directives').directive('commentList', ['$window', '$timeout', 'commentsService', commentList]);

})();