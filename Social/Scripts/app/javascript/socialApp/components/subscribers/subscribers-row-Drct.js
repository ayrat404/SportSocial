var SubscribersInRow;

SubscribersInRow = (function() {
  function SubscribersInRow($rootScope, $timeout, subscribeService) {
    return {
      restrict: 'E',
      require: 'ngModel',
      replace: true,
      scope: {
        subscribers: '=ngModel',
        id: '@',
        opts: '@'
      },
      controller: function($scope) {
        return $scope.subscribe = function() {
          if (!$scope.loading) {
            $scope.loading = true;
            return subscribeService.set({
              id: $scope.id,
              current: $scope.subscribers.isSubscribed
            }).then(function(newStatus) {
              var i, j, l, len, ref, results;
              $scope.subscribers.isSubscribed = newStatus;
              if (newStatus) {
                $scope.subscribers.count++;
                return $scope.subscribers.list.unshift({
                  id: $rootScope.user.id,
                  avatar: $rootScope.user.avatar
                });
              } else {
                $scope.subscribers.count--;
                ref = $scope.subscribers.list;
                results = [];
                for (i = j = 0, len = ref.length; j < len; i = ++j) {
                  l = ref[i];
                  if (l.id === $rootScope.user.id) {
                    $scope.subscribes.list.splice(i, 1);
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
      },
      templateUrl: '/template/components/subscribers/subscribers-rowTpl',
      link: function(scope, element, attrs, ngModel) {
        var defaults;
        defaults = {
          rowCount: 4,
          showLink: true,
          imageSize: 50
        };
        return scope.o = defaults;
      }
    };
  }

  return SubscribersInRow;

})();

angular.module('socialApp.directives').directive('subscribersInRow', ['$rootScope', '$timeout', 'subscribeService', SubscribersInRow]);
