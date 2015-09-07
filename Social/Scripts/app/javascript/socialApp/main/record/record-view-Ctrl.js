var RecordView;

RecordView = (function() {
  function RecordView($scope, $stateParams, $rootScope, journalService, mixpanel) {
    var _this;
    _this = this;
    _this.it = {
      loaded: false
    };
    journalService.getById(+$stateParams.id).then(function(res) {
      _this.it = res.data;
      return _this.it.loaded = true;
    });
  }

  return RecordView;

})();

angular.module('socialApp.controllers').controller('recordViewController', ['$scope', '$stateParams', '$rootScope', 'journalService', 'mixpanel', RecordView]);
