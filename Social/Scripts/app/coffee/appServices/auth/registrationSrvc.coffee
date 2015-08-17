# CoffeeScript
class registration extends Service('appSrvc')
    constructor: ($state, $location, $q, $rootScope, base, mixpanel) ->
        defaults =
            showNotices: true
            
        url = '/test/registration'
        
        # ---------------
        register = (data)->
        
        # ---------------
        return {
            register: register
        }