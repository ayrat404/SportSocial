class QueryParams extends Service('shared')
    constructor: (
        modalService)->

        # run service
        # ---------------
        queryServices =
            # media modal show
            # ----------
            media: (id)->
                debugger
                modalService.show(
                    name: 'mediaShow'
                    data: {
                        id: id
                    }
                )

        # todo параметры могут совмещаться

        # check func
        # ---------------
        check = (params)->
            for k,v of params
                if queryServices[k] != undefined
                    queryServices[k](v)

        # ---------------
        return {
            check: check
        }