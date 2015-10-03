(function(){
var SettingsProfile;

SettingsProfile = (function() {
  function SettingsProfile($scope, $state, $stateParams, $rootScope, settingsService) {
    var _this;
    _this = this;
    _this.model = {};
    _this.prop = {};
    _this.pageError = false;
    _this.pageLoading = true;
    _this.serverValidation = {};
    _this.datepickerOptions = {
      maxDate: Date.now()
    };
    settingsService.getProfileSettings().then(function(res) {
      _this.model = res.data.model;
      return _this.prop = res.data.prop;
    }, function(res) {
      return _this.pageError = true;
    })["finally"](function(res) {
      return _this.pageLoading = false;
    });
    _this.save = function() {
      _this.pageLoading = true;
      return settingsService.saveProfileSettings(_this.model).then(function(res) {
        return console.log('success');
      }, function(res) {
        return _this.serverValidation = res.errors;
      })["finally"](function(res) {
        return _this.pageLoading = false;
      });
    };
  }

  return SettingsProfile;

})();

angular.module('socialApp.controllers').controller('settingsProfileController', ['$scope', '$state', '$stateParams', '$rootScope', 'settingsService', SettingsProfile]);

})();