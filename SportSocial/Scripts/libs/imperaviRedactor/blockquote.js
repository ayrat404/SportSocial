// -------------------------------------------------------------------------
if (typeof RedactorPlugins === 'undefined') {
    var RedactorPlugins = {};
}
RedactorPlugins.advanced = {
    init: function () {
        this.buttonAdd('quote', 'Quote', this.insertAdvancedHtml);
    },
    insertAdvancedHtml: function (buttonName, buttonDOM, buttonObj, e) {
        this.exec('formatBlock', 'blockquote');
    }
};
// -------------------------------------------------------------------------