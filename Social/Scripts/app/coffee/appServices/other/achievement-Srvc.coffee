class Achievement extends Service('appSrvc')
    constructor: (
        base
        srvcConfig
        RequestConstructor)->

        baseUrl = srvcConfig.baseServiceUrl + '/achievement'

        url =
            list: "#{baseUrl}s"
            temp: "#{baseUrl}/temp"           # work with temp achievement
            voice: "#{baseUrl}/voice"         # set voice
            filter: "#{baseUrl}/filter"       # get filter options

        rqst =

            # ---------- for achievement create ----------

            # create new temp achievement
            # ---------------
            post: new RequestConstructor.klass 'post', url.temp

            # change exists temp achievement
            # ---------------
            put: new RequestConstructor.klass 'put', url.temp

            # get temp achievement
            # ---------------
            getTemp: new RequestConstructor.klass 'get', url.temp

            # cancel temp achievement
            # ---------------
            cancelTemp: new RequestConstructor.klass 'delete', url.temp

            # ---------- for achievement create ----------


            # ---------- other ----------

            # get list
            # ---------------
            getList: new RequestConstructor.klass 'get', url.list

            # get filter prop
            # ---------------
            getFilterProp: new RequestConstructor.klass 'get', url.filter

            # get by id
            # ---------------
            getById: new RequestConstructor.klass 'get', baseUrl, (data)->
                if !data || !data.id
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Achievement item get: itemId variable error'
                            type: 'danger'
                    return false
                true

            # voice
            # ---------------
            voice: new RequestConstructor.klass 'post', url.voice, (data)->
                if !data || !data.id || !data.action
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Achievement voice: validate error'
                            type: 'danger'
                    return false
                true

            # ---------- other ----------

        facade =
            saveTemp: (data)->
                if data.id == -1
                    rqst.post.do data
                else
                    rqst.put.do data
            getById: rqst.getById.do
            getTemp: rqst.getTempRqst.do
            cancelTemp: rqst.cancelTempRqst.do
            voice: rqst.voiceRqst.do
            getFilterProp: rqst.getFilterPropRqst.do
            getList: rqst.getListRqst.do

        return facade


#        # ---------- api for achievement create ----------
#
#        # create new temp achievement
#        # ---------------
#        postRqst = new RequestConstructor.klass 'post', url.temp
#
#        # change exists temp achievement
#        # ---------------
#        putRqst = new RequestConstructor.klass 'put', url.temp
#
#        # get temp achievement
#        # ---------------
#        getTempRqst = new RequestConstructor.klass 'get', url.temp
#
#        # cancel temp achievement
#        # ---------------
#        cancelTempRqst = new RequestConstructor.klass 'delete', url.temp
#
#        # receive data for create or put achievement
#        # ---------------
#        saveTemp = (data)->
#            if data.id == -1
#                postRqst.do data
#            else
#                putRqst.do data
#
#        # ---------- api for achievement create ----------
#
#
#        # ---------- other api ----------
#
#        # get item by id todo send id in params...
#        # ---------------
#        getById = (id)->
#            $q (resolve, reject)->
#                if id
#                    $http.get("#{baseUrl}/#{id}").then (res)->
#                        if res.data.success
#                            resolve.res.data
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Achievement item get: itemId variable error'
#                            type: 'danger'
#
#        # voice
#        # ---------------
#        voiceRqst = new RequestConstructor.klass 'post', url.voice, (data)->
#                if !data || !data.id || !data.action
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Achievement voice: validate error'
#                            type: 'danger'
#                    return false
#                true
#
#        # get list
#        # ---------------
#        getListRqst = new RequestConstructor.klass 'get'
#
#        # get filter prop
#        # ---------------
#        getFilterPropRqst = new RequestConstructor.klass 'get', url.filter
#
#        # ---------- other api ----------
#
#
#        return {
#            saveTemp: saveTemp
#            getById: getById
#            getTemp: getTempRqst.do
#            cancelTemp: cancelTempRqst.do
#            voice: voiceRqst.do
#            getFilterProp: getFilterPropRqst.do
#            getList: (data)->
#                getListRqst.do params: data
#        }