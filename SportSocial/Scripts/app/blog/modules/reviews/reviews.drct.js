'use strict';

// директива для работы со списком отзывов
// ---------------
angular
    .module('blog')
    .directive('reviews',
    [   'ratingRqst',
        'utilsSrvc',
        function
        (ratingRqst,
            utilsSrvc) {
            return {
                restrict: 'A',
                link: function(scope, element, attr) {
                    var ae = angular.element,
                        $wrap = ae(element);

                    // show/hide comments
                    // ----------
                    (function() {
                        var select = {
                                showComments: '.ri__comments-show',
                                showCommentsText: '.ri__comments-show__text',
                                commentsList: '.ri__comments-list'
                            },
                            hideClass = 'hidden',
                            texts = ['Показать комментарии', 'Скрыть комментарии'];
                        $wrap.on('click', select.showComments, function() {
                            var $cList = ae(this).next(select.commentsList),
                                $text = ae(this).find(select.showCommentsText);
                            if ($cList.hasClass(hideClass)) {
                                $cList.removeClass(hideClass);
                                $text.text(texts[1]);
                            } else {
                                $cList.addClass(hideClass);
                                $text.text(texts[0]);
                            }
                        });
                    })();

                    // like/dislike review
                    // ----------
                    (function() {
                        var select = {
                                act: '.ri__act',
                                total: '.ri__rating',
                                progress: '.ri__progress',
                                bar: '.ri__bar'
                            },
                            ratingClasses = {
                                minus: 'ri__rating--minus',
                                plus: 'ri__rating--minus'
                            };

                        $wrap.on('click', select.act, function() {
                            var $this = ae(this),
                                $parent = $this.parent(),
                                $rating = $this.siblings(select.total),
                                $bar = $this.siblings(select.progress).find(select.bar),
                                //currentAction = $parent.find(select.act + '.active').length ? $parent.find(select.act + '.active').data('action') : null,
                                data = {
                                    id: $parent.data('id'),
                                    entityType: $parent.data('type'),
                                    actionType: $this.data('action')
                                };
                            ratingRqst.send(utilsSrvc.token.add(data))
                                .then(function(res) {
                                    if (res.data.success) {

                                        // calc variables
                                        // ----------
                                        var total = res.data.likesCount + res.data.dislikesCount,
                                            rating = res.data.likesCount - res.data.dislikesCount,
                                            ratingText = '',
                                            ratingClass;
                                        if (rating > 0) {
                                            ratingClass = ratingClasses.plus;
                                            ratingText = '+' + rating;
                                        } else if (rating < 0) {
                                            ratingClass = ratingClasses.minus;
                                            ratingText = '-' + rating;
                                        } else {
                                            ratingClass = '';
                                        }

                                        if (!$rating.hasClass(ratingClass)) {
                                            // remove all classes for rating value
                                            // ----------
                                            for (var pname in ratingClasses) {
                                                $rating.removeClass(ratingClasses[pname]);
                                            }
                                            // add new rating class
                                            // ----------
                                            if (ratingClass != '')
                                                $rating.addClass(ratingClass);
                                        }

                                        // change rating text
                                        // ---------
                                        $rating.text(ratingText);

                                        // change progress width
                                        // ----------
                                        $bar.css({ width: total / res.data.likesCount + '%' });

                                        // set like/dislike active class
                                        // ----------
                                        if ($this.hasClass('active')) {
                                            $this.removeClass('active');
                                        } else {
                                            $parent.find(select.act).removeClass('active');
                                            $this.addClass('active');
                                        }
                                    }
                                });
                        });

                    })();


                }
            }
        }
    ]);