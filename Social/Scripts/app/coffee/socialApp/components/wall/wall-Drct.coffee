class WallVertical extends Directive('socialApp.directives')
    constructor: ->

        return {
            restrict: 'A'
            controller: ->
                this.toggleEdit = (it, isOwner)->
                    if isOwner
                        if it.editing != true
                            it.editing = true
                        else
                            it.editing = false
                    debugger
                    #$scope.apply()
            controllerAs: 'wall'
            link: (scope, element, attrs)->
                console.log('wall directive')
        }
