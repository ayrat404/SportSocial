class modalClick extends Directive('shared')
    constructor: (modalService)->
        return {
            restrict: 'A'
            link: (scope, element, attrs)->
                modalItem = attrs.modalClick
                element.on('click', ->
                    modalService.show(name: modalItem)
                )
                return
        }