angular.module('shared').factory 'mixpanel', ['$location', ($location)->
    api = (action, event, prop)->
        if window.mixpanel == null || window.mixpanel == undefined
            console.log('mixpanel: mixpanel isn\'t defined');
            return
        if typeof mixpanel[action] != 'function'
            console.log('mixpanel: method "mixpanel.' + action + '" is not defined')
            return
        switch action
            when 'identify'
                mixpanel.identify(event, prop)
            when 'people.set'
                mixpanel.people.set(event, prop)
            else mixpanel[action](event, prop)
        #console.log(prop)

    # ---------------
    return {
        api: api
        ev:
            visitPage: (title)->
                api('track', 'visit page',
                    title: title
                    url: $location.path()
                )
    }
]