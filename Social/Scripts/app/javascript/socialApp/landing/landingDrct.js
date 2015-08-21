(function(){
var landingScripts;

landingScripts = (function() {
  function landingScripts(base) {
    return {
      restrict: 'A',
      link: function(scope, element, attrs) {
        var $landing, $window, wHeight;
        $window = $(window);
        wHeight = $window.height();
        $landing = angular.element(element);
        (function() {
          $window.resize(function() {
            wHeight = $window.height();
            $landing.css('height', '');
            if ($landing.outerHeight() < wHeight) {
              return $landing.css('height', wHeight);
            } else {
              return $landing.css('height', $landing.outerHeight());
            }
          });
          return $window.trigger('resize');
        })();
        (function() {
          var $sliderControlsContainer, $sliderImgWrap, changeContentBySlide, dynContent, sliderApi, sliderCtrlTpl;
          $sliderImgWrap = $landing.find('.slider-mac-body');
          $sliderControlsContainer = $landing.find('.js-slider-left-wrap');
          sliderCtrlTpl = {
            controls: {
              $wrap: $('<div>', {
                "class": 'slide__controls'
              }),
              $bottom: $('<div>', {
                "class": 'sc__it sc__it--bottom'
              }),
              $top: $('<div>', {
                "class": 'sc__it sc__it--top'
              }),
              init: function() {
                return this.$wrap.append(this.$bottom).append(this.$top);
              }
            },
            nav: {
              $wrap: $('<div>', {
                "class": 'slide__nav'
              }),
              $item: $('<div>', {
                "class": 'slide__nav__it'
              }),
              init: function(count) {
                var i, j, ref;
                for (i = j = 0, ref = count; 0 <= ref ? j <= ref : j >= ref; i = 0 <= ref ? ++j : --j) {
                  this.$wrap.append(this.$item.clone());
                }
                return this.$wrap;
              }
            }
          };
          dynContent = {
            $leftWrap: $landing.find('.slide__left-it'),
            $rightTitle: $landing.find('.slide__short-desc')
          };
          $sliderImgWrap.fotorama({
            nav: false,
            loop: true,
            autoplay: 6000,
            transition: 'dissolve'
          });
          sliderApi = $sliderImgWrap.data('fotorama');
          $sliderControlsContainer.append(sliderCtrlTpl.controls.init());
          $sliderControlsContainer.append(sliderCtrlTpl.nav.init(4));
          changeContentBySlide(sliderApi.activeFrame.i - 1);
          $sliderImgWrap.on('fotorama:readry fotorama:show', function(e, fotorama, extra) {
            return changeContentBySlide(fotorama.activeFrame.i - 1);
          });
          sliderCtrlTpl.controls.$bottom.on('click', function() {
            return sliderApi.show('>');
          });
          sliderCtrlTpl.controls.$top.on('click', function() {
            return sliderApi.show('<');
          });
          sliderCtrlTpl.nav.$wrap.on('click', '.slide__nav__it', function() {
            return sliderApi.show(sliderCtrlTpl.nav.$wrap.find('.slide__nav__it').index(this));
          });
          changeContentBySlide = function(slideIndex) {
            var $aLeft, $aRight;
            $aLeft = dynContent.$leftWrap.eq(slideIndex);
            $aRight = dynContent.$rightTitle.eq(slideIndex);
            dynContent.$leftWrap.addClass('hidden');
            $aLeft.removeClass('hidden');
            base.animation.add($aLeft, 'fadeIn');
            dynContent.$rightTitle.addClass('hidden');
            $aRight.removeClass('hidden');
            base.animation.add($aRight, 'fadeInLeft');
            sliderCtrlTpl.nav.$wrap.find('.slide__nav__it').removeClass('active');
            return sliderCtrlTpl.nav.$wrap.find('.slide__nav__it').eq(slideIndex).addClass('active');
          };
        })();
      }
    };
  }

  return landingScripts;

})();

angular.module('socialApp.directives').directive('landingScripts', ['base', landingScripts]);

})();