(function(){
var MediaModalShow;

MediaModalShow = (function() {
  function MediaModalShow($scope, $state, $modalInstance, $rootScope, base, journalService, modalService, modalData) {
    var doc, i, keyListener, receiveParams, setByIndex, v;
    receiveParams = ['media', 'entityType', 'index'];
    $scope.maxText = 40;
    if (modalData.media && modalData.entityType) {
      for (i in receiveParams) {
        v = receiveParams[i];
        $state.params[v] = modalData[v];
      }
      $scope.currentIndex = modalData.index !== void 0 ? +modalData.index : 1;
      $scope.entityType = modalData.entityType;
      journalService.getById({
        id: modalData.media
      }).then(function(res) {
        $scope.it = res.data;
        $scope.it.loader = false;
        if ($rootScope.user.id === $scope.it.author.id) {
          $scope.it.isOwner = true;
        } else {
          $scope.it.isOwner = false;
        }
        $scope.itemsCount = $scope.it.media.length;
        if ($scope.currentIndex > $scope.itemsCount) {
          $scope.currentIndex = 1;
        }
        return setByIndex($scope.currentIndex);
      }, function(res) {
        $modalInstance.dismiss();
        return base.notice.show({
          text: 'Record with id=' + modalData.media + ' is not defined.',
          type: 'warning'
        });
      });
      setByIndex = function(index) {
        i = index - 1;
        if ($scope.it.media[i] !== void 0) {
          $scope.current = $scope.it.media[i];
        }
        $state.params.index = index;
        return $state.transitionTo($state.current, $state.params, {
          notify: false
        });
      };
      $scope.socialShare = function() {
        return modalService.show({
          name: 'socialShare',
          data: {
            url: $state.href('main.journalIt', {
              id: $scope.it.id
            }, {
              absolute: true
            }),
            text: $scope.it.text,
            media: $scope.current.url,
            hashtags: $scope.it.tags
          }
        });
      };
      $scope.before = function() {
        if ($scope.currentIndex === 1) {
          $scope.currentIndex = $scope.itemsCount;
        } else {
          --$scope.currentIndex;
        }
        return setByIndex($scope.currentIndex);
      };
      $scope.next = function() {
        if ($scope.currentIndex === $scope.itemsCount) {
          $scope.currentIndex = 1;
        } else {
          ++$scope.currentIndex;
        }
        return setByIndex($scope.currentIndex);
      };
      doc = angular.element(document);
      keyListener = function(event) {
        if (event.which === 37) {
          $scope.before();
          return event.preventDefault();
        } else if (event.which === 39) {
          $scope.next();
          return event.preventDefault();
        }
      };
      doc.on('keydown', keyListener);
      $scope.$on('$destroy', function() {
        return doc.off('keydown', keyListener);
      });
    } else {
      $modalInstance.dismiss();
      console.log('media id or type undefined');
    }
    $modalInstance.result["catch"](function() {
      for (i in receiveParams) {
        v = receiveParams[i];
        $state.params[v] = null;
      }
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    });
  }

  return MediaModalShow;

})();

angular.module('socialApp.controllers').controller('mediaModalShowController', ['$scope', '$state', '$modalInstance', '$rootScope', 'base', 'journalService', 'modalService', 'modalData', MediaModalShow]);

})();