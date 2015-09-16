class Achievement extends Service('appSrvc')
    constructor: (
        $q
        $http
        base
        servicesDefault)->

        urlTemp = servicesDefault.baseServiceUrl + '/achievement/temp'

        # ---------- api for achievement create ----------

        # create new temp achievement
        # ---------------
        post = (data)->
            $q (resolve, reject)->
                $http.post(urlTemp, data).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject null

        # change exists temp achievement
        # ---------------
        put = (data)->
            $q (resolve, reject)->
                $http.put(urlTemp, data).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject null

        # receive data for create or put achievement
        # ---------------
        saveTemp = (data)->
            if data.id == -1
                post data
            else
                put data

        # get temp achievement
        # ---------------
        getTemp = ->
            $q (resolve, reject)->
                $http.get(urlTemp).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject null

        # cancel temp achievement
        # ---------------
        cancelTemp = ->
            $q (resolve, reject)->
                $http.delete(urlTemp).then (res)->
                    if res.data.success
                        resolve res.data
                    else
                        reject res.data
                , (res)->
                    reject res

        # ---------- api for achievement create ----------


        # ---------- other api ----------


        # ---------- other api ----------


        return {
            saveTemp: saveTemp
            getTemp: getTemp
            cancelTemp: cancelTemp
        }