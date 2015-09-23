(function(){
var RequestConstructor,
  bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; };

RequestConstructor = (function() {
  function RequestConstructor($q, $http) {
    var RC;
    RC = (function() {
      function RC(type, url, validate, success) {
        this.type = type;
        this.url = url;
        this.validate = validate;
        this.success = success;
        this["do"] = bind(this["do"], this);
      }

      RC.prototype.isValid = function(data) {
        if (typeof this.validate === "function") {
          return this.validate(data);
        } else {
          return true;
        }
      };

      RC.prototype["do"] = function(data) {
        var _this;
        _this = this;
        return $q(function(resolve, reject) {
          if (_this.isValid(data)) {
            if (typeof data === 'object' && _this.type === 'get') {
              data = {
                params: data
              };
            }
            return $http[_this.type](_this.url, data).then(function(res) {
              if (res.data.success) {
                resolve(res.data);
                return typeof _this.success === "function" ? _this.success(res.data) : void 0;
              } else {
                return reject(res.data);
              }
            }, function(res) {
              return reject(res);
            });
          } else {
            return reject();
          }
        });
      };

      return RC;

    })();
    return {
      klass: RC
    };
  }

  return RequestConstructor;

})();

angular.module('appSrvc').factory('RequestConstructor', ['$q', '$http', RequestConstructor]);

})();