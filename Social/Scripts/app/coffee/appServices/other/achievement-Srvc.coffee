class Achievement extends Service('appSrvc')
    constructor: (
        $q
        $http
        base
        servicesDefault)->

        urlTemp = servicesDefault.baseServiceUrl + '/achievement/temp'      # work with temp achievement
        urlBase = servicesDefault.baseServiceUrl + '/achievement'           # work with achievement
        urlVoice = servicesDefault.baseServiceUrl + '/achievement/voice'    # set voice
        urlFilter = servicesDefault.baseServiceUrl + '/achievement/filter'  # get filter options

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

        # get item by id
        # ---------------
        getById = (id)->
            $q (resolve, reject)->
                if id
                    $http.get(urlBase + '/' + id).then((res)->
                        if res.data.success
                            resolve res.data
                        else
                            reject res.data
                    , (res)->
                        reject res
                    )
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show
                            text: 'Achievement item get: itemId variable error'
                            type: 'danger'

        # voice
        # ---------------
        voice = (data)->
            $q (resolve, reject)->
                if data.id && data.action
                    $http.post(urlVoice, data).then (res)->
                        if res.data.success
                            resolve res.data
                        else
                            reject res.data
                    , (res)->
                        reject res
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Achievement voice: validate error'
                            type: 'danger'
                        )

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

        # ---------- other api ----------


        return {
            saveTemp: saveTemp
            getTemp: getTemp
            cancelTemp: cancelTemp
            getById: getById
            getList: getList
            voice: voice
            getFilterProp: getFilterProp
        }