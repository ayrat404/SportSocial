(function(){
var Journal;

Journal = (function() {
  function Journal($q, $http, $location, $rootScope, base, srvcConfig, RequestConstructor) {
    var facade, rqst, url, urlRecords, valid, validate;
    url = srvcConfig.baseServiceUrl + '/journal';
    urlRecords = srvcConfig.baseServiceUrl + '/records';
    valid = {
      minText: 50
    };
    validate = function(data) {
      if (data && data.text && data.text.length >= valid.minText) {
        return true;
      }
      return false;
    };
    rqst = {
      post: new RequestConstructor.klass('post', url, function(data) {
        if (!validate(data)) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Minimum text length 50 symbols',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      put: new RequestConstructor.klass('put', url, function(data) {
        if (!validate(data)) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Minimum text length 50 symbols',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      remove: new RequestConstructor.klass('delete', url, function(data) {
        if (!data || !data.id) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Journal delete: itemId variable error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      getList: new RequestConstructor.klass('get', urlRecords, function(data) {
        if (!data || !data.authorId) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Records get: authorId variable error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      }),
      getById: new RequestConstructor.klass('get', url, function(itemId) {
        if (!itemId) {
          if (srvcConfig.noticeShow.errors) {
            base.notice.show({
              text: 'Journal item get: itemId variable error',
              type: 'danger'
            });
          }
          return false;
        }
        return true;
      })
    };
    facade = {
      save: function(data) {
        if (data.id) {
          return rqst.put["do"](data);
        } else {
          return rqst.post["do"](data);
        }
      },
      remove: rqst.remove["do"],
      getList: rqst.getList["do"],
      getById: rqst.getById["do"]
    };
    return facade;
  }

  return Journal;

})();

angular.module('appSrvc').service('journalService', ['$q', '$http', '$location', '$rootScope', 'base', 'srvcConfig', 'RequestConstructor', Journal]);

})();