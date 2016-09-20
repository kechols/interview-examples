/**
 * Created by kechols on 12/3/2015.
 *
 * Initializes the Configure Clinical Decision Support Admin Menu tem to take out the normal popup window crap that SRIS does.
 * It also is dependent on "configureClinicalDecisionSupportWindowPlugin.js" because it uses the plugin to open the CDS Window when
 * the menu is clicked
 *
 */


function openCdsConfig() {
    openCentered("/openCdsConfig.do", "kevinsCdsConfig", "scrollbars=0; status=0; width=1250; height=880;toolbar=no, resizable=yes", '880', '1250');
}
