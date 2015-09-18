class mediaShowModal extends Directive('shared')
    constructor: (modalService)->
        return {
            restrict: 'A'
            scope:
                media: '@id'
                index: '@'
                entityType: '@'
            link: (scope, element, attrs)->

                # listener
                # ---------------
                listener = ->
                    modalService.show
                        name: 'mediaShow'
                        data:
                            media: scope.media
                            index: +scope.index + 1
                            entityType: scope.entityType

                # ---------------
                element.on 'click', listener

                # ---------------
                scope.$on '$destroy', ->
                    element.off 'click', listener


        }