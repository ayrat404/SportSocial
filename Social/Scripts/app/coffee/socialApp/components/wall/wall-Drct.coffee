class WallVertical extends Directive('socialApp.directives')
    constructor: (
        modalService)->

        return {
            restrict: 'A'
            controller: ->
                _this = this
                _this.remove = (id)->
                    modalService.show
                        name: 'journalRemove'
                        data:
                            id: id

            controllerAs: 'wall'
            link: (scope, element, attrs)->
                console.log('wall directive')
        }
