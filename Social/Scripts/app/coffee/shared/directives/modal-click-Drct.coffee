class modalClick extends Directive('shared')
    constructor: (modalService)->
        return {
            restrict: 'A'
            link: (scope, element, attrs)->

                modalItem = attrs.modalClick

                # listener
                # ---------------
                listener = ->
                    modalService.show name: modalItem

                # ---------------
                element.on 'click', listener

                # ---------------
                scope.$on '$destroy', ->
                    element.off 'click', listener
        }