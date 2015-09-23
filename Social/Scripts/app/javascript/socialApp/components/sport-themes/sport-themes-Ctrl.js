(function(){
var ThemesAutocomplete;

ThemesAutocomplete = (function() {
  function ThemesAutocomplete($scope, sportThemesService, base) {
    if (!base.isArray($scope.themes)) {
      $scope.themes = [];
    }
    $scope.getThemes = function(search) {
      return sportThemesService.get({
        query: search
      }).then(function(res) {
        if (res.data.length) {
          return res.data;
        } else {
          return [search];
        }
      });
    };
    $scope.format = function($item, $model, $label) {
      if ($scope.themes.indexOf($item) !== -1) {
        base.notice.show({
          text: 'Theme "' + $item + '" is already selected',
          type: 'warning'
        });
      } else {
        $scope.search = '';
        $scope.themes.push($item);
      }
    };
    $scope.removeTag = function(tag) {
      var index;
      index = $scope.themes.indexOf(tag);
      if (index !== -1) {
        return $scope.themes.splice(index, 1);
      }
    };
  }

  return ThemesAutocomplete;

})();

angular.module('socialApp.controllers').controller('themesAutocompleteController', ['$scope', 'sportThemesService', 'base', ThemesAutocomplete]);

})();