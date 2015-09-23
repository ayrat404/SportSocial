(function(){
var Achievement;

Achievement = (function() {
  function Achievement(base, srvcConfig, RequestConstructor) {
    var baseUrl, facade, rqst, url;
    baseUrl = srvcConfig.baseServiceUrl + '/achievement';
    url = {
      list: baseUrl + "s",
      temp: baseUrl + "/temp",
      voice: baseUrl + "/voice",
      filter: baseUrl + "/filter"
    };
    rqst = {
      post: new RequestConstructor.klass('post', url.temp),
      put: new RequestConstructor.klass('put', url.temp),
      getTemp: new RequestConstructor.klass('get', url.temp),
      cancelTemp: new RequestConstructor.klass('delete', url.temp),
      getList: new RequestConstructor.klass('get', url.list),
      getFilterProp: new RequestConstructor.klass('get', url.filter),
      getById: new RequestConstructor.klass('get', baseUrl, function(data) {
        if (!data || !data.id) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Achievement item get: itemId variable error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      voice: new RequestConstructor.klass('post', url.voice, function(data) {
        if (!data || !data.id || !data.action) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Achievement voice: validate error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      })
    };
    facade = {
      saveTemp: function(data) {
        if (data.id === -1) {
          return rqst.post["do"](data);
        } else {
          return rqst.put["do"](data);
        }
      },
      getById: rqst.getById["do"],
      getTemp: rqst.getTempRqst["do"],
      cancelTemp: rqst.cancelTempRqst["do"],
      voice: rqst.voiceRqst["do"],
      getFilterProp: rqst.getFilterPropRqst["do"],
      getList: rqst.getListRqst["do"]
    };
    return facade;
  }

  return Achievement;

})();

angular.module('appSrvc').service('achievementService', ['base', 'srvcConfig', 'RequestConstructor', Achievement]);

})();