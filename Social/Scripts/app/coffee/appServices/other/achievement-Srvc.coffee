class Achievement extends Service('appSrvc')
    constructor: (
        $q
        $http
        base
        srvcConfig)->

        url =
            base: srvcConfig.baseServiceUrl + '/achievement'    # base achievement url
            temp: '/temp'           # work with temp achievement
            voice: '/voice'         # set voice
            filter: '/filter'       # get filter options

        class Request
            constructor: (@type, path, @validate)->
                @url = url.base + path
            isValid: ->
                if @validate && typeof @validate.func == "function"
                    @validate.func()
                else
                    true
            do: (data)=>
                _this = this
                $q (resolve, reject)->
                    if _this.isValid data
                        $http[_this.type](_this.url, data).then (res)->
                            if res.data.success
                                resolve res.data
                            else
                                reject res.data
                        , (res)->
                            reject res
                    else
                        reject()
                        _this.validate.onFail?()


        # ---------- api for achievement create ----------

        # create new temp achievement
        # ---------------
        postRqst = new Request 'post', url.temp

        # change exists temp achievement
        # ---------------
        putRqst = new Request 'put', url.temp

        # get temp achievement
        # ---------------
        getTempRqst = new Request 'get', url.temp

        # cancel temp achievement
        # ---------------
        cancelTempRqst = new Request 'delete', url.temp

        # receive data for create or put achievement
        # ---------------
        saveTemp = (data)->
            if data.id == -1
                postRqst.do data
            else
                putRqst.do data

        # ---------- api for achievement create ----------


        # ---------- other api ----------

        # get item by id todo send id in params...
        # ---------------
        getById = (id)->
            $q (resolve, reject)->
                if id
                    $http.get("#{url.base}/#{id}").then (res)->
                        if res.data.success
                            resolve.res.data
                        else
                            reject res.data
                    , (res)->
                        reject res
                else
                    reject()
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Achievement item get: itemId variable error'
                            type: 'danger'

        # voice
        # ---------------
        voiceRqst = new Request 'post', url.voice, {
            func: (data)->
                if data.id && data.action then true else false
            onFail: ->
                if srvcConfig.noticeShow.errors
                    base.notice.show
                        text: 'Achievement voice: validate error'
                        type: 'danger'
        }

        # get list
        # ---------------
        getListRqst = new Request 'get'

        # get filter prop
        # ---------------
        getFilterPropRqst = new Request 'get', url.filter

        # ---------- other api ----------


        return {
            saveTemp: saveTemp
            getById: getById
            getTemp: getTempRqst.do
            cancelTemp: cancelTempRqst.do
            voice: voiceRqst.do
            getFilterProp: getFilterPropRqst.do
            getList: (data)->
                getListRqst.do params: data
        }