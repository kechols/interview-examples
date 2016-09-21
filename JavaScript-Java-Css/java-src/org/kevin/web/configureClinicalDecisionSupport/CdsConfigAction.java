package org.kevin.web.configureClinicalDecisionSupport;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import org.kevin.server.dataFactories.ClinicalDecisionSupportModalityAndProcedureFactory;
import org.kevin.server.dataFactories.ClinicalDecisionSupportVendorFactory;
import org.kevin.server.persisters.ClinicalDecisionSupportVendorToProcedureMappingPersister;
import org.kevin.server.persisters.ClinicalDecisionSupportVendorToProcedureMappingPersister.PersistAllResult;
import corg.kevin.server.valueObject.CdsModalityAndProcedureValue;
import org.kevin.server.valueObject.CdsVendorValue;
import org.apache.log4j.LogManager;
import org.apache.log4j.Logger;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.lang.reflect.Type;
import java.net.URLEncoder;
import java.util.*;

/**
 *
 * Created by IntelliJ IDEA.
 * User: kechols
 * Date: 01-Dec-2015
 * Time: 3:56:16 PM
 *
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
public class CdsConfigAction extends BasicAction {

    private final static Logger log = LogManager.getLogger(CdsConfigAction.class);

    public ActionForward openCdsConfig(ActionMapping mapping, ActionForm actionForm, HttpServletRequest request, HttpServletResponse response) throws Exception {
        IBusinessContextInfo contextInfo = getContextInfo(request);
        try {
            CdsConfigForm form = (CdsConfigForm) actionForm;
            List<CdsVendorValue> vendors = getCdsVendors();
            if(vendors.size() > 0) {
                form.setCdsConfigGridVendorColumns(URLEncoder.encode(getJqGridVendorColumns(vendors), "UTF-8"));
                form.setCdsConfigGridData(URLEncoder.encode(getJqGridRows(contextInfo, vendors), "UTF-8"));
            }
        }
        finally {
            contextInfo.cleanup();
        }

        return mapping.findForward(Keys.Forward.SUCCESS);
    }


    public ActionForward persistCdsConfigChangedData(ActionMapping mapping, ActionForm actionForm, HttpServletRequest request, HttpServletResponse response) throws Exception {
        IBusinessContextInfo contextInfo = getContextInfo(request);
        try {
            CdsConfigForm form = (CdsConfigForm) actionForm;
            Type cdsConfigDataListType =  new TypeToken<ArrayList<CdsConfigData>>(){}.getType();
            Type cdsConfigChangedDataListType =  new TypeToken<ArrayList<CdsConfigChangedData>>(){}.getType();

            PersistAllResult persistAllResult = ClinicalDecisionSupportVendorToProcedureMappingPersister.getInstance().persistAll(
                new Gson().fromJson(
                    form.getCdsConfigChangedData(),
                    cdsConfigChangedDataListType
                ),
                new Gson().fromJson(
                    form.getOriginalPersistableCdsConfigData(),
                    cdsConfigDataListType
                ),
                contextInfo
            );

            return mapping.findForward(persistAllResult.toString());
        }
        finally {
            contextInfo.cleanup();
        }
    }


    private List<Object> addVendorColumnValues(List<Object> rowCells, List <CdsVendorValue> cdsVendors, CdsModalityAndProcedureValue modalityAndProcedureValue, ClinicalDecisionSupportVendorToProcedureMappingPersister cdsVendorToProcedureMappingPersister){
        cdsVendors.stream().forEach((vendor)-> {
            rowCells.add(
                isProcedureSupportedByVendor(
                    modalityAndProcedureValue,
                    vendor,
                    cdsVendorToProcedureMappingPersister
                )
            );
            rowCells.add(vendor.getId());
        });
        return rowCells;
    }


    private List<CdsVendorValue> getCdsVendors(){
        List <CdsVendorValue> vendors = new ArrayList<>();
        try {
            vendors.addAll(ClinicalDecisionSupportVendorFactory.getInstance().getAll());
        }
        catch (Exception ex){
            log.error("CdsConfigAction:getCdsVendors failed :", ex);
        }
        return vendors;
    }


    private Map<String, Object> getJqGridObject(String key, Object value){
        Map<String, Object> map = new HashMap<String, Object>();
        map.put(key, value);
        return map;
    }


    private String getJqGridRows(IBusinessContextInfo contextInfo, List<CdsVendorValue> vendors) {
        List<JqGridCdsRow> rows = new ArrayList<>();
        List<CdsModalityAndProcedureValue> modalityAndProcedureValues = getModalityAndProcedureValues();
        if(modalityAndProcedureValues.size() > 0) {
            rows.addAll(getModalityAndProcedureRowData(modalityAndProcedureValues, vendors));
        }

        return new Gson().toJson(getJqGridObject("rows", rows));
    }


    private String getJqGridVendorColumns(List<CdsVendorValue> vendors) {
        List<String> vendorColumns = new ArrayList<>();

        vendors.stream().forEach((vendor)-> {
            vendorColumns.add(vendor.getName());
            vendorColumns.add(vendor.getName() + " Id");
        });

        return new Gson().toJson(getJqGridObject("vendorColumns", vendorColumns));
    }


    private List<CdsModalityAndProcedureValue> getModalityAndProcedureValues(){
        List<CdsModalityAndProcedureValue> modalityAndProcedureValues = new ArrayList<>();

        try {
            modalityAndProcedureValues.addAll(ClinicalDecisionSupportModalityAndProcedureFactory.getInstance().getAll());
        }
        catch(Exception ex){
            log.error("CdsConfigAction:getModalityAndProcedureValues failed!", ex);
        }

        return modalityAndProcedureValues;
    }



    private List<JqGridCdsRow> getModalityAndProcedureRowData(List<CdsModalityAndProcedureValue> modalityAndProcedureValues, List<CdsVendorValue> vendors) {
        List<JqGridCdsRow> rows = new ArrayList<>();

        try {
            // The cdsVendorToProcedureMappingPersister is created once because the mapping values are lazy loaded only once - the first time getAll is called.
            // This is so that the expensive call to get the vendor mappings for all procedures is only done once. Note: in some QA databases there are thousands of non retired procedures.
            ClinicalDecisionSupportVendorToProcedureMappingPersister cdsVendorToProcedureMappingPersister = ClinicalDecisionSupportVendorToProcedureMappingPersister.getInstance();
            cdsVendorToProcedureMappingPersister.clearCache();

            modalityAndProcedureValues.stream().forEach((modalityAndProcedureValue)-> {
                JqGridCdsRow row = new JqGridCdsRow();
                row.id = rows.size();
                row.cells = addVendorColumnValues (
                    new ArrayList(
                        Arrays.asList(modalityAndProcedureValue.getProcedureId(),
                        modalityAndProcedureValue.getModalityName(),
                        modalityAndProcedureValue.getProcedureName())
                    ),
                    vendors,
                    modalityAndProcedureValue,
                    cdsVendorToProcedureMappingPersister
                ).toArray();
                rows.add(row);
            });
        }
        catch(Exception ex){
            log.error("CdsConfigAction:getModalityAndProcedureRowData failed!", ex);
        }

        return rows;
    }


    private boolean isProcedureSupportedByVendor(CdsModalityAndProcedureValue modalityAndProcedureValue, CdsVendorValue vendor, ClinicalDecisionSupportVendorToProcedureMappingPersister cdsVendorToProcedureMappingPersister) {
        boolean isProcedureSupportedByVendor = false;

        try {
            isProcedureSupportedByVendor = cdsVendorToProcedureMappingPersister.isSupportedForVendor(modalityAndProcedureValue, vendor);
        }
        catch(Exception ex) {
            log.error("CdsConfigAction:isProcedureSupportedByVendor failed", ex);
        }

        return isProcedureSupportedByVendor;
    }
}