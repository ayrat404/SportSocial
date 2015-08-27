class serverValidation extends Directive('shared')
    constructor: (
        $timeout,
        base)->

        # generate error el func
        # ---------------
        template = (text, name)->
            angular.element('<div>', { class: 'field-error', 'data-target': name, text: text })

        # highlight error field func
        # ---------------
        highlightField = ($field)->
            if !$field.hasClass('fs-input--error')
                $field.addClass('fs-input--error')
                $timeout(->
                    $field.removeClass('fs-input--error')
                , 5000)

        # show error func
        # ---------------
        showError = ($el, error)->
            name = $el.attr 'name'
            $error = template error, name
            if $el.parent().find('.field-error[data-target="' + name + '"]').length
                $el.before $error
                $timeout(->
                    $error.hide('slow', ->
                        $error.remove()
                    )
                , 5000)

        # directive
        # ---------------
        return {
            restrict: 'A'
            scope:
                obj: '=serverValidation'
            link: (scope, element, attrs)->
                $container = angular.element(element)
                $form = $container.find('form')
                scope.$watch 'obj', (obj)->
                    if obj
                        if typeof obj.common == 'object' &&
                          obj.common.error &&
                          obj.common.error.length
                            showError($form, obj.common.error)
                            if obj.common.fields &&
                              obj.common.fields.length &&
                              base.isArray(obj.common.fields)
                                for i in [0...obj.common.fields.length]
                                    highlightField($form.find('[name="' + obj.common.fields[i] + '"]'))

                            if obj.fields &&
                              obj.fields.length &&
                              base.isArray(obj.fields)
                                for i in [0...obj.fields.length]
                                    $element = $form.find('[name="' + obj.fields[i].name + '"]')
                                    if $element.length
                                        showError($element.eq(0), obj.fields[j].error)
                                        highlightField($element.eq(0))
                return
        }