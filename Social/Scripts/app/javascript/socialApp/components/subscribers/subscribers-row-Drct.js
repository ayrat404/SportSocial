(function(){
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
        $scope.subscribe = function() {
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
        return $rootScope.$on('changeAvatar', function(event, newAvatar) {
          var index, item, j, len, ref, results;
          ref = $scope.subscribers.list;
          results = [];
          for (index = j = 0, len = ref.length; j < len; index = ++j) {
            item = ref[index];
            if (item.id === $rootScope.user.id) {
              $scope.subscribers.list[index].avatar = newAvatar;
              break;
            } else {
              results.push(void 0);
            }
          }
          return results;
        });
      },
      templateUrl: '/template/components/subscribers/subscribers-rowTpl',
      link: function(scope, element, attrs, ngModel) {
        var defaults;
        defaults = {
          rowCount: 4,
          showLink: true,
          imageSize: 50
        };
        scope.o = defaults;
        return scope.isOwner = +$rootScope.user.id === +scope.id;
      }
    };
  }

  return SubscribersInRow;

})();

angular.module('socialApp.directives').directive('subscribersInRow', ['$rootScope', '$timeout', 'subscribeService', SubscribersInRow]);

})();