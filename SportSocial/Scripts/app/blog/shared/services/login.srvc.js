'use strict';

angular.module('blog').factory('loginRqst',
    ['$http',
     'serializeObj',
function ($http, serializeObj) {
     var send = function (url, obj) {
         return $http({
             method :   'POST',
             url    :   '/Login/' + url,
             data   :   serializeObj(obj),
             headers:   { 'Content-Type': 'application/x-www-form-urlencoded' }
         });
     };
     return {
         // Авторизация
         // --------------
         signIn: function (obj) {
             return send('SignIn', obj);
         },
         // Запрос смс кода подтверждения при регистрации
         // --------------
         requestCode: function (obj) {
             return send('Register', obj);
         },
         // Окончательная регистрация
         // --------------
         registration: function (obj) {
             return send('ConfirmPhone', obj);
         },
         // Запрос кода для восстановления пароля
         // ---------------
         requestRestoreCode: function(obj) {
             return send('RestorePassword', obj);
         },
         // Отправка данных для восстановления пароля
         // ---------------
         restorePassword: function(obj) {
             return send('RestorePasswordConfirm', obj);
         }
     };
 }]);
