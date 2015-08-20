'use strict';

angular.module('shared')
    .directive('serverValidation', [
        '$timeout',
        'base',
        function ($timeout, base) {

            // generate error el function
            // ---------------
            function template(text, name) {
                return $('<div>', { class: 'field-error', 'data-target': name, text: text });
            }

            // highlight error field func
            // ---------------
            function highlightField($field) {
                if (!$field.hasClass('fs-input--error')) {
                    $field.addClass('fs-input--error');
                    $timeout(function () {
                        $field.removeClass('fs-input--error');
                    }, 5000);
                }
            }

            // show error func
            // ---------------
            function showError($el, error) {
                var name = $el.attr('name'),
                    $error = template(error, name);
                if (!$el.parent().find('.field-error[data-target="' + name + '"]').length) {
                    $el.before($error);
                    $timeout(function () {
                        $error.hide('slow', function () {
                            $error.remove();
                        });
                    }, 5000);
                }
            }

            // drct
            // ---------------
            return {
                restrict: 'A',
                scope: {
                    obj: '=serverValidation'
                },
                link: function (scope, element, attrs) {
                    var $container = angular.element(element),
                        $form = $container.find('form');
                    scope.$watch('obj', function (obj) {
                        if (obj) {
                            if (typeof obj.common === 'object' && obj.common.error && obj.common.error.length) {
                                showError($form, obj.common.error);
                                if (obj.common.fields &&
                                    obj.common.fields.length &&
                                    base.isArray(obj.common.fields)) {
                                    for (var i = 0; i < obj.common.fields.length; i++) {
                                        highlightField($form.find('[name="' + obj.common.fields[i] + '"]'));
                                    }
                                }
                            }
                            if (obj.fields &&
                                obj.fields.length &&
                                base.isArray(obj.fields)) {
                                for (var j = 0; j < obj.fields.length; j++) {
                                    var $element = $form.find('[name="' + obj.fields[j].name + '"]');
                                    if ($element.length) {
                                        showError($element.eq(0), obj.fields[j].error);
                                        highlightField($element.eq(0));
                                    }
                                }
                            }
                        }
                    });
                }
            }
        }
    ]);