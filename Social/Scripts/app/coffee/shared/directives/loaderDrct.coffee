class loader extends Directive('shared')
    constructor: ->
        spinnerClass = 'loader timer-loader'
        spinnerText = 'Загрузка...'
        containerClass = 'loader-container'
        wrapClass = 'loader-wrap'

        return {
            restrict: 'A'
            scope:
                loader: '='
            link: (scope, element, attrs)->
                scope.$watch('loader', (newVal, oldVal)->
                    if newVal != oldVal
                        $el = angular.element(element)
                        if newVal == true
                            $el.addClass containerClass
                            $el.append(
                                angular.element('<div>', { class: wrapClass }).append(
                                    angular.element('<div>', { class: spinnerClass, text: spinnerText })
                                )
                            )
                        else if newVal == false
                            $el.removeClass containerClass
                            $el.find('.' + wrapClass).remove()
                )
        }