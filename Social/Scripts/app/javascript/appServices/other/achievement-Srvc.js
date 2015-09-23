var Achievement,
  bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

Achievement = (function() {
  function Achievement($q, $http, base, srvcConfig) {
    var Request, cancelTempRqst, getById, getFilterPropRqst, getListRqst, getTempRqst, postRqst, putRqst, saveTemp, url, voiceRqst;
    url = {
      base: srvcConfig.baseServiceUrl + '/achievement',
      temp: '/temp',
      voice: '/voice',
      filter: '/filter'
    };
    Request = (function() {
      function Request(type, path, validate) {
        this.type = type;
        this.validate = validate;
        this["do"] = bind(this["do"], this);
        this.url = url.base + (path ? path : '');
      }

      Request.prototype.isValid = function() {
        if (this.validate && typeof this.validate.func === "function") {
          return this.validate.func();
        } else {
          return true;
        }
      };

      Request.prototype["do"] = function(data) {
        var _this;
        _this = this;
        return $q(function(resolve, reject) {
          var base1;
          if (_this.isValid(data)) {
            return $http[_this.type](_this.url, data).then(function(res) {
              if (res.data.success) {
                return resolve(res.data);
              } else {
                return reject(res.data);
              }
            }, function(res) {
              return reject(res);
            });
          } else {
            reject();
            return typeof (base1 = _this.validate).onFail === "function" ? base1.onFail() : void 0;
          }
        });
      };

      return Request;

    })();
    postRqst = new Request('post', url.temp);
    putRqst = new Request('put', url.temp);
    getTempRqst = new Request('get', url.temp);
    cancelTempRqst = new Request('delete', url.temp);
    saveTemp = function(data) {
      if (data.id === -1) {
        return postRqst["do"](data);
      } else {
        return putRqst["do"](data);
      }
    };
    getById = function(id) {
      return $q(function(resolve, reject) {
        if (id) {
          return $http.get(url.base + "/" + id).then(function(res) {
            if (res.data.success) {
              return resolve.res.data;
            } else {
              return reject(res.data);
            }
          }, function(res) {
            return reject(res);
          });
        } else {
          reject();
          if (srvcConfig.noticeShow.errors) {
            return base.notice.show({
              text: 'Achievement item get: itemId variable error',
              type: 'danger'
            });
          }
        }
      });
    };
    voiceRqst = new Request('post', url.voice, {
      func: function(data) {
        if (data.id && data.action) {
          return true;
        } else {
          return false;
        }
      },
      onFail: function() {
        if (srvcConfig.noticeShow.errors) {
          return base.notice.show({
            text: 'Achievement voice: validate error',
            type: 'danger'
          });
        }
      }
    });
    getListRqst = new Request('get');
    getFilterPropRqst = new Request('get', url.filter);
    return {
      saveTemp: saveTemp,
      getById: getById,
      getTemp: getTempRqst["do"],
      cancelTemp: cancelTempRqst["do"],
      voice: voiceRqst["do"],
      getFilterProp: getFilterPropRqst["do"],
      getList: function(data) {
        return getListRqst["do"]({
          params: data
        });
      }
    };
  }

  return Achievement;

})();

angular.module('appSrvc').service('achievementService', ['$q', '$http', 'base', 'srvcConfig', Achievement]);
