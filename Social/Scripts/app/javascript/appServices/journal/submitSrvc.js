(function(){
var JournalSubmit;

JournalSubmit = (function() {
  function JournalSubmit() {
    return {
      test: function() {
        return console.log('journal submit serrvice test action');
      }
    };
  }

  return JournalSubmit;

})();

angular.module('appSrvc').service('journalSubmitService', [JournalSubmit]);

})();