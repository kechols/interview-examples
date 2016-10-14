/**
 * Created by kechols on 9/29/2016.
 *
 *
 * This "addListItem" file is a javascript that is responsibe for
 * initializing the loading of the addListEntries.html content page. 
 * 
 * In particular it is resposible for making sure that the:
 * -- add item button click behaviour occurs as described in the quiz question
 * ----- no empty entries are added to the list box
 * ----- every third entry is red
 * ----- all other entries are black
 *
 *
 */


$(function () {
    // Initialize the plugin that manages the behaviour
    getAddListEntriesPlugin().bindEventHandlers();
});


// ---------------------------- Beginning of Plugin code
(function ($) {

    $.addListEntriesPlugin = function (element) {
        // ----------------------------- Public functions ---------------------------------------

        this.bindEventHandlers = function () {
            bindDataEntryEventHandlers();
        };


        // ----------------- Private functions and variables ------------------------------------

        var addNewEntry = function () {
            var newEntryValue = $("#newEntry").val().trim();
            if (newEntryValue !== "") {
                var isEveryThird = (($(".entryItem").length + 1) % 3) === 0;
                var styleClasses = "entryItem " + (isEveryThird ? "redEntryItem" : "blackEntryItem");
                $("#entryItems").append("<option class='" + styleClasses + "' value='" + newEntryValue + "'>" + newEntryValue + "</option>");
            } else {
                alert("Entry must have non blank characters.");
            }
        };


        var bindDataEntryEventHandlers = function () {
            $("#addEntry").click(function () {
                addNewEntry();
            });

            $("#newEntry").keyup(function(keyUpEvent) {
                if (keyUpEvent.keyCode === 13) {
                    addNewEntry();
                }
            });
        };
    }


    // Exposed Plugin function for the users to construct the plugin
    $.fn.addListEntriesPlugin = function () {
        return this.each(function () {
            var element = $(this);
            // Return early if this element already has a plugin instance
            if (element.data(addListEntriesPluginDataKey())) {
                return;
            }

            var plugin = new $.addListEntriesPlugin(this);
            // Store plugin object in this element's data
            element.data(addListEntriesPluginDataKey(), plugin);
        });
    };
})(jQuery);
// ------------------------- End of Plugin Code



// --------------------------- Global functions -------------------------------------


function addListEntriesPluginDataKey() {
    var thisFunctionsName = arguments.callee.toString();
    // Will removes the ')'
    thisFunctionsName = thisFunctionsName.substr('function '.length);
    // Will return "addListEntriesPluginDataKey" or what ever name you give to this function
    // after removing the '('
    return thisFunctionsName.substr(0, thisFunctionsName.indexOf('('));
}


function getAddListEntriesPlugin() {
    var bodyReference = $("body");

    // Initialize the addListEntriesPlugin for the above bodyReference if it has not been
    bodyReference.addListEntriesPlugin();

    return bodyReference.data(addListEntriesPluginDataKey());
}



