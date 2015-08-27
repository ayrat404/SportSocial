'use strict';

angular.module('socialApp.directives')
    .directive('landingScripts', [
        'base',
    function (base) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {

                var $window = $(window),
                    wHeight = $window.height(),
                    $landing = angular.element(element);

                // content height
                // ---------------
                (function () {
                    $window.resize(function () {
                        wHeight = $window.height();
                        $landing.css('height', '');
                        if ($landing.outerHeight() < wHeight) {
                            $landing.css('height', wHeight);
                        } else {
                            $landing.css('height', $landing.outerHeight());
                        }
                    });
                    $window.trigger('resize');
                })();

                // slider
                // ---------------
                (function () {
                    var $sliderImgWrap = $landing.find('.slider-mac-body'),
                        $sliderControlsContainer = $landing.find('.js-slider-left-wrap'),
                        sliderCtrlTpl = {
                            controls: {
                                $wrap: $('<div>', { class: 'slide__controls' }),
                                $bottom: $('<div>', { class: 'sc__it sc__it--bottom' }),
                                $top: $('<div>', { class: 'sc__it sc__it--top' }),
                                init: function() {
                                    return this.$wrap.append(this.$bottom).append(this.$top);
                                }
                            },
                            nav: {
                                $wrap: $('<div>', { class: 'slide__nav' }),
                                $item: $('<div>', { class: 'slide__nav__it' }),
                                init: function(count) {
                                    for (var i = 0; i < count; i++) {
                                        this.$wrap.append(this.$item.clone());
                                    }
                                    return this.$wrap;
                                }
                            }
                        },
                        sliderApi,
                        dynContent = {
                            $leftWrap: $landing.find('.slide__left-it'),
                            $rightTitle: $landing.find('.slide__short-desc')
                        };

                    // slider plugin init
                    // ----------
                    $sliderImgWrap.fotorama({
                        nav: false,
                        loop: true,
                        autoplay: 6000,
                        transition: 'dissolve'
                    });

                    // get api
                    // ----------
                    sliderApi = $sliderImgWrap.data('fotorama');

                    // paste slider controls
                    // ----------
                    $sliderControlsContainer.append(sliderCtrlTpl.controls.init());
                    $sliderControlsContainer.append(sliderCtrlTpl.nav.init(4));

                    // init actives
                    // ----------
                    changeContentBySlide(sliderApi.activeFrame.i - 1);

                    // on slide change
                    // ----------
                    $sliderImgWrap.on('fotorama:readry fotorama:show', function(e, fotorama, extra) {
                        changeContentBySlide(fotorama.activeFrame.i - 1);
                    });

                    // handle controls
                    // ----------
                    sliderCtrlTpl.controls.$bottom.on('click', function () {
                        sliderApi.show('>');
                    });
                    sliderCtrlTpl.controls.$top.on('click', function () {
                        sliderApi.show('<');
                    });
                    sliderCtrlTpl.nav.$wrap.on('click', '.slide__nav__it', function () {
                        sliderApi.show(sliderCtrlTpl.nav.$wrap.find('.slide__nav__it').index(this));
                    });

                    // change content by slide index
                    // ----------
                    function changeContentBySlide(slideIndex) {
                        var $aLeft = dynContent.$leftWrap.eq(slideIndex),
                            $aRight = dynContent.$rightTitle.eq(slideIndex);

                        // change left
                        // -----
                        dynContent.$leftWrap.addClass('hidden');
                        $aLeft.removeClass('hidden');
                        base.animation.add($aLeft, 'fadeIn');

                        // change right
                        // -----
                        dynContent.$rightTitle.addClass('hidden');
                        $aRight.removeClass('hidden');
                        base.animation.add($aRight, 'fadeInLeft');

                        // change nav dots
                        // -----
                        sliderCtrlTpl.nav.$wrap.find('.slide__nav__it').removeClass('active');
                        sliderCtrlTpl.nav.$wrap.find('.slide__nav__it').eq(slideIndex).addClass('active');
                    }
                })();
                
            }
        }
    }
    ]);