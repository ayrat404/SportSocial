(function(){
var Settings;

Settings = (function() {
  function Settings(base, srvcConfig, RequestConstructor) {
    var facade, rqst, urlAccount, urlPassword, urlPhoneOne, urlPhoneTwo, urlProfile;
    urlAccount = srvcConfig.baseServiceUrl + '/settings/account';
    urlProfile = srvcConfig.baseServiceUrl + '/settings/profile';
    urlPassword = srvcConfig.baseServiceUrl + '/settings/password';
    urlPhoneOne = srvcConfig.baseServiceUrl + '/settings/change_phone_one';
    urlPhoneTwo = srvcConfig.baseServiceUrl + '/settings/change_phone_two';
    rqst = {
      getAccountSettings: new RequestConstructor.klass('get', urlAccount),
      changePassword: new RequestConstructor.klass('post', urlPassword, function(data) {
        if (!data || !data.oldPassword || !data.newPassword || !data.newRepeatPassword) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Change password submit validate error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      getProfileSettings: new RequestConstructor.klass('get', urlProfile),
      saveProfileSettings: new RequestConstructor.klass('put', urlProfile),
      sendPhoneForCode: new RequestConstructor.klass('post', urlPhoneOne),
      sendPhoneWithCode: new RequestConstructor.klass('post', urlPhoneTwo)
    };
    facade = {
      getAccountSettings: rqst.getAccountSettings["do"],
      getProfileSettings: rqst.getProfileSettings["do"],
      saveProfileSettings: rqst.saveProfileSettings["do"],
      changePassword: rqst.changePassword["do"],
      sendPhoneForCode: rqst.sendPhoneForCode["do"],
      sendPhoneWithCode: rqst.sendPhoneWithCode["do"]
    };
    return facade;
  }

  return Settings;

})();

angular.module('appSrvc').service('settingsService', ['base', 'srvcConfig', 'RequestConstructor', Settings]);

})();