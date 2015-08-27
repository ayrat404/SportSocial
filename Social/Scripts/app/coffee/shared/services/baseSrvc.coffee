# Notices Class
# ---------------
noticeCtrl = do ->
    $body = angular.element('body')
    defaults =
        type: 'info'
        delay: 6000
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

    # notice for server responses
    # ---------------
    response = (data)->
        noticeClass = data.success ? 'success': 'warning'
        if data.message &&
          data.message.length
            show(
                text: data.message
                type: noticeClass
            )

    # set bottom offset for notice
    # ---------------
    setBottomOffset = (i)->
        offset = bottomOffset
        if notices[i] != undefined
            if notices[i - 1] != undefined
                for i in [0...notices.indexOf(notices[i - 1])]
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
        response: response
    }

# Animation Class
# ---------------
animationCtrl = do ->
    return {
    add: ($el, x, callback)->
        $el.addClass(x + ' animated').one 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', ->
        $(this).removeClass(x + ' animated')
        if callback != undefined && typeof callback == 'function'
            callback()
    }

# Validation Class
# ---------------
validationCtrl = do ->
    return {
    email: (email)->
        re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
        return re.test(email)
    }

# Delay Class
# ---------------
delay = (callback, ms)->
    timer = 0
    clearTimeout(timer)
    timer = setTimeout(callback, ms)


# service
# ---------------
angular.module('shared').factory('base', [->
    return {
    notice: noticeCtrl
    animation: animationCtrl
    validate: validationCtrl
    isArray: (array)->
        if Object.prototype.toString.call(array) == '[object Array]'
            return true
        false
    delayConstructor: delay
    }
])