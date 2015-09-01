class MediaUpload extends Directive('socialApp.directives')
    constructor: (base)->
        return {
            restrict: 'EA'
            require: 'ngModel'
            replace: true
            scope:
                media: '=ngModel'
            controller: 'mediaUploadController'
            templateUrl: '/template/components/media-uploadTpl'
        }
