'use sctict';

// загрузка/смена аватара
// ---------------
angular
    .module('blog')
    .directive('avatarUpload', ['$timeout', function ($timeout) {
        return {
            restrict: 'A',
            link: function ($scope, element, attr) {
                $scope.avatar = $scope.avatar != undefined ? $scope.avatar : {};    // модель для изображения
                $scope.isLoading = false;   // статус загрузки
                $scope.errors = {
                    client: false,  // ошибка загрузки на клиенте
                    server: false   // ошибка загрузки на сервере
                }

                // добавить изображение
                // ---------------
                $scope.changeAvatar = function () {
                    //var test = element.find('.js-img-input');
                    //debugger;
                    $timeout(function () { element.find('.js-img-input').trigger('click'); });
                }

                // удалить изображение
                // ---------------
                $scope.removeAvatar = function () {
                    $scope.avatar = {};
                }

                // FileApi плагин
                // ---------------
                element.fileapi({
                    url: '/settings/avatar',
                    accept: 'image/*',
                    data: { type: 'avatar' },
                    maxSize: 10 * FileAPI.MB,
                    imageSize: { minWidth: 200, minHeight: 200 },
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
                            $scope.avatar = uiEvt.result;
                            $scope.errors.server = false;
                        } else {
                            $scope.errors.server = true;
                        }
                        $scope.$apply();
                    }
                });
            }
        };
    }]);