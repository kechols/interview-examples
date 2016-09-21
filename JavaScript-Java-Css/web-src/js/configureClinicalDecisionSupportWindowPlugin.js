/**
 * Created by kechols on 12/4/2015.
 *
 *
 * This "configureClinicalDecisionSupportWindowPlugin" file is a javascript plugin. Being a plugin it is an
 * object and not a function bucket like most of the previous *.js files. The "configureClinicalDecisionSupportWindowPlugin" is responsible
 * for the behavior of the configure clinical decision support window or index.html file
 *
 * Please name functions and variables descriptively and appropriately as this file was written to be living documentation.
 *  - Comments can be limited if the code says what it is doing -
 *  - Renaming variables and functions as there intention changes is a good thing. And it is well worth everyone's time.
 *
 *  Please keep the code tidy. There is no need to leave dead or commented out code around. If it is commented out after one or more checkins
 *  then it is just noise that some else will have to read through. We are all smart people. We don't have to leave dead code around as
 *  examples of what we decided not to do (just like we don't leave artifacts in word documents)
 *
 *  Please keep functions that have the same scope in alphabetical order. It makes things easier to find as documented in the clean code
 *  book.
 *
 *  Please indent and use proper spacing just as you would in any document. As mentioned this file is living documentation and it should be
 *  maintained as if it were a word document that is to be read by others.
 */


