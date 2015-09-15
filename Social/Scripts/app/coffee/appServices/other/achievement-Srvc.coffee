class Achievement extends Service('appSrvc')
    constructor: (
        $q
        $http
        base
        servicesDefault)->

        urlTemp = servicesDefault.baseServiceUrl + '/achievement/temp'
        urlCards = servicesDefault.baseServiceUrl + '/achievement/cards'

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

        # get achievements cards
        # ---------------
        getCards = ->
            $q (resolve, reject)->
                $http.get(urlCards).then (res)->
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
            getCards: getCards
        }









#        # submit new comment
#        # ---------------
#        submit = (data)->
#            $q (resolve, reject)->
#                # todo validate
#                if data
#                    $http.post(url, data).then((res)->
#                        if res.data.success
#                            resolve(res.data)
#                        else
#                            reject(res.data)
#                            base.notice.response(res) if servicesDefault.noticeShow.errors
#                    , (res)->
#                        reject(res)
#                    )
#                else
#                    reject()
#                    if servicesDefault.noticeShow.errors
#                        base.notice.show(
#                            text: 'Comment submit validate error'
#                            type: 'danger'
#                        )
#
#
#        # remove journal item
#        # ---------------
#        remove = (itemId)->
#            $q (resolve, reject)->
#                if itemId && typeof itemId == 'number'
#                    $http.delete(url, { params: { id: itemId } }).then((res)->
#                        if res.data.success
#                            resolve(res.data)
#                        else
#                            reject(res.data)
#                            base.notice.response(res) if servicesDefault.noticeShow.errors
#                    , (res)->
#                        reject(res)
#                    )
#                else
#                    reject()
#                    if servicesDefault.noticeShow.errors
#                        base.notice.show(
#                            text: 'Comment delete: itemId variable error'
#                            type: 'danger'
#                        )
#


        return {
#            submit: submit
#            remove: remove
        }