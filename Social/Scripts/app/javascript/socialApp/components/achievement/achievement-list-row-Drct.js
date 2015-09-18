var achievementListRow;

achievementListRow = (function() {
  function achievementListRow() {
    return {
      restrict: 'E',
      replace: true,
      scope: {
        achievements: '=ngModel'
      },
      templateUrl: '/template/components/achievements-list-rowTpl'
    };
  }

  return achievementListRow;

})();

angular.module('socialApp.directives').directive('achievementListRow', [achievementListRow]);
