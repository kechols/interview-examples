/**
 * Created by kechols on 12/4/2015.
 *
 *
 * This "configureClinicalDecisionSupportWindow" file is a javascript that is responsibe for
 * initializing the loading of the configureClinicalDecisionSupportWindow. It is depends on the
 * the configureClinicalDecisionSupportWindowPlugin.js file
 * because the plugin has the loadUi method.
 *
 * I didn't want the document ready call as part of the plugin because by definition the plugin is
 * an object responsible for the behavior of the configureClinicalDecisionSupportWindow and not how/when
 * the screen is loaded
 *
 */


$(function () {
    // Initialize the plugin that manages the behaviour
    getConfigureClinicalDecisionSupportWindowPlugin().loadUi();
});



