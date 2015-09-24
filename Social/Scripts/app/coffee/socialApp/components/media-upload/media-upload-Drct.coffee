class MediaUpload extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'EA'
            require: 'ngModel'
            replace: true
            scope:
                media: '=ngModel'
            controller: 'mediaUploadController'
            templateUrl: '/template/components/media-uploadTpl'
        }