(function ($) {

    $.configureClinicalDecisionSupportWindowPlugin = function(element) {
        // ----------------------------- Public functions ---------------------------------------


        this.loadUi = function () {
            loadUiDataAndAttachEventHandlers();
            setInterval(checkIfLoggingOut, checkIfLoggingOutInterval);
        };


        // ----------------- Private functions and variables - please keep in alphabetical order ------------------------------------------
        var checkIfLoggingOutInterval = 1000;
        var gridHeightBuffer = 190;
        var gridWidthBuffer = 65;
        var lastSelecteRowClass = "lastSelectedRow";
        var standardUnHighligtableGroupCellClass = "ui-th-column ui-th-ltr ui-th-rtl ui-state-default";


        var addCheckboxEventHandlers = function () {
            $("table tbody").on("change", "input[type=checkbox]", function () {
                var actedUponCheckbox = $(this);
                var vendorname = "";
                if (actedUponCheckbox.is(".groupHeader")) {
                    var containerWithModalityAndVendorAttributes = actedUponCheckbox.closest("th");
                    var procedureCheckboxesForModalityAndVendor = getModalityAndVendorCheckBoxesOfType(
                        getCheckboxTypeEnumDefinition().PROCEDURE,
                        containerWithModalityAndVendorAttributes
                    );
                    var theGroupIsChecked = this.checked;
                    procedureCheckboxesForModalityAndVendor.each(function () {
                        if (!this.checked || !theGroupIsChecked) {
                            $(this).prop("checked", theGroupIsChecked);
                        }
                    });
                    vendorname = containerWithModalityAndVendorAttributes.attr("vendorname");
                    highlightRowOfCheckboxClicked(containerWithModalityAndVendorAttributes);
                }
                else {
                    var containerWithModalityAndVendorAttributes = actedUponCheckbox.parent();
                    checkGroupCheckboxIfNeccesary(containerWithModalityAndVendorAttributes);
                    vendorname = containerWithModalityAndVendorAttributes.attr("vendorname");
                    highlightRowOfCheckboxClicked(containerWithModalityAndVendorAttributes);
                }

                setTimeout(function () {
                    updateHeaderCountersForVendor(vendorname);
                    setTimeout(function () {
                        updateModifiedPersistableCdsConfigDataAfterRecentChanges(
                            vendorname,
                            $.parseJSON($("#vendorNameToVendorIdData").val())
                        ).done(function (modifiedPersistableCdsConfigData) {
                                enableSaveButton(areThereChangesToTheConfigData(modifiedPersistableCdsConfigData));
                            }).fail(function (data) {
                                alert("The system failed to update the modified persistable CdsConfigData After Recent Changes. " + data);
                            });
                    }, 25);
                }, 5);

            });
        };


        var addConfigureVendorButtonAndEventHandlers = function () {
            $("#configurableContents").jqGrid(
                'navGrid',
                '#configurableContentsPagination',
                {
                    add:false,
                    del : false,
                    search : false,
                    refresh : false
                },
                {}, // edit parameters
                { // add parameters
                    url : ''
                }
            );

            getHtmlTemplate("configureVendorsButtonTemplate.html").done(function (configureVendorsButtonHtml) {
                $("#edit_configurableContents").
                    attr("title", "Configure the CDS vendor(s)").
                    addClass("ui-dominate-button-enabled configureClinicalDecisionSupportWindowConfigureVendors").
                    html(configureVendorsButtonHtml);
            });

        };


        var addDominateButtonsAndTheirEventHandlers = function () {
            getHtmlTemplate("dominateButtonsTemplate.html").done(function (dominateButtonsHtml) {
                $(".ui-paging-pager tbody").html(dominateButtonsHtml);

                $(".configureClinicalDecisionSupportWindowSave").click(function () {
                    if (isDominateButtonEnabled($(this))) {
                        performDominateButtonOperation(true);
                    }
                });

                $(".configureClinicalDecisionSupportWindowCancel").click(function () {
                    performDominateButtonOperation(false);
                });
            }).fail(function (data) {
                alert("The system failed to add the 'dominate Buttons html'. " + data);
            });
        };


        var addModalityDisplayManipulationHeaderAndEventHandlers = function () {
            getHtmlTemplate("modalityDisplayManipulationTemplate.html").done(function (modalityDisplayManipulationHtml) {
                $("#configurableContents_modality").html(modalityDisplayManipulationHtml);

                $(".collapseAllModalityItems").click(function () {
                    expandOrCollapseItems(false);
                });

                $(".expandAllModalityItems").click(function () {
                    expandOrCollapseItems(true);
                });
            }).fail(function (data) {
                alert("The system failed to add the modality display manipulation header." + data);
            });
        };


        var addSelectAllInGroupCheckboxes = function (vendorColumns) {
            getHtmlTemplate("checkAllInGroupCheckboxCellTemplate.html").done(function (checkAllInGroupCheckboxCellTemplate) {
                var modalityGroupGridInfo = $("#configurableContents").jqGrid("getGridParam", "groupingView").groups;

                $.each($("tr.jqgroup"), function (index) {
                    var modalityGroupRow = $(this).click(function () {
                        highlightRow($(this));
                    });
                    modalityGroupRow.find("td:eq(0)").attr("colspan", 2);

                    var modalityValue = modalityGroupGridInfo[index].displayValue;

                    $.each(getDisplayedVendorColumns(vendorColumns), function () {
                        var checkAllInGroupCheckboxCell = $(checkAllInGroupCheckboxCellTemplate);
                        checkAllInGroupCheckboxCell.attr("modality", modalityValue);
                        checkAllInGroupCheckboxCell.attr("vendorname", this);
                        modalityGroupRow.append(checkAllInGroupCheckboxCell);
                        checkGroupCheckboxIfNeccesary(checkAllInGroupCheckboxCell);
                        updateHeaderCountersForVendor(this);
                    });
                });
            }).fail(function (data) {
                alert("The system failed to add the 'select all in group checkboxes'." + data);
            });
        };


        var addSelectionCountersToVendorHeaders = function (vendorColumns) {
            getHtmlTemplate("vendorHeaderTemplate.html").done(function (selectionCountersForVendorHeaders) {
                $.each(getDisplayedVendorColumns(vendorColumns), function () {
                    $("[id='configurableContents_\"" + this + "\"']").html(
                        getSelectionCounterHtmlForVendor(
                            this,
                            replaceText(selectionCountersForVendorHeaders, 'vendorNameGoesHere', this)
                        )
                    );
                    setVendorCounterDescriptions(this);
                    updateHeaderCountersForVendor(this);
                });
            }).fail(function (data) {
                alert("The system failed to add the 'selection counters for vendor headers'." + data);
            });
        };


        var areThereChangesToTheConfigData = function (modifiedPersistableCdsConfigData) {
            var originalPersistableCdsConfigData = $.parseJSON($("#originalPersistableCdsConfigData").val());
            return $(modifiedPersistableCdsConfigData).not(
                    function (index) {
                        return this.supported == originalPersistableCdsConfigData[index].supported;
                    }
                ).length != 0;
        };


        var calculateCdsConfigChangedData = function () {
            var originalPersistableCdsConfigData = $.parseJSON($("#originalPersistableCdsConfigData").val());
            var changes = [];
            $.each(
                $($.parseJSON($("#modifiedPersistableCdsConfigData").val())).not(
                    function (index) {
                        return this.supported == originalPersistableCdsConfigData[index].supported;
                    }
                ),
                function () {
                    changes.push(
                        {
                            procedureId: this.procedureId,
                            supported: this.supported,
                            vendorId: this.vendorId
                        }
                    );
                }
            );
            $("#cdsConfigChangedData").val(JSON.stringify(changes));
        };


        var checkGroupCheckboxIfNeccesary = function (containerWithModalityAndVendorAttributes) {
            var procedureCheckboxesForModalityAndVendor = getModalityAndVendorCheckBoxesOfType(
                getCheckboxTypeEnumDefinition().PROCEDURE,
                containerWithModalityAndVendorAttributes
            );

            var allAreChecked = procedureCheckboxesForModalityAndVendor.filter(":checked").length === procedureCheckboxesForModalityAndVendor.length;
            getModalityAndVendorCheckBoxesOfType(
                getCheckboxTypeEnumDefinition().GROUP,
                containerWithModalityAndVendorAttributes
            ).prop("checked", allAreChecked);
        };



        var  checkIfLoggingOut = function (){
            if (toBoolean($.cookie('loggingOut')))  {
                closeConfigureCdsWindow();
            }
        };


        var closeConfigureCdsWindow = function () {
            window.open('', '_self', '');
            window.close();
        };


        var enableSaveButton = function (enable) {
            var saveButton = $(".configureClinicalDecisionSupportWindowSave");
            var enableButtonClasses = "ui-dominate-button-enabled";
            var disableButtonClasses = "ui-state-disabled ui-dominate-button-disabled";

            if (enable != isDominateButtonEnabled(saveButton)) {
                var newButtonClasses = enable ? enableButtonClasses : disableButtonClasses;
                var oldButtonClasses = enable ? disableButtonClasses : enableButtonClasses;
                saveButton.removeClass(oldButtonClasses).addClass(newButtonClasses);
            }
        };


        var enableSaveButtonBasedOnSaveSucceeding = function (saveResponseText, postedForm) {
            if(new RegExp('\\balready\\b').test(saveResponseText)){
                window.location = window.location.href;
            }
            else {
                var saveSucceeded = new RegExp('\\bsucceeded\\b').test(saveResponseText)
                $("#originalPersistableCdsConfigData").val((saveSucceeded ?
                        postedForm.find("#modifiedPersistableCdsConfigData").val() :
                        postedForm.find("#originalPersistableCdsConfigData").val()
                ));

                $("#modifiedPersistableCdsConfigData").val(postedForm.find("#modifiedPersistableCdsConfigData").val());
                $("#vendorNameToVendorIdData").val(postedForm.find("#vendorNameToVendorIdData").val());
                enableSaveButton(!saveSucceeded);
            }
        };


        var endsWithId = function (value) {
            return value.indexOf(" Id", value.length - value.length) !== -1;
        };


        var expandOrCollapseItems = function (expandAll) {
            var toggleClass = expandAll ?
                "ui-icon-circlesmall-plus" :
                "ui-icon-circlesmall-minus";
            $.each($("table tbody span." + toggleClass).closest("tr"), function () {
                $("#configurableContents").jqGrid('groupingToggle', $(this).attr("id"));
            });
        };


        var extractOriginalPeristableCdsConfigData = function (dataRows, vendors) {
            var notification = $.Deferred();
            var originalPersistableCdsConfigData = [];
            var vendorNameToVendorIdData = {};
            $.each(dataRows, function () {
                var dataRow = this;
                $.each(vendors, function () {
                    originalPersistableCdsConfigData.push(
                        {
                            procedureId: dataRow.id,
                            supported: dataRow[this],
                            vendorId: parseInt(dataRow[this + " Id"])
                        }
                    );
                    if (!vendorNameToVendorIdData.hasOwnProperty(this)) {
                        vendorNameToVendorIdData[this] = parseInt(dataRow[this + " Id"]);
                    }
                });
            });
            notification.resolve(originalPersistableCdsConfigData, vendorNameToVendorIdData);
            return notification.promise();
        };


        var extractTheCdsConfigGridData = function (vendorColumns) {
            return storeOriginalPersistableCdsConfigData(
                getGridDataWithMetaDataAdded($.parseJSON(urlDecode($("#cdsConfigGridData").val())).rows, vendorColumns),
                getDisplayedVendorColumns(vendorColumns)
            );
        };


        var extractTheCdsConfigGridVendorColumns = function () {
            return $.parseJSON(urlDecode($("#cdsConfigGridVendorColumns").val())).vendorColumns;
        };


        var getBaseUrl = function () {
            var value = window.location.protocol;
            value = value + "//" + window.location.hostname;
            value = value + ":" + getPort();
            return value;
        };


        var getCheckboxTypeEnumDefinition = function () {
            return {
                GROUP: 0,
                PROCEDURE: 1
            }
        };


        var getCheckboxType = function (checkboxTypeEnum) {
            return checkboxTypeEnum == getCheckboxTypeEnumDefinition().GROUP ? "th" : "td";
        };


        var getColumnModel = function (vendorColumns) {
            var columnModel = [
                {name: 'id', index: 'id', width: 1, sortable: false, hidden: true},
                {name: 'modality', index: 'modality', width: 90, sortable: false},
                {name: 'procedure', index: 'procedure', width: 160, sortable: false}
            ];

            var columnWidth = parseInt(515 / (vendorColumns.length / 2));
            $.each(vendorColumns, function () {
                columnModel.push(getVendorColumnModelObject(this, columnWidth));
            });

            return columnModel;
        };


        var getDisplayedVendorColumns = function (vendorColumns) {
            return $.grep(vendorColumns, function (vendorColumn) {
                return !endsWithId(vendorColumn);
            });
        };


        var getGridColumns = function (vendorColumns) {
            var gridColumns = ["Id", "Modality", "Procedure"];

            $.each(vendorColumns, function () {
                gridColumns.push(this);
            });

            return gridColumns;
        };


        var getGridDataWithMetaDataAdded = function (rows, vendorColumns) {
            var gridDataWithMetaDataAdded = [];
            $.each(rows, function () {
                row = this;
                var cells = {id: row.cells[0], modality: row.cells[1], procedure: row.cells[2]};
                $.each(vendorColumns, function (index) {
                    cells[this] = row.cells[index + 3];
                });
                gridDataWithMetaDataAdded.push(cells);
            });
            return gridDataWithMetaDataAdded;
        };


        var getHtmlTemplate = function (htmlTemplateFileName) {
            return $.get(getBaseUrl() + "/configureClinicalDecisionSupport/" + htmlTemplateFileName + "?randomNoCache=" + new Date().getTime());
        };


        var getModalityAndVendorCheckBoxesOfType = function (checkboxTypeEnum, containerWithModalityAndVendorAttributes) {
            var modalityToCheck = containerWithModalityAndVendorAttributes.attr("modality");
            var vendorToCheck = containerWithModalityAndVendorAttributes.attr("vendorname");
            return  $("table tbody " +
                    getCheckboxType(checkboxTypeEnum) +
                    getModalitySelector(modalityToCheck)
                ).filter(function () {
                    return $(this).attr("vendorname") == vendorToCheck;
                }).find(":checkbox");
        };


        var getModalitySelector = function (modalityToCheck) {
            return "[modality='" +
                replaceText(modalityToCheck, "'", "\\'") +
                "']";
        };


        var getPort = function () {
            var portNumber = 0;
            if (Number(window.location.port) == 0) {
                if (window.location.protocol.toUpperCase() == "HTTPS:") {
                    portNumber = 443;
                } else {
                    portNumber = 80;
                }
            } else {
                portNumber = Number(window.location.port);
            }
            return portNumber;
        };


        var getSelectionCounterHtmlForVendor = function (vendorname, customizedSelectionCountersForVendorHeader) {
            var vendorClassPrefix = getVendorClassPrefix(vendorname);
            customizedSelectionCountersForVendorHeader = replaceText(customizedSelectionCountersForVendorHeader, 'vendorNameGoesHere', vendorname);
            $.each(["SelectedCounter", "SelectedCounterDescription", "UnSelectedCounter", "UnSelectedCounterDescription"], function () {
                customizedSelectionCountersForVendorHeader = replaceText(
                    customizedSelectionCountersForVendorHeader,
                    'vendor' + this + 'GoesHere',
                    vendorClassPrefix + this
                );
            });
            return customizedSelectionCountersForVendorHeader;
        };


        var getVendorCdsConfigDataFromUi = function (vendorName, vendorId) {
            var notification = $.Deferred();
            var uiRowsForVendor = $("table tbody " + getCheckboxType(getCheckboxTypeEnumDefinition().PROCEDURE) + "[vendorname='" + vendorName + "']");
            var dataRowsForVendor = [];
            $.each(uiRowsForVendor, function () {
                var currentVendorCheckbox = $(this).find(":checkbox");
                dataRowsForVendor.push({
                    procedureId: parseInt($(this).attr("procedureid")),
                    supported: $(currentVendorCheckbox)[0].checked,
                    vendorId: vendorId
                });
            });
            notification.resolve(dataRowsForVendor);
            return notification.promise();
        };


        var getVendorCheckboxSelectionCount = function (vendorname) {
            var checkboxesForVendor = $("table tbody " + getCheckboxType(getCheckboxTypeEnumDefinition().PROCEDURE) + "[vendorname='" + vendorname + "'] :checkbox");
            var numberChecked = checkboxesForVendor.filter(":checked").length;
            return {
                selected: numberChecked,
                unselected: checkboxesForVendor.length - numberChecked
            };
        };


        var getVendorClassPrefix = function (vendorname) {
            return replaceText(vendorname, ' ', '');
        };


        var getVendorColumnModelObject = function (vendorColumn, columnWidth) {
            var vendorColumnModelObject = {};

            if (endsWithId(vendorColumn)) {
                vendorColumnModelObject = {
                    name: vendorColumn, index: vendorColumn, width: 1, sortable: false, hidden: true
                };
            }
            else {
                vendorColumnModelObject = {
                    name: '"' + vendorColumn + '"',
                    index: vendorColumn,
                    width: columnWidth,
                    align: "center",
                    editable: true,
                    edittype: 'checkbox',
                    sortable: false,
                    formatter: function (cellvalue, options, rowObject) {
                        return '<input ' +
                                    (rowObject[vendorColumn] ? 'checked ' : '') +
                                    'class="configurableContentsPointer" type="checkbox" value="" offval="no">' +
                                '</input>';
                    },
                    cellattr: function (rowId, tableValue, rawObject, columnModel, rawData) {
                        return ' modality = "' + rawObject.modality +
                            '" procedure = "' + rawObject.procedure +
                            '" procedureId = "' + rawObject.id + '"' +
                            '" vendorname = "' + vendorColumn + '"' +
                            '" vendorId = "' + rawObject[vendorColumn + " Id"] + '"';
                    }
                };
            }

            return vendorColumnModelObject;
        };


        var getVendorHeaderCounter = function (vendorname, useForDescription, selected) {
            return $(
                "." +
                getVendorClassPrefix(vendorname) +
                (selected ? "SelectedCounter" : "UnSelectedCounter") +
                (useForDescription ? "Description" : "")
            );
        };


        var getVendorCounterDescription = function (vendorname, selected) {
            return "The number of '" +
                vendorname + "' " +
                (selected ? "SELECTED" : "UNSELECTED") +
                " item(s).";
        };


        var hidePleaseWait = function () {
            $(window).unblock({fadeOut : 0});
        };


        var highlightRow = function (rowToHighlight, dontSelect) {
            var lastSelectedGroupRow = $("table tbody ." + lastSelecteRowClass);
            if(lastSelectedGroupRow.length > 0){
                lastSelectedGroupRow.removeClass(lastSelecteRowClass);
                if (lastSelectedGroupRow.find("th").length > 0){
                    lastSelectedGroupRow.find("th").addClass(standardUnHighligtableGroupCellClass);
                }
            }

            rowToHighlight.addClass(lastSelecteRowClass);

            if(rowToHighlight.find("th").length > 0) {
                rowToHighlight.find("th").removeClass(standardUnHighligtableGroupCellClass);
            }

            if (!dontSelect) {
                $("#configurableContents").jqGrid(
                    'setSelection',
                    rowToHighlight.attr('id')
                );
            }
        };



        var highlightRowOfCheckboxClicked = function (containerOfCheckbox) {;
            var rowToHighlight = $(containerOfCheckbox.parents('tr:last'));

            highlightRow(rowToHighlight);
        };


        var isDominateButtonEnabled = function (dominateButton) {
            return dominateButton.is(".ui-dominate-button-enabled")
        };


        var loadUiDataAndAttachEventHandlers = function () {
            var vendorColumns = extractTheCdsConfigGridVendorColumns();
            $("#configurableContents").jqGrid({
                autowidth: true,
                caption: "Configure CDS Item(s)",
                colModel: getColumnModel(vendorColumns),
                colNames: getGridColumns(vendorColumns),
                data: extractTheCdsConfigGridData(vendorColumns),
                datatype: "local",
                emptyrecords: "No item(s) to view",
                grouping: true,
                groupingView: {
                    groupField: ['modality'],
                    groupColumnShow: [true],
                    groupText: ['<b>{0} - {1} Item(s)</b>'],
                    groupCollapse: true,
                    groupOrder: ['asc']
                },
                height: ($(window).height() - gridHeightBuffer),
                loadComplete: function () {
                    showPleaseWait();
                    setTimeout(function () {
                        performCustomGridLookAndFeelAdjustments(vendorColumns);
                        addSelectAllInGroupCheckboxes(vendorColumns);
                        addCheckboxEventHandlers();
                        addDominateButtonsAndTheirEventHandlers();
                        hidePleaseWait();
                    }, 250);
                },
                loadtext: "Loading....",
                onSelectRow: function(rowId) {
                    highlightRow($("#" + rowId), true);
                },
                pager: "#configurableContentsPagination",
                pgtext: "",
                rowNum: -1,
                shrinkToFit: true,
                sortable: false,
                sortname: 'modality',
                viewrecords: false,
                width: null
            });

            addConfigureVendorButtonAndEventHandlers();
        };


        var performCustomGridLookAndFeelAdjustments = function (vendorColumns) {
            $("td[aria-describedby='configurableContents_modality']").html("");
            $(".HeaderButton").hide();
            $("[id^='jqgh_configurableContents_']").addClass("configurableContents");
            $("#configurableContentsPagination").css("background", "#5c9ccc url(/configureClinicalDecisionSupport/images/ui-bg_gloss-wave_55_5c9ccc_500x100.png) 50% 50% repeat-x");

            addModalityDisplayManipulationHeaderAndEventHandlers();
            addSelectionCountersToVendorHeaders(vendorColumns);
            resizeGridWhenWindowResizes();
        };


        var performDominateButtonOperation = function (saveClicked) {
            if (saveClicked) {
                persistCdsConfigChangedData();
            }
            else {
                closeConfigureCdsWindow();
            }
        };


        var persistCdsConfigChangedData = function () {
            showPleaseWait()
            calculateCdsConfigChangedData();

            $('#cdsConfigForm').ajaxForm(
                {
                    success: function (responseText, statusText, xhr, postedForm) {
                        alert(responseText);
                        hidePleaseWait();
                        enableSaveButtonBasedOnSaveSucceeding(responseText, $(postedForm[0]));
                    }
                }
            ).submit();
        };


        var replaceText = function (stringValue, oldValue, newValue) {
            var expression = new RegExp(oldValue, 'gi');
            return stringValue.replace(expression, newValue);
        };


        var resizeGridWhenWindowResizes = function () {
            $(window).bind('resize', function () {
                $("#configurableContents").setGridWidth($(window).width() - gridWidthBuffer);
                $("#configurableContents").setGridHeight($(window).height() - gridHeightBuffer);
            }).trigger('resize');
        };


        var setVendorCounterDescriptions = function (vendorname) {
            getVendorHeaderCounter(vendorname, true, true).attr("title", getVendorCounterDescription(vendorname, true));
            getVendorHeaderCounter(vendorname, true, false).attr("title", getVendorCounterDescription(vendorname, false));
        };


        var showPleaseWait = function () {
            $(window).block({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                },
                fadeIn: 0
            });
        };


        var storeOriginalPersistableCdsConfigData = function (dataRows, vendors){
            extractOriginalPeristableCdsConfigData(dataRows, vendors).done(function (originalPersistableCdsConfigData, vendorNameToVendorIdData) {
                $("#modifiedPersistableCdsConfigData").val(JSON.stringify(originalPersistableCdsConfigData));
                $("#originalPersistableCdsConfigData").val(JSON.stringify(originalPersistableCdsConfigData));
                $("#vendorNameToVendorIdData").val(JSON.stringify(vendorNameToVendorIdData));
            });
            return dataRows;
        };


        var toBoolean = function (stringValue){
            try{
                return new RegExp("^\true|t|yes|y|1", "gi").test(stringValue);
            }
            catch (e){
                return false;
            }
        };


        var updateHeaderCountersForVendor = function (vendorname) {
            var vendorCheckboxSelectionCount = getVendorCheckboxSelectionCount(vendorname);
            getVendorHeaderCounter(vendorname, false, true).html(vendorCheckboxSelectionCount.selected);
            getVendorHeaderCounter(vendorname, false, false).html(vendorCheckboxSelectionCount.unselected);
        };


        var updateModifiedPersistableCdsConfigDataAfterRecentChanges = function (vendorName, vendorNameToVendorIdData){
            var notification = $.Deferred();
            var vendorId = vendorNameToVendorIdData[vendorName];
            setTimeout(function() {
                getVendorCdsConfigDataFromUi(vendorName, vendorId).done(function (vendorCdsConfigDataAfterRecentChanges) {
                    var modifiedPersistableCdsConfigData = $.parseJSON($("#modifiedPersistableCdsConfigData").val());
                    $.each(modifiedPersistableCdsConfigData, function(cdsDataObjectIndex) {
                        var modifiedPersistableCdsConfigObject = this;
                        if (modifiedPersistableCdsConfigObject.vendorId == vendorId) {
                            $.each(vendorCdsConfigDataAfterRecentChanges, function () {
                                if ( modifiedPersistableCdsConfigObject.procedureId == this.procedureId &&
                                    modifiedPersistableCdsConfigObject.supported != this.supported
                                ) {
                                    modifiedPersistableCdsConfigData[cdsDataObjectIndex].supported = this.supported;
                                }
                            });
                        }
                    });
                    $("#modifiedPersistableCdsConfigData").val(JSON.stringify(modifiedPersistableCdsConfigData));
                    notification.resolve(modifiedPersistableCdsConfigData);
                });
            }, 5);

            return notification.promise();
        };


        var urlDecode = function (encodedValue) {
            encodedValue = encodedValue.replace(new RegExp('\\+', 'g'), ' ');
            return decodeURIComponent(encodedValue);
        };
    }


    // Exposed Plugin function for the users to construct the plugin
    $.fn.configureClinicalDecisionSupportWindowPlugin = function () {
        return this.each(function () {
            var element = $(this);
            // Return early if this element already has a plugin instance
            if (element.data(configureClinicalDecisionSupportWindowPluginDataKey())) {
                return;
            }

            var plugin = new $.configureClinicalDecisionSupportWindowPlugin(this);
            // Store plugin object in this element's data
            element.data(configureClinicalDecisionSupportWindowPluginDataKey(), plugin);
        });
    };
})(jQuery);


// --------------------------- Global functions - Please keep in alphabetical order -------------------------------------


function configureClinicalDecisionSupportWindowPluginDataKey() {
    return "ConfigureClinicalDecisionSupportDataKey";
}


function getConfigureClinicalDecisionSupportWindowPlugin() {
    var bodyReference = $("body");

    // Initialize the configureClinicalDecisionSupportWindowPlugin for the above bodyReference if it has not been
    bodyReference.configureClinicalDecisionSupportWindowPlugin();

    return bodyReference.data(configureClinicalDecisionSupportWindowPluginDataKey());
}
