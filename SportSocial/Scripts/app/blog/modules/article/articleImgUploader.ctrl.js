'use strict';

angular.module('blog').directive('articleImgUploader', ['$timeout', function ($timeout) {
    return {
        restrict: 'AE',
        templateUrl: '/Scripts/templates/blog/article/img-uploader.html',
        scope: {
            images: '='
        },
        link: function ($scope, element, attrs) {
            $scope.images = $scope.images != undefined ? $scope.images : [];    // модель для изображений
            $scope.isLoading = false;   // статус загрузки
            $scope.errors = {
                client: false,  // ошибка загрузки на клиенте
                server: false   // ошибка загрузки на сервере
            }

            // добавить изображение
            // ---------------
            $scope.addImage = function () {
                $timeout(function () { element.find('.js-img-input').trigger('click'); });
            }

            // удалить изображение
            // ---------------
            $scope.removeImage = function (id) {
                $scope.images.forEach(function(val, i, arr) {
                    if (val.id === id) {
                        arr.splice(i, 1);
                    }
                });
            }

            // FileApi плагин
            // ---------------
            element.fileapi({
                url: '/file/images',
                accept: 'image/*',
                data: { type: 'article' },
                multiple: false,
                maxFiles: 1,
                maxSize: 10 * FileAPI.MB,
                imageSize: { minWidth: 400, minHeight: 300 },
                autoUpload: true,
                onSelect: function (evt, uiEvt) {
                    if (uiEvt.other[0] != undefined && uiEvt.other[0].errors != undefined) {
                        $scope.errors.client = true;
                    } else {
                        $scope.errors.client = false;
                    }
                    $scope.$apply();
                },
                onBeforeUpload: function (evt, uiEvt) {
                    $scope.isLoading = true;
                },
                onFileComplete: function (evt, uiEvt) {
                    $scope.isLoading = false;
                    if (uiEvt.result.success) {
                        $scope.images.push(uiEvt.result);
                        $scope.errors.server = false;
                    } else {
                        $scope.errors.server = true;
                    }
                    $scope.$apply();
                }
            });
        }
    }
}]);