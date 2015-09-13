class ngAutofocus extends Directive('shared')
    constructor: ($timeout)->
        return {
            restrict: 'A'
            link: (scope, element, attrs)->
                attrs.$observe 'ngAutofocus', (val)->
                    if val
                        $timeout(->
                            element[0].focus()
                        )
        }