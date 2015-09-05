class Journal extends Service('appSrvc')
    constructor: (
        $q
        $http
        $location
        $rootScope
        base
        servicesDefault)->

        url = servicesDefault.baseServiceUrl + '/journal'
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

        # submit new journal item
        # ---------------
        submit = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title
            $q (resolve, reject)->
                if validate data
                    $http.post(url, data).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                        base.notice.response(res) if servicesDefault.noticeShow.errors
                    , (res)->
                        reject(res)
                    )
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Journal submit validation error'
                            type: 'danger'
                        )
        # save exists journal item
        # ---------------
        save = (data, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title
            $q (resolve, reject)->
                if validate data
                    $http.put(url, data).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                        base.notice.response(res) if servicesDefault.noticeShow.errors
                    , (res)->
                        reject(res)
                    )
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Journal save validation error'
                            type: 'danger'
                        )

        # remove journal item
        # ---------------
        remove = (itemId, prop)->
            opts = angular.extend(servicesDefault, prop)
            evTrackProp =
                url: $location.path()
                title: $rootScope.title
            $q (resolve, reject)->
                if itemId && typeof itemId == 'number'
                    $http.delete(url, { params: { id: itemId } }).then((res)->
                        if res.data.success
                            resolve(res.data)
                        else
                            reject(res.data)
                        base.notice.response(res) if servicesDefault.noticeShow.errors
                    , (res)->
                        reject(res)
                    )
                else
                    reject()
                    if servicesDefault.noticeShow.errors
                        base.notice.show(
                            text: 'Journal delete: itemId variable error'
                            type: 'danger'
                        )

        return {
        submit: submit
        save: save
        remove: remove
        }