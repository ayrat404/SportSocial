class LikesInRow extends Directive('socialApp.directives')
    constructor: (
        $rootScope
        $timeout
        likeService)->
        return {
            restrict: 'E'
            require: 'ngModel'
            replace: true
            scope:
                likes: '=ngModel'
                id: '@'
                entityType: '@'
                opts: '@'
            controller: 'likesInRowController'

            templateUrl: '/template/components/likes/likes-rowTpl'
            link: (scope, element, attrs, ngModel)->
                defaults =
                    rowCount: 4
                    showLink: true
                    imageSize: 50

                scope.o = defaults
        }