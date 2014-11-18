'use sctict';

// загрузка/смена аватара
// ---------------
angular
    .module('blog')
    .directive('avatarUpload', [function () {
        return {
            restrict: 'A',
            link: function ($scope, elem, attr) {
                $scope.avatar = $scope.avatar != undefined ? $scope.avatar : {};    // модель для изображения
                $scope.isLoading = false;   // статус загрузки
                $scope.errors = {
                    client: false,  // ошибка загрузки на клиенте
                    server: false   // ошибка загрузки на сервере
                }

                // добавить изображение
                // ---------------
                $scope.changeAvatar = function () {
                    $timeout(function () { element.find('.js-img-input').trigger('click'); });
                }

                // удалить изображение
                // ---------------
                $scope.removeAvatar = function () {
                    $scope.avatar = {};
                }

                // FileApi плагин
                // ---------------
                element.find().fileapi({
                    url: '/settings/avatar',
                    accept: 'image/*',
                    maxSize: 10 * FileAPI.MB,
                    imageSize: { minWidth: 300, minHeight: 300 },
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