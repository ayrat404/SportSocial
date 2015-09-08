class QueryParams extends Service('shared')
    constructor: (
        modalService)->

        # run service
        # ---------------
        queryServices =
            # media modal show
            # ----------
            media: (params)->
                modalService.show
                    name: 'mediaShow'
                    data: params

        # check func
        # ---------------
        check = (params)->
            for k,v of params
                if queryServices[k] != undefined && v != undefined
                    queryServices[k](params)

        # ---------------
        return {
            check: check
        }