# CoffeeScript
class user extends Service('appSrvc')
    constructor: (store)->

        # save user in storage
        # ---------------
        set = (user)->
            store.set('user', user);
            return user

        # get user from storage
        # ---------------
        get = ->
            user = store.get('user')
            if user == undefined || user == null
                return {}
            else
                return user

        # ---------------
        return {
            get: get
            set: set
        }