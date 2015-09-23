(function(){
var serverValidation;

serverValidation = (function() {
  function serverValidation($timeout, base) {
    var highlightField, showError, template;
    template = function(text, name) {
      return angular.element('<div>', {
        "class": 'field-error',
        'data-target': name,
        text: text
      });
    };
    highlightField = function($field) {
      if (!$field.hasClass('fs-input--error')) {
        $field.addClass('fs-input--error');
        return $timeout(function() {
          return $field.removeClass('fs-input--error');
        }, 5000);
      }
    };
    showError = function($el, error) {
      var $error, name;
      name = $el.attr('name');
      $error = template(error, name);
      if (!$el.parent().find('.field-error[data-target="' + name + '"]').length) {
        $el.after($error);
        return $timeout(function() {
          return $error.hide('slow', function() {
            return $error.remove();
          });
        }, 5000);
      }
    };
    return {
      restrict: 'A',
      scope: {
        obj: '=serverValidation'
      },
      link: function(scope, element, attrs) {
        var $container, $form;
        $container = angular.element(element);
        $form = $container.find('form');
        scope.$watch('obj', function(obj) {
          var $element, i, j, k, ref, ref1, results;
          if (obj) {
            if (obj.common !== null && obj.common !== void 0 && obj.common.error && obj.common.error.length) {
              showError($form, obj.common.error);
              if (obj.common.fields && obj.common.fields.length && base.isArray(obj.common.fields)) {
                for (i = j = 0, ref = obj.common.fields.length; 0 <= ref ? j < ref : j > ref; i = 0 <= ref ? ++j : --j) {
                  highlightField($form.find('[name="' + obj.common.fields[i] + '"]'));
                }
              }
            }
            if (obj.fields && obj.fields.length && base.isArray(obj.fields)) {
              results = [];
              for (i = k = 0, ref1 = obj.fields.length; 0 <= ref1 ? k < ref1 : k > ref1; i = 0 <= ref1 ? ++k : --k) {
                $element = $form.find('[name="' + obj.fields[i].name + '"]');
                if ($element.length) {
                  showError($element.eq(0), obj.fields[i].error);
                  results.push(highlightField($element.eq(0)));
                } else {
                  results.push(void 0);
                }
              }
              return results;
            }
          }
        });
      }
    };
  }

  return serverValidation;

})();

angular.module('shared').directive('serverValidation', ['$timeout', 'base', serverValidation]);

})();