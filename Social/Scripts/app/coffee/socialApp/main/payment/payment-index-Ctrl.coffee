class PaymentIndex extends Controller('socialApp.controllers')
    constructor: ($sce, paymentService)->

        data = {
            tariffId: null
            systemId: null
        }

        _this = this
        _this.loading = true
        _this.mode = 0

        prepareTariffs = (tariffs)->
            base = (item for item in tariffs when item.month is 1)[0]
            for item in tariffs
                if item.id != base.id
                    item['profit'] = 100 - (item.cost * 100 / base.cost)
            return tariffs

        # get page info
        # ---------------
        paymentService.getInfo().then (res)->
            _this.trial = res.data.trial
            _this.tariffs = prepareTariffs res.data.tariffs
            _this.systems = res.data.systems
            _this.payment = res.data.payment
        , (res)->
            _this.pageError = true
        .finally (res)->
            _this.loading = false

        # on tariff select
        # ---------------
        _this.selectTariff = (tariffId)->
            _this.mode += 1
            data.tariffId = tariffId

        # on payment systems select
        # ---------------
        _this.selectSystem = (systemId)->
            data.systemId = systemId
            _this.loading = true

            # asdasd
#            _this.mode += 1
#            _this.stat = {
#                tariff: '1 мес.'
#                cost: '100 руб.'
#                system: 'PayPal'
#                form: $sce.trustAsHtml('<form><input type="hidden" value="12"/><button type="submit" class="btn btn-fill-green">Подвтвердить</button></form>')
#            }

            paymentService.init(data).then (res)->
                _this.mode += 1
                res.data.form = $sce.trustAsHtml res.data.form
                _this.stat = res.data
            , (res)->
                console.log 'error'
            .finally (res)->
              _this.loading = false


        # prev mode
        # ---------------
        _this.back = ->
            _this.mode -= 1 if _this.mode > 0


#
#        # fake data
#        _this.trial =
#            isEnabled: true
#            currentDays: 8
#            allDays: 12
#
#        tariffs = [
#            {id: 1, months: 1, cost: 100, curr: 'руб'}
#            {id: 2, months: 6, cost: 80, curr: 'руб'}
#            {id: 3, months: 12, cost: 60, curr: 'руб'}
#        ]


#        _this.payment =
#            status: true
#            until: '2015-09-23T17:13:32.487'

#        _this.tariffs = prepareTariffs tariffs
#
#        _this.systems = [
#            { id: 1, name: 'PayPal' }
#            { id: 2, name: 'Яндекс' }
#        ]
