'use strict';

// выбор видео и проигрывание
// ---------------
angular
    .module('shared')
    .directive('videoPlay',
    [function () {

    var $videoTemplate = function(data) {
        var $template = {
                wrap: $('<video>', { class: 'video-js vjs-default-skin', preload: 'none', poster: data.imageUrl, id: 'video_' + data.id, controls: 'true' }),
                negative: $('<p>', { class: 'vjs-no-js' }).html('To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a>'),
            },
            apiUrl = '/api/play?id=',
            formats = [
                { type: 'video/mp4', exp: '.mp4' },
                { type: 'video/webm', exp: '.webm' },
                { type: 'video/ogg', exp: '.ogv' }
            ];
        for (var i = 0; i < formats.length; i++) {
            $template.wrap.append($('<source/>', { src: apiUrl + data.id + formats[i].exp, type: formats[i].type }));
        }
        $template.wrap.append($template.negative);
        return $template.wrap;
    }



    return {
        restrict: 'A',
        scope: {
            videoPlay: '='
        },
        link: function (scope, element, attrs) {

            // on play
            // ---------------
            angular.element(element).on('click', function () {
                var data = scope.videoPlay,
                    player = null;
                bootbox.dialog({
                    title: data.title,
                    message: $videoTemplate(data),
                    onEscape: function () {
                        if (player !== null) {
                            player.dispose();
                            player = null;
                        }
                    },
                    className: 'modal--video'
                });
                player = videojs('video_' + data.id, {}, function() {
                    this.play();
                });
            });

        }
    }
}]);