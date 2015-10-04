(function(){
var PaymentIndex;

PaymentIndex = (function() {
  function PaymentIndex($sce, paymentService) {
    var _this, data, prepareTariffs;
    data = {
      tariffId: null,
      systemId: null
    };
    _this = this;
    _this.loading = true;
    _this.mode = 0;
    prepareTariffs = function(tariffs) {
      var base, i, item, len;
      base = ((function() {
        var i, len, results;
        results = [];
        for (i = 0, len = tariffs.length; i < len; i++) {
          item = tariffs[i];
          if (item.month === 1) {
            results.push(item);
          }
        }
        return results;
      })())[0];
      for (i = 0, len = tariffs.length; i < len; i++) {
        item = tariffs[i];
        if (item.id !== base.id) {
          item['profit'] = 100 - (item.cost * 100 / base.cost);
        }
      }
      return tariffs;
    };
    paymentService.getInfo().then(function(res) {
      _this.trial = res.data.trial;
      _this.tariffs = prepareTariffs(res.data.tariffs);
      _this.systems = res.data.systems;
      return _this.payment = res.data.payment;
    }, function(res) {
      return _this.pageError = true;
    })["finally"](function(res) {
      return _this.loading = false;
    });
    _this.selectTariff = function(tariffId) {
      _this.mode += 1;
      return data.tariffId = tariffId;
    };
    _this.selectSystem = function(systemId) {
      data.systemId = systemId;
      _this.loading = true;
      return paymentService.init(data).then(function(res) {
        _this.mode += 1;
        res.data.form = $sce.trustAsHtml(res.data.form);
        return _this.stat = res.data;
      }, function(res) {
        return console.log('error');
      })["finally"](function(res) {
        return _this.loading = false;
      });
    };
    _this.back = function() {
      if (_this.mode > 0) {
        return _this.mode -= 1;
      }
    };
  }

  return PaymentIndex;

})();

angular.module('socialApp.controllers').controller('paymentIndexController', ['$sce', 'paymentService', PaymentIndex]);

})();