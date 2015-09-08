class LikesInRow extends Directive('socialApp.controllers')
    constructor: ->
        return {
            restrict: 'E'
            require: 'ngModel'
            replace: true
            scope:
                likes: '=ngModel'
                id: '@'
                type: '@'
                opts: '@'
            controller: 'likesRowController'
            templateUrl: '/template/components/likes-rowTpl'
            link: (scope, element, attrs, ngModel)->
                defaults =
                    rowCount: 4
                    showLink: true
                    imageSize: 50

                scope.o = angular.extend defaults, eval('(' + scope.opts + ')')
        }