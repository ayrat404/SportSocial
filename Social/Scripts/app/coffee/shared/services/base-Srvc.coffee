# Notices module
# ---------------
noticeCtrl = do ->
    $body = angular.element('body')
    defaults =
        type: 'info'
        delay: 9000
        autohide: true
    boxClass = 'float-notice alert alert-'
    bottomOffset = 20
    margin = 10
    notices = []

    # on window resize
    # ---------------
    angular.element(window).on 'resize', ->
        if notices.length
            for i in [0...notices.length]
                setBottomOffset(i)
            checkColumnHeight()

    # show notice
    # ---------------
    show = (opts)->
        options = mergeOpts(opts)
        $tmpl = angular.element('<div>', {class: boxClass + options.type}).html(options.text)
        $tmpl.id = new Date().getTime()
        $body.append($tmpl)
        notices.push($tmpl)
        setBottomOffset(notices.length - 1)

        checkColumnHeight()

        if options.autohide
            setTimeout(->
                hide($tmpl.id)
            , options.delay)

        return {
        $el: $tmpl
        id: $tmpl.id
        }

    # hide notice
    # ---------------
    hide = (it)->
        if it != null && it != undefined
            if typeof it == 'object' && it.id != undefined then id = it.id
            else if typeof it == 'number' then id = it
            for i in [0...notices.length]
                if notices[i] != undefined && notices[i].id == id
                    notices[i].remove()
                    notices.splice(i, 1)
                    for j in [0...notices.length]
                        setBottomOffset(j)
                    break

    # hide all notices
    # ---------------
    hideAll = ->
        for i in [0...notices.length]
            notices[i].remove()
        notices = []

    # set bottom offset for notice
    # ---------------
    setBottomOffset = (i)->
        offset = bottomOffset
        if notices[i] != undefined
            if notices[i - 1] != undefined
                for j in [0..notices.indexOf(notices[i - 1])]
                    offset += notices[j].outerHeight() + margin
            notices[i].css('bottom', offset)

    # check column height
    # ---------------
    checkColumnHeight = ->
        $lastEl = notices[notices.length - 1]
        wH = window.innerHeight
        bottom = parseInt($lastEl[0].style.bottom)
        if wH < $lastEl.outerHeight() + bottom
            hide(notices[0])
            if wH < $lastEl.outerHeight() + bottom
                checkColumnHeight()

    # merge options func
    # ---------------
    mergeOpts = (opts)->
        return angular.extend({}, defaults, opts)


    return {
        hide: hide
        hideAll: hideAll
        show: show
    }

# Animation module
# ---------------
animationCtrl = do ->
    return {
    add: ($el, x, callback)->
        $el.addClass(x + ' animated').one 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', ->
        $(this).removeClass(x + ' animated')
        if callback != undefined && typeof callback == 'function'
            callback()
    }

# Validation module
# ---------------
validationCtrl = do ->
    return {
    email: (email)->
        re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
        return re.test(email)
    }

# Format module
# ---------------
formatCtrl = do ->
    word = (count, words)->
        cnt =  +count.toString().substring(count.toString().length - 1, count.toString().length)
        if count > 4 && count < 21
            return words[2]
        if cnt == 1
            return words[0]
        else if cnt > 1 && cnt < 5
            return words[1]
        else return words[2]
    return {
        word: word
    }

# Delay module
# ---------------
delay = (callback, ms)->
    timer = 0
    clearTimeout(timer)
    timer = setTimeout(callback, ms)

# Img resize module
# ---------------
imageCtrl = do ->
    resize = (imgUrl, params)->
        [imgUrl, '?w=', params.w, '&h=', params.h, '&mode=', params.mode].join('')
    return {
        resize: resize
    }

# service
# ---------------
angular.module('shared').factory('base', [->
    return {
    notice: noticeCtrl
    animation: animationCtrl
    validate: validationCtrl
    format: formatCtrl
    image: imageCtrl
    isArray: (array)->
        if Object.prototype.toString.call(array) == '[object Array]'
            return true
        false
    delayConstructor: delay
    GUID: ->
        s4 = ->
          Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1)
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4()
    }
])