'use sctict';

// клик не на элементе
// ---------------
angular
    .module('shared')
    .directive('clickOutside', ['$document', function ($document) {
        return {
            restrict: 'A',
            scope: {
                clickOutside: '&'
            },
            link: function ($scope, elem, attr) {
                var classList = (attr.outsideIfNot !== undefined) ? attr.outsideIfNot.replace(', ', ',').split(',') : [];
                if (attr.id !== undefined) classList.push(attr.id);
                $document.on('click touchstart', function (e) {
                    var i = 0,
                        element;
                    var test1 = e.target;
                    var test = elem.find(test1);
                    if (!e.target || elem.find(e.target).length > 0) return;

                    for (element = e.target; element; element = element.parentNode) {
                        var id = element.id,
                            classNames = element.className;

                        if (id !== undefined && id !== null) {
                            for (i = 0; i < classList.length; i++) {
                                if (id.indexOf(classList[i]) > -1 || classNames.indexOf(classList[i]) > -1) {
                                    return;
                                }
                            }
                        }
                    }
                    $scope.$eval($scope.clickOutside);
                });
            }
        };
    }]);