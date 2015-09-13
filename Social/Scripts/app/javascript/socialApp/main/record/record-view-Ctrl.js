var RecordView;

RecordView = (function() {
  function RecordView($scope, $stateParams, $rootScope, journalService, modalService) {
    var _this;
    $scope.$root.title = 'Fortress | Запись в дневнике';
    _this = this;
    _this.it = {
      loader: true
    };
    journalService.getById(+$stateParams.id).then(function(res) {
      _this.it = res.data;
      _this.it.loader = false;
      if ($rootScope.user.id === _this.it.author.id) {
        _this.it.isOwner = true;
      } else {
        _this.it.isOwner = false;
      }
      return _this.it.comments.form = {};
    });
    _this.edit = function() {
      return console.log('edit');
    };
    _this.remove = function() {
      return modalService.show({
        name: 'journalRemove',
        data: {
          id: _this.it.id,
          success: function(res) {
            return $state.go('main.profile', {
              userId: $rootScope.user.id
            });
          }
        }
      });
    };
    _this.share = function() {
      return modalService.show({
        name: 'socialShare',
        data: {
          text: _this.it.text,
          image: _this.it.media.length ? _this.it.media[0] : null
        }
      });
    };
  }

  return RecordView;

})();

angular.module('socialApp.controllers').controller('recordViewController', ['$scope', '$stateParams', '$rootScope', 'journalService', 'modalService', RecordView]);
