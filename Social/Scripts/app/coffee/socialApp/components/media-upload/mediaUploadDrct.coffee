class MediaUpload extends Directive('socialApp.directives')
    constructor: (base)->
        return {
            restrict: 'EA'
            require: 'ngModel'
            replace: true
            scope:
                media: '=ngModel'
            controller: 'mediaUploadController'
            templateUrl: '/Scripts/templates/components/media-uploadTpl.html'
        }
