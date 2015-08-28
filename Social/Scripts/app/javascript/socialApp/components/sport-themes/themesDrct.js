(function(){
var ThemesAutocomplete;

ThemesAutocomplete = (function() {
  function ThemesAutocomplete(journalSubmitService) {
    return {
      restrict: 'EA',
      require: 'ngModel',
      replace: true,
      scope: {
        themes: '=ngModel'
      },
      controller: 'themesAutocompleteController',
      templateUrl: '/Scripts/templates/components/sport-themesTpl.html',
      link: function(scope, element, attrs, ngModel) {}
    };
  }

  return ThemesAutocomplete;

})();

angular.module('socialApp.directives').directive('themesAutocomplete', ['journalSubmitService', ThemesAutocomplete]);

})();