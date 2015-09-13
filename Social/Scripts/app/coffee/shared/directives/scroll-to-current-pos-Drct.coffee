class scrollToCurPos extends Directive('shared')
    constructor: ->
        return {
            restrict: 'A'
            link: (scope, element, attrs)->
                attrs.$observe 'scrollToCurPos', (val)->
                    if val
                        $('html, body').animate({ scrollTop: $(element).offset().top }, 'slow')
        }