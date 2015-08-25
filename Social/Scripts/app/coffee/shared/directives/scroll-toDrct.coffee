class scrollTo extends Directive('shared')
    constructor: ->
        navSelector = '.js-nav'
        defaults =
            speed: 600
            offset: 0

        # scroll to func
        # ---------------
        scrollTo = (to, offset)->
            if $(to).length
                offset = angular.extend(defaults.offset, offset)
                offset += $(navSelector).outerHeight() if $(navSelector).length
                $('html, body').animate({
                    scrollTop: $(to).offset().top - offset
                }, defaults.speed)

        # ---------------
        return {
            restrict: 'A'
            link: (scope, element, attrs)->
                $el = $(element)
                $el.on('click', (e)->
                    e.preventDefault()
                    scrollTo(attrs.scrollTo, attrs.offset)
                )
        }