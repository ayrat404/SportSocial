# CoffeeScript
class user extends Service('appSrvc')
    constructor: (
        $q
        $http
        store
        srvcConfig)->

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


        urlBase = srvcConfig.baseServiceUrl + '/users'
        urlFilter = srvcConfig.baseServiceUrl + '/users/filter'

        # get list
        # ---------------
        getList = (data)->
            $q (resolve, reject)->
                $http.get(urlBase, { params: data }).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject res

        # get filter prop
        # ---------------
        getFilterProp = ->
            $q (resolve, reject)->
                $http.get(urlFilter).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject res

        # ---------------
        return {
            get: get
            set: set
            getFilterProp: getFilterProp
            getList: getList
        }