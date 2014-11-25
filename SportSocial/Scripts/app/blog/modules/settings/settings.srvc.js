'use strict';

angular.module('blog').factory('settingsRqst', ['$http', 'serializeObj', function ($http) {
    var send = function (action, obj) {
        return $http({
            method: 'POST',
            url: '/Settings/' + action,
            data: obj
        });
    };
    return {
        // Сменить пароль
        // ---------------
        changePassword: function(obj) {
            return send('ChangePassword', obj);
        },
        // Запросить код для смены телефона
        // ---------------
        requestCode: function(obj) {
            return send('RequestCode', obj);
        },
        // Подтвердить номер телефона отправкой кода
        // ---------------
        confirmCode: function(obj) {
            return send('ConfirmCode', obj);
        },
        // Удалить аватар пользователя
        // ---------------
        removeAvatar: function() {
            return send('RemoveAvatar');
        }
    };
}]);
