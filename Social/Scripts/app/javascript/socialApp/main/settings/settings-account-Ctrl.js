(function(){
var SettingsAccount;

SettingsAccount = (function() {
  function SettingsAccount($scope, $state, $stateParams, $rootScope, settingsService, modalService) {
    var _this;
    _this = this;
    _this.model = {};
    _this.pageError = false;
    _this.pageLoading = true;
    _this.passwordLoading = false;
    _this.passwordValidation = {};
    settingsService.getAccountSettings().then(function(res) {
      return _this.model = res.data;
    }, function(res) {
      return _this.pageError = true;
    })["finally"](function(res) {
      return _this.pageLoading = false;
    });
    _this.changePassword = function() {
      _this.passwordLoading = true;
      return settingsService.changePassword(_this.pass).then(function(res) {
        var k, ref, results, v;
        ref = _this.pass;
        results = [];
        for (k in ref) {
          v = ref[k];
          results.push(_this.pass[k] = '');
        }
        return results;
      }, function(res) {
        return _this.passwordValidation = res.errors;
      })["finally"](function(res) {
        return _this.passwordLoading = false;
      });
    };
  }

  return SettingsAccount;

})();

angular.module('socialApp.controllers').controller('settingsAccountController', ['$scope', '$state', '$stateParams', '$rootScope', 'settingsService', 'modalService', SettingsAccount]);

})();