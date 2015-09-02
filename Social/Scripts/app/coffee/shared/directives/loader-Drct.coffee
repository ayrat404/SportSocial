class loader extends Directive('shared')
    constructor: ->
        containerClass = 'loader-container'
        loaderWrap = 'loader-wrap'
        delay = 300
        timer = null

        return {
        restrict: 'A'
        scope:
            loader: '='
        link: (scope, element, attrs)->
            scope.$watch('loader', (newVal, oldVal)->
                if newVal != oldVal
                    $el = angular.element(element)
                    if newVal == true
                        timer = setTimeout(->
                            $loader = angular.element('<div>', class: loaderWrap).append(
                              angular.element('<div>', class: 'loader').append(
                                angular.element '<div>', class: 'inner one'
                                angular.element '<div>', class: 'inner two'
                                angular.element '<div>', class: 'inner three'
                              )
                            )
                            $el.addClass containerClass
                            $el.append($loader)
                            $loader.show('slow')
                        , delay)
                    else if newVal == false
                        clearTimeout(timer)
                        $loader = $el.children('.' + loaderWrap)
                        $el.removeClass containerClass
                        if $loader.length
                            $loader.remove()
            )
        }