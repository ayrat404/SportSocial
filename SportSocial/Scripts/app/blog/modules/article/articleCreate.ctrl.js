'use strict';

angular.module('app').controller('ArticleCreateCtrl',
    ['$scope',
     'articleRqst',
     'tokenRqst',
     'FileUploader',
function ($scope, articleRqst, tokenRqst, FileUploader) {

    // настройки загрузчика
    // ---------------
    //var uploader = $scope.uploader = new FileUploader({
    //    url         :   '/files/images',
    //    autoUpload  :   true,
    //    queueLimit  :   1
    //});

    // успешная загрузка изображения на сервер
    // ---------------
    //uploader.onSuccessItem = function (fileItem, response, status, headers) {
    //    console.info('onSuccessItem', fileItem, response, status, headers);
    //};

    // фильтр по типу файла
    // ---------------
    //uploader.filters.push({
    //    name: 'imageFilter',
    //    fn: function (item, options) {
    //        var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
    //        return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
    //    }
    //});

    //
    $scope.test = function (event, $flow, flowFile) {
        debugger;
    }

    // создание статьи
    // ---------------
    $scope.createArticle = function() {
        
    }

}]);
