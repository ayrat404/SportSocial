var MediaModalShow;

MediaModalShow = (function() {
  function MediaModalShow($scope, $state, $modalInstance, $rootScope, base, journalService, modalService, modalData) {
    var setByIndex;
    $scope.maxText = 40;
    if (modalData.media !== void 0) {
      $state.params.media = modalData.media;
      $scope.currentIndex = modalData.index !== void 0 ? +modalData.index : 1;
      journalService.getById(modalData.media).then(function(res) {
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
        var i;
        i = index - 1;
        if ($scope.it.media[i] !== void 0) {
          $scope.current = $scope.it.media[i];
        }
        $state.params.index = index;
        return $state.transitionTo($state.current, $state.params, {
          notify: false
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
      $scope.socialShare = function() {
        return modalService.show({
          name: 'socialShare',
          data: {
            text: $scope.it.text,
            media: $scope.current.url,
            hashtags: $scope.it.tags
          }
        });
      };
      $scope.next = function() {
        if ($scope.currentIndex === $scope.itemsCount) {
          $scope.currentIndex = 1;
        } else {
          ++$scope.currentIndex;
        }
        return setByIndex($scope.currentIndex);
      };
    } else {
      console.log('media id undefined');
    }
    $modalInstance.result["catch"](function() {
      $state.params.index = null;
      $state.params.media = null;
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    });
  }

  return MediaModalShow;

})();

angular.module('socialApp.controllers').controller('mediaModalShowController', ['$scope', '$state', '$modalInstance', '$rootScope', 'base', 'journalService', 'modalService', 'modalData', MediaModalShow]);
