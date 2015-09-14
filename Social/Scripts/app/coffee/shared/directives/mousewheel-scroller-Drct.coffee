class mousewheelScroller extends Directive('shared')
    constructor: ($timeout)->
        return {
            restrict: 'A'
            link: (scope, element, attrs)->

                $el = angular.element(element)

                listener = (e)->
                    scrollTop = $el.scrollTop()
                    if e.originalEvent.type == 'touchmove'
                        console.log('touch')
                    else
                        if e.originalEvent.wheelDeltaY.toString().slice(0,1) == '-'
                            $el.scrollTop scrollTop + 100
                        else
                            $el.scrollTop scrollTop - 100
                    if $el.scrollTop() != 0
                        e.preventDefault()

                # ---------------
                $el.on 'mousewheel', listener

                # ---------------
                scope.$on '$destroy', ->
                    element.off 'mousewheel', listener
        }