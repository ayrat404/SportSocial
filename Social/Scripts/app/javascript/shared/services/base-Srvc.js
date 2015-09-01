var animationCtrl, delay, noticeCtrl, validationCtrl;

noticeCtrl = (function() {
  var $body, bottomOffset, boxClass, checkColumnHeight, defaults, hide, hideAll, margin, mergeOpts, notices, response, setBottomOffset, show;
  $body = angular.element('body');
  defaults = {
    type: 'info',
    delay: 6000,
    autohide: true
  };
  boxClass = 'float-notice alert alert-';
  bottomOffset = 20;
  margin = 10;
  notices = [];
  angular.element(window).on('resize', function() {
    var i, k, ref;
    if (notices.length) {
      for (i = k = 0, ref = notices.length; 0 <= ref ? k < ref : k > ref; i = 0 <= ref ? ++k : --k) {
        setBottomOffset(i);
      }
      return checkColumnHeight();
    }
  });
  show = function(opts) {
    var $tmpl, options;
    options = mergeOpts(opts);
    $tmpl = angular.element('<div>', {
      "class": boxClass + options.type
    }).html(options.text);
    $tmpl.id = new Date().getTime();
    $body.append($tmpl);
    notices.push($tmpl);
    setBottomOffset(notices.length - 1);
    checkColumnHeight();
    if (options.autohide) {
      setTimeout(function() {
        return hide($tmpl.id);
      }, options.delay);
    }
    return {
      $el: $tmpl,
      id: $tmpl.id
    };
  };
  hide = function(it) {
    var i, id, j, k, l, ref, ref1, results;
    if (it !== null && it !== void 0) {
      if (typeof it === 'object' && it.id !== void 0) {
        id = it.id;
      } else if (typeof it === 'number') {
        id = it;
      }
      results = [];
      for (i = k = 0, ref = notices.length; 0 <= ref ? k < ref : k > ref; i = 0 <= ref ? ++k : --k) {
        if (notices[i] !== void 0 && notices[i].id === id) {
          notices[i].remove();
          notices.splice(i, 1);
          for (j = l = 0, ref1 = notices.length; 0 <= ref1 ? l < ref1 : l > ref1; j = 0 <= ref1 ? ++l : --l) {
            setBottomOffset(j);
          }
          break;
        } else {
          results.push(void 0);
        }
      }
      return results;
    }
  };
  hideAll = function() {
    var i, k, ref;
    for (i = k = 0, ref = notices.length; 0 <= ref ? k < ref : k > ref; i = 0 <= ref ? ++k : --k) {
      notices[i].remove();
    }
    return notices = [];
  };
  response = function(data) {
    var noticeClass;
    noticeClass = data.success === true ? 'success' : 'warning';
    if (data.message && data.message.length) {
      show({
        text: data.message,
        type: noticeClass
      });
    }
    if (data.statusText) {
      return show({
        text: data.status + ': ' + data.statusText,
        type: noticeClass
      });
    }
  };
  setBottomOffset = function(i) {
    var j, k, offset, ref;
    offset = bottomOffset;
    if (notices[i] !== void 0) {
      if (notices[i - 1] !== void 0) {
        for (j = k = 0, ref = notices.indexOf(notices[i - 1]); 0 <= ref ? k <= ref : k >= ref; j = 0 <= ref ? ++k : --k) {
          offset += notices[j].outerHeight() + margin;
        }
      }
      return notices[i].css('bottom', offset);
    }
  };
  checkColumnHeight = function() {
    var $lastEl, bottom, wH;
    $lastEl = notices[notices.length - 1];
    wH = window.innerHeight;
    bottom = parseInt($lastEl[0].style.bottom);
    if (wH < $lastEl.outerHeight() + bottom) {
      hide(notices[0]);
      if (wH < $lastEl.outerHeight() + bottom) {
        return checkColumnHeight();
      }
    }
  };
  mergeOpts = function(opts) {
    return angular.extend({}, defaults, opts);
  };
  return {
    hide: hide,
    hideAll: hideAll,
    show: show,
    response: response
  };
})();

animationCtrl = (function() {
  return {
    add: function($el, x, callback) {
      $el.addClass(x + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function() {});
      $(this).removeClass(x + ' animated');
      if (callback !== void 0 && typeof callback === 'function') {
        return callback();
      }
    }
  };
})();

validationCtrl = (function() {
  return {
    email: function(email) {
      var re;
      re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
      return re.test(email);
    }
  };
})();

delay = function(callback, ms) {
  var timer;
  timer = 0;
  clearTimeout(timer);
  return timer = setTimeout(callback, ms);
};

angular.module('shared').factory('base', [
  function() {
    return {
      notice: noticeCtrl,
      animation: animationCtrl,
      validate: validationCtrl,
      isArray: function(array) {
        if (Object.prototype.toString.call(array) === '[object Array]') {
          return true;
        }
        return false;
      },
      delayConstructor: delay,
      GUID: function() {
        var s4;
        s4 = function() {
          return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
        };
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
      }
    };
  }
]);
