var Tape;

Tape = (function() {
  function Tape($q, $scope, $state, tapeService) {
    var _this, getList, k, ref, setUrl, v;
    $scope.$root.title = 'Fortress | Моя лента';
    _this = this;
    _this.loader = false;
    _this.pageError = false;
    _this.showMoreLoading = false;
    _this.filter = {
      count: 20,
      page: 3
    };
    ref = $state.params;
    for (k in ref) {
      v = ref[k];
      if (v !== void 0) {
        _this.filter[k] = v;
      }
    }
    setUrl = function() {
      $state.params = _this.filter;
      return $state.transitionTo($state.current, $state.params, {
        notify: false
      });
    };
    getList = function(filter) {
      setUrl();
      return $q(function(resolve, reject) {
        return tapeService.getList(filter).then(function(res) {
          _this.showMore = res.data.isMore;
          return resolve(res.data.list);
        }, function(res) {
          _this.list = [];
          _this.showMore = false;
          return reject(res);
        });
      });
    };
    (function() {
      var filter;
      filter = angular.extend({}, _this.filter);
      filter.page = 1;
      filter.count = _this.filter.page * _this.filter.count;
      return getList(filter).then(function(list) {
        return _this.list = list;
      }, function(res) {
        return _this.pageError = true;
      })["finally"](function() {
        return _this.loader = false;
      });
    })();
    _this.loadMore = function() {
      if (!_this.showMoreLoading) {
        _this.showMoreLoading = true;
        _this.filter.page = +_this.filter.page + 1;
        return getList(_this.filter).then(function(list) {
          return _this.list.push(list);
        })["finally"](function() {
          return _this.showMoreLoading = false;
        });
      }
    };
    _this.share = function(obj) {
      var url;
      url = '';
      if (obj.type === 'record') {
        url = 'main.journalIt';
      } else if (obj.type === 'achievement') {
        url = 'main.achievementView';
      }
      return modalService.show({
        name: 'socialShare',
        data: {
          url: $state.href(url, {
            id: obj.id
          }, {
            absolute: true
          }),
          text: obj.text,
          media: obj.media,
          hashtags: obj.tags
        }
      });
    };
  }

  return Tape;

})();

angular.module('socialApp.controllers').controller('tapeController', ['$q', '$scope', '$state', 'tapeService', Tape]);
