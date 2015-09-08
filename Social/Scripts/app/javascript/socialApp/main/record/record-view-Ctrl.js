var RecordView;

RecordView = (function() {
  function RecordView($scope, $stateParams, $rootScope, journalService, mixpanel) {
    var _this;
    _this = this;
    _this.it = {
      loader: true
    };
    journalService.getById(+$stateParams.id).then(function(res) {
      _this.it = res.data;
      _this.it.loader = false;
      if ($rootScope.user.id === _this.it.author.id) {
        return _this.it.isOwner = true;
      } else {
        return _this.it.isOwner = false;
      }
    });
    _this.edit = function() {
      return console.log('edit');
    };
    _this.remove = function() {
      return console.log('remove');
    };
  }

  return RecordView;

})();

angular.module('socialApp.controllers').controller('recordViewController', ['$scope', '$stateParams', '$rootScope', 'journalService', 'mixpanel', RecordView]);
