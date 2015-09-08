class WallVertical extends Directive('socialApp.directives')
    constructor: ->

        return {
            restrict: 'A'
            controller: ->

            controllerAs: 'wall'
            link: (scope, element, attrs)->
                console.log('wall directive')
        }
