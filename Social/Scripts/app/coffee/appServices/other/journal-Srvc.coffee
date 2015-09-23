class Journal extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        srvcConfig
        RequestConstructor)->

        url = srvcConfig.baseServiceUrl + '/journal'
        urlRecords = srvcConfig.baseServiceUrl + '/records'
        valid =
            minText: 50

        # validate journal model
        # ---------------
        validate = (data)->
            if data &&
              data.text &&
              data.text.length >= valid.minText
                return true
            return false

        rqst =

            # submit new journal item
            # ---------------
            post: new RequestConstructor.klass 'post', url, (data)->
                if !validate(data)
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Minimum text length 50 symbols'
                            type: 'danger'
                    return false
                true

            # save exists journal item
            # ---------------
            put: new RequestConstructor.klass 'put', url, (data)->
                if !validate(data)
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Minimum text length 50 symbols'
                            type: 'danger'
                    return false
                true

            # remove journal item
            # ---------------
            remove: new RequestConstructor.klass 'delete', url, (data)->
                if !data || !data.id
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Journal delete: itemId variable error'
                            type: 'danger'
                    return false
                true

            # get records list
            # ---------------
            getList: new RequestConstructor.klass 'get', urlRecords, (data)->
                if !data || !data.authorId
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Records get: authorId variable error'
                            type: 'danger'
                    return false
                true

            # get journal item (single record)
            # ---------------
            getById: new RequestConstructor.klass 'get', urlRecords, (itemId)->
                if !itemId
                    if srvcConfig.noticeShow.errors
                        base.notice.show
                            text: 'Journal item get: itemId variable error'
                            type: 'danger'
                    return false
                true

        facade =
            save: (data)->
                if data.id
                    rqst.put.do data
                else
                    rqst.post.do data
            remove: rqst.remove.do
            getList: rqst.getList.do
            getById: rqst.getById.do

        return facade


        # submit new journal item
        # ---------------
#        submit = (data)->
#            if data.id then method = 'put' else method = 'post'
#            $q (resolve, reject)->
#                if validate data
#                    $http[method](url, data).then (res)->
#                        if res.data.success
#                            resolve res.data
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Journal submit validation error'
#                            type: 'danger'
#
        # save exists journal item
        # ---------------
#        save = (data)->
#            $q (resolve, reject)->
#                if validate data
#                    $http.put(url, data).then (res)->
#                        if res.data.success
#                            resolve res.data
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Journal save validation error'
#                            type: 'danger'
#
        # remove journal item
        # ---------------
#        remove = (itemId)->
#            $q (resolve, reject)->
#                if itemId && typeof itemId == 'number'
#                    $http.delete(url, { params: { id: itemId } }).then (res)->
#                        if res.data.success
#                            resolve res.data
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Journal delete: itemId variable error'
#                            type: 'danger'
#
        # get journal item (single record)
        # ---------------
#        getById = (itemId)->
#            $q (resolve, reject)->
#                if itemId
#                    $http.get(url + '/' + itemId).then (res)->
#                        if res.data.success
#                            resolve res.data
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Journal item get: itemId variable error'
#                            type: 'danger'
#
        # get records list
        # ---------------
#        getList = (data)->
#            $q (resolve, reject)->
#                if data && data.authorId
#                    $http.get(urlRecords, params: data).then (res)->
#                        if res.data.success
#                            resolve res.data
#                        else
#                            reject res.data
#                    , (res)->
#                        reject res
#                else
#                    reject()
#                    if srvcConfig.noticeShow.errors
#                        base.notice.show
#                            text: 'Records get: authorId variable error'
#                            type: 'danger'
#
#        return {
#        submit: submit
#        save: save
#        remove: remove
#        getById: getById
#        getList: getList
#        }