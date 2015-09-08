class cut extends Filter('shared')
    constructor: ->
        (value, wordwise, max, tail)->
            if !value then return ''
            max = parseInt max, 10
            if !max then return value
            if value.length <= max then return value
            value = value.substr 0, max
            if wordwise
                lastspace = value.lastIndexOf ' '
                if lastspace != -1
                    value = value.substr 0, lastspace
            return value + (tail || ' ...')