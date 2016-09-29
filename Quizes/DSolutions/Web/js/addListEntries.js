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
    $("#addEntry").click(function () {
        addNewEntry();
    });
});


function addNewEntry() {
    var newEntryValue = $("#newEntry").val().trim();
    if (newEntryValue !== "") {
        var isEveryThird = (($(".entryItem").length + 1) % 3) === 0;
        var styleClasses = "entryItem " + (isEveryThird ? "redEntryItem" : "blackEntryItem");
        $("#entryItems").append("<option class='" + styleClasses + "' value='" + newEntryValue + "'>" + newEntryValue + "</option>");
    }
}



