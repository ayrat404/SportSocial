class RequestConstructor extends Factory('appSrvc')
    constructor: ($q, $http)->

        class RC
            constructor: (@type, @url, @validate, @success)->
            isValid: (data)->
                if typeof @validate == "function"
                    @validate data
                else
                    true
            do: (data)=>
                _this = this
                $q (resolve, reject)->
                    if _this.isValid data
                        if typeof data == 'object' && (_this.type == 'get' || _this.type == 'delete')
                            data =
                                params: data
                        $http[_this.type](_this.url, data).then (res)->
                            if res.data.success
                                resolve res.data
                                _this.success?(res.data)
                            else
                                reject res.data
                        , (res)->
                            reject res
                    else
                        reject()

        return {
            klass: RC
        }