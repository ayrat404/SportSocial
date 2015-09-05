class contentHeight extends Directive('shared')
    constructor: ($window)->

        defaults =
            offsetEl: '.social__header'

        elHeight = ($el, offset)->
            wHeight = angular.element($window).height()
            $offset = offset.split(',')
            offsetHeight = 0
            if $offset.length
                angular.forEach $offset, (val, key)->
                    offsetHeight += angular.element(val).outerHeight()
            if angular.element(defaults.offsetEl).length
                offsetHeight += angular.element(defaults.offsetEl).outerHeight()
            $el.css('height', '')
            if $el.outerHeight() < wHeight
                $el.css('height', wHeight - offsetHeight)
            else
                $el.css('height', $el.outerHeight())

        return {
            restrict: 'A'
            link: (scope, element, attrs)->
                $el = angular.element element
                elHeight($el, attrs.contentHeight)
                if attrs.onresize != false
                    angular.element($window).resize ->
                        elHeight($el, attrs.contentHeight)
        }