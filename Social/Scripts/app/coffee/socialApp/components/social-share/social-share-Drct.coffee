class socialShare extends Directive('shared')
    constructor: ($location)->

        defaults =
            socials: ['vk', 'twitter', 'facebook', 'google+']
            counters: false

        return {
            restrict: 'E'
            replace: true
            scope:
                prop: '='
            templateUrl: '/template/components/social-shareTpl'
            link: (scope, element, attr)->

                # default prop
                # ---------------
                propDefaults =
                    url: $location.absUrl()
                    title: 'Fortress. Sport social network.'
                    text: 'Test text test text test text'
                    media: '~/Content/socialApp/images/common/logo-big.png'
                    hashtags: 'fortress, sport, fitness'

                # new prop
                # ---------------
                properties = {}

                # get image from media array
                # ---------------
                if scope.prop.media && scope.prop.media.length && typeof scope.prop.media != 'string'
                    scope.prop.media = scope.prop.media[0].url

                # set string from hashtags array
                # ---------------
                if scope.prop.hashtags && scope.prop.hashtags.length
                    scope.prop.hashtags = scope.prop.hashtags.join(', ')

                # check prop
                # ---------------
                for k,v of propDefaults
                    if propDefaults.hasOwnProperty k
                        if scope.prop[k]
                            properties[k] = scope.prop[k]
                        else
                            properties[k] = propDefaults[k]

                scope.prop = properties
                scope.providerList = defaults.socials
        }