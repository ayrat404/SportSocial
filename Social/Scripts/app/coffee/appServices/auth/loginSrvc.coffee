# CoffeeScript
class login extends Service('appSrvc')
    constructor: ($state, $location, $q, $rootScope, base, mixpanel) ->
        defaults =
            showNotices: true
            
        url = '/test/auth'
        
        # ---------------
        logIn = (data)->
        
        # ---------------
        logOut = ()->
        
        # ---------------
        return {
            logIn: logIn,
            logOut: logOut
        }