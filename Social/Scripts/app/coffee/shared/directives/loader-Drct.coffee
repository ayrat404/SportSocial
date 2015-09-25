class loader extends Directive('shared')
    constructor: ->
        containerClass = 'loader-container'
        loaderWrap = 'loader-wrap'
        delay = 0
        timer = null

        createLoader = ($el)->
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

        return {
        restrict: 'A'
        scope:
            loader: '='
            delay: '@'
        link: (scope, element, attrs)->
            delay = attrs.delay || delay
            scope.$watch('loader', (newVal, oldVal)->
                if newVal != undefined
                    $el = angular.element(element)
                    if newVal == true
                        if attrs.delay
                            timer = setTimeout ->
                                createLoader $el
                            , delay
                        else
                            createLoader $el
                    else if newVal == false
                        clearTimeout(timer)
                        $loader = $el.children('.' + loaderWrap)
                        $el.removeClass containerClass
                        if $loader.length
                            $loader.remove()
            )
        }