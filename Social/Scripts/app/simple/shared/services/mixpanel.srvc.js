'use strict';

// mixpanel
// ---------------
angular.module('shared')
    .factory('mixpanel',
    ['$location',
        function ($location) {

            // api
            // ---------------
            var api = function (action, event, prop) {
                if (window.mixpanel == null || window.mixpanel == undefined) {
                    console.log('mixpanel: mixpanel isn\'t defined');
                    return;
                }
                if (typeof mixpanel[action] !== 'function') {
                    console.log('mixpanel: method "mixpanel.' + action + '" is not defined');
                    return;
                }
                switch (action) {
                    case 'identify':
                        mixpanel.identify(event, prop);
                        break;
                    case 'people.set':
                        mixpanel.people.set(event, prop);
                        break;
                    default:
                        //console.log('mixpanel ' + action + ': event "' + event + '"');
                        mixpanel[action](event, prop);
                        break;
                }
                console.log(prop);
            };

            // ---------------
            return {
                // mixpanel api facade
                // ----------
                api: api,

                // preset events
                // ----------
                ev: {
                    visitPage: function (title) {
                        api('track', 'visit page', {
                            title: title,
                            url: $location.path()
                        });
                    }
                }
            }

        }
    ]);
