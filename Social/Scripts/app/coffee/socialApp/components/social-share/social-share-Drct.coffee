class socialShare extends Directive('shared')
    constructor: ($location)->

        defaults =
            socials: ['vk', 'twitter', 'facebook', 'google+']
            counters: false

        return {
            restrict: 'E'
            replace: true
            templateUrl: '/template/components/social-shareTpl'
            link: (scope, element, attr)->

                properties = {}

                propDefaults =
                    'url': $location.absUrl()
                    'counters': ''
                    'socials': ''
                    'text': ''
                    'title': 'Fortress. Sport Social.'
                    'tags': 'fortress, sport, fitness, putin'

                for k,v of propDefaults
                    if propDefaults.hasOwnProperty k
                        attributeName = 'ss' + k.substring(0, 1).toUpperCase() + k.substring(1)
                        if properties[k] == undefined || (properties[k] != undefined && !properties[k].length)
                            properties[k] = propDefaults[k]
                        if attr[attributeName] != undefined && properties[k].length
                            properties[k] = attr[attributeName]

                scope.prop = properties
                scope.list = defaults.socials
        }