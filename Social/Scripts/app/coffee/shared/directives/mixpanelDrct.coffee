class mixpanelEvent extends Directive('shared')
    constructor: (
        mixpanel,
        $location)->

        return {
            restrict: 'A'
            link: (scope, element, attrs)->
                nt = attrs.name || attrs.title || ''
                if nt.trim() != ''
                    angular.element(element).on(attrs.mixpanelEvent, ()->
                        mixpanel.api('track', ['on', '"' + nt + '"', attrs.mixpanelEvent].join(' '), {
                            url: $location.path()
                            title: scope.$root.title
                        })
                    )
                else
                    console.log('mixpanel event on element: invalid name or title')
        }
