class LikesInFinger extends Directive('socialApp.directives')
    constructor: ->

        return {
            restrict: 'E'
            require: 'ngModel'
            replace: true
            scope:
                likes: '=ngModel'
                id: '@'
                entityType: '@'
            controller: 'likesInFingerController'
            templateUrl: '/template/components/likes/likes-fingerTpl'
        }