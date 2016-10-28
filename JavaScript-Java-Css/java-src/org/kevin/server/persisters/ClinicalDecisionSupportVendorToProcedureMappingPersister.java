package com.mbs.rlData.persisters;

import com.allscripts.rlweb.configureClinicalDecisionSupport.CdsConfigChangedData;
import com.allscripts.rlweb.configureClinicalDecisionSupport.CdsConfigData;
import com.mbs.rlData.dataFactories.*;
import com.mbs.rlData.dataLayer.StandardDataReaderWriter;
import com.mbs.rlData.dataLayer.metadata.TableDefinition;
import com.mbs.rlManagers.valueObject.CdsModalityAndProcedureValue;
import com.mbs.rlManagers.valueObject.CdsVendorToProcedureMappingValue;
import com.mbs.rlManagers.valueObject.CdsVendorValue;
import com.mbs.rlweb.util.IBusinessContextInfo;
import com.mbs.rlweb.util.Keys;
import org.apache.commons.lang.NotImplementedException;
import org.apache.commons.lang.StringUtils;
import org.apache.log4j.LogManager;
import org.apache.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;
import java.util.stream.Collectors;

/*
 * ClinicalDecisionSupportVendorToProcedureMappingPersister.java
 *
 * Creation :
 * User: kechols
 * Date: Nov 20, 2015
 * Time: 2:38:47 PM
 *
 */

public class ClinicalDecisionSupportVendorToProcedureMappingPersister extends BasicFactory implements IFactory {

    public enum PersistAllResult {
        CHANGES_ALREADY_EXIST("changesAlreadyExist"), FAILURE(Keys.Forward.FAILURE), SUCCESS(Keys.Forward.SUCCESS);

        private final String successText;

        PersistAllResult(String successText) {
            this.successText = successText;
        }

        @Override
        public String toString() {
            return successText;
        }
    }

    private final static Logger log = LogManager.getLogger(ClinicalDecisionSupportVendorToProcedureMappingPersister.class);

    // Having the below MAX_VENDOR_TO_PROCEDURE_MAPPING_UPDATE_SIZE is to avoid the Sql Sever Exception where the max
    // parameters of 2300 of the 2016 QA Databases is exceeded. Since there are 2 params for each vendor that we may desire to update
    // the max limit of vendors is being set to 500 - thus 500*2 parameters = 1000 Max parameters
    private static final int MAX_VENDOR_TO_PROCEDURE_MAPPING_UPDATE_SIZE = 500;

    private static ClinicalDecisionSupportVendorToProcedureMappingPersister instance;

    private List<CdsVendorToProcedureMappingValue> cachedMppingValues;


    public static ClinicalDecisionSupportVendorToProcedureMappingPersister getInstance() throws FactoryException {
        if (instance == null) {
            instance = new ClinicalDecisionSupportVendorToProcedureMappingPersister();
        }
        return instance;
    }


    private ClinicalDecisionSupportVendorToProcedureMappingPersister() throws FactoryException {
    }


    public void clearCache() throws FactoryException {
        log.debug("clear the cache");
        cachedMppingValues = null;
    }


    public void removeInstance() {
        instance = null;
    }


    public List<CdsVendorToProcedureMappingValue> getAll(){
        // NOTE: The clinicalDecisionSupportVendorToProcedureMappingPersister should be created once because the mapping values are lazy loaded only once - the first time getAll is called.
        // This is so that the expensive call to get the vendor mappings for all procedures is only done once. Note: in some QA databases there are thousands of non retired procedures.

        List<CdsVendorToProcedureMappingValue> cdsVendorToProcedureMappings= new ArrayList<>();

        if(cachedMppingValues == null) {
            Connection connection = null;
            StringBuilder getAllSql = new StringBuilder();
            try {
                getAllSql.append("select * from ").append(getTableName()).append(Keys.SQL.NOLOCK);

                if (log.isDebugEnabled()) {
                    log.debug("ClinicalDecisionSupportVendorToProcedureMappingPersister:getAll: " + getAllSql.toString() + " params: ");
                }

                connection = DataSourceFactory.getFactory().getConnection();

                try (PreparedStatement statement = connection.prepareStatement(getAllSql.toString())) {
                    try (ResultSet resultSet = statement.executeQuery()) {
                        while (resultSet.next()) {
                            CdsVendorToProcedureMappingValue mappingValue = new CdsVendorToProcedureMappingValue();
                            mappingValue.setPrimaryKey(resultSet.getInt("mappingId"));
                            mappingValue.setVendorId(resultSet.getInt("vendorId"));
                            mappingValue.setProcedureId(resultSet.getInt("procedureId"));
                            cdsVendorToProcedureMappings.add(mappingValue);
                        }
                    }
                }
            } catch (Exception ex) {
                log.error("ClinicalDecisionSupportVendorToProcedureMappingPersister:getAll: " + getAllSql.toString() + " params: " + getAllSql + " : FAILED : " + ex);
            } finally {
                DataSourceFactory.getFactory().closeConnectionIfSafe(connection);
                cachedMppingValues = new ArrayList<>();
                cachedMppingValues.addAll(cdsVendorToProcedureMappings);
            }
        }
        else {
            cdsVendorToProcedureMappings.addAll(cachedMppingValues);
        }

        return cdsVendorToProcedureMappings;
    }


    public Map getLocaleValues(Locale locale) {
        throw new NotImplementedException("Use getAll instead");
    }


    public String getTableName() {
        return TableDefinition.CDSVENDOR_TO_PROCEDURE_MAPPING_TABLE;
    }


    public boolean isSupportedForVendor(CdsModalityAndProcedureValue modalityAndProcedureValue, CdsVendorValue vendor){
        return getAll().stream().anyMatch(
            mapping ->  mapping.getProcedureId() == modalityAndProcedureValue.getProcedureId() &&
            mapping.getVendorId() == vendor.getPrimaryKey()
        );
    }


    public PersistAllResult persistAll(List<CdsConfigChangedData> cdsConfigChangedDataList, List<CdsConfigData> cdsConfigOriginalUiDataList, IBusinessContextInfo contextInfo){
        PersistAllResult result =  PersistAllResult.SUCCESS;
        try {
            guarantyNoConcurrentSaves();

            if (getCurrentCdsConfigDataList().stream().anyMatch(
                currentCdsConfigData -> !cdsConfigOriginalUiDataList.contains(currentCdsConfigData)
            )) {
                result = PersistAllResult.CHANGES_ALREADY_EXIST;
            }
            else {
                Connection connection = null;
                try {
                    connection = DataSourceFactory.getFactory().getConnection();
                    removeSupportForVendors(cdsConfigChangedDataList, connection, contextInfo);
                    addSupportForVendors(cdsConfigChangedDataList, connection, contextInfo);
                }
                catch(Exception ex){
                    result = PersistAllResult.FAILURE;
                    log.error("ClinicalDecisionSupportVendorToProcedureMappingPersister:persistAll : FAILED :  " + ex);
                }
                finally {
                    DataSourceFactory.getFactory().closeConnectionIfSafe(connection);
                }
            }
        }
        catch(Exception ex){
            result = PersistAllResult.FAILURE;
            log.error("ClinicalDecisionSupportVendorToProcedureMappingPersister:persistAll : FAILED :  " + ex);
        }

        return result;
    }


    private void addSupportForVendors(List<CdsConfigChangedData> cdsConfigChangedDataList, Connection connection, IBusinessContextInfo contextInfo) throws SQLException{
        List<CdsConfigChangedData> cdsConfigChangedDataWithDesiredVendorSupport = getCdsConfigChangedDataWithVendorSupport(true, cdsConfigChangedDataList);

        if(!cdsConfigChangedDataWithDesiredVendorSupport.isEmpty()) {
            StandardDataReaderWriter persister = new StandardDataReaderWriter(connection, contextInfo.getLoggedInUserValue().getUserNo());
            StringBuilder addSupportSqlPrefix = new StringBuilder("insert into ");
            addSupportSqlPrefix.append(getTableName() + " ");
            addSupportSqlPrefix.append("(vendorId, procedureId) values ");

            int step = MAX_VENDOR_TO_PROCEDURE_MAPPING_UPDATE_SIZE;
            for (int start = 0; start < cdsConfigChangedDataWithDesiredVendorSupport.size(); start += step) {
                int end = Math.min(start + step, cdsConfigChangedDataWithDesiredVendorSupport.size());
                List<String> valuesClauses = new ArrayList<>();
                List<Integer> paramValues = new ArrayList<>();

                cdsConfigChangedDataWithDesiredVendorSupport.subList(start, end).stream().forEach((cdsConfigChangedData)-> {
                    valuesClauses.add("(?, ?) ");
                    paramValues.add(cdsConfigChangedData.getVendorId());
                    paramValues.add(cdsConfigChangedData.getProcedureId());
                });

                StringBuilder addSupportSql = new StringBuilder(addSupportSqlPrefix.toString());
                addSupportSql.append(StringUtils.join(valuesClauses, ", "));

                if (log.isDebugEnabled()) {
                    log.debug("addSupportForVendors: " + addSupportSql.toString() + " params: " + StringUtils.join(paramValues, ","));
                }

                persister.executeSql(addSupportSql.toString(), paramValues.toArray());
            }
        }
    }


    private List<CdsConfigChangedData> getCdsConfigChangedDataWithVendorSupport(boolean hasSupport, List<CdsConfigChangedData> cdsConfigChangedDataList){
        return cdsConfigChangedDataList.stream()
                .filter(cdsConfigChangedData -> cdsConfigChangedData.isSupported() == hasSupport)
            .collect(Collectors.toList());
    }


    private List<CdsConfigData> getCurrentCdsConfigDataList() {
        List<CdsConfigData> currentCdsConfigDataList = new ArrayList<>();
        List<CdsModalityAndProcedureValue> modalityAndProcedureValues = new ArrayList<>(ClinicalDecisionSupportModalityAndProcedureFactory.getInstance().getAll());
        clearCache();

        ClinicalDecisionSupportVendorFactory.getInstance().getAll().stream().forEach((vendor)-> {
            modalityAndProcedureValues.forEach((modalityAndProcedureValue)-> {
                CdsConfigData currentCdsConfigData = new CdsConfigData(
                    modalityAndProcedureValue.getProcedureId(),
                    isSupportedForVendor(modalityAndProcedureValue, vendor),
                    vendor.getVendorId()
                );
                currentCdsConfigDataList.add(currentCdsConfigData);
            });
        });

        return currentCdsConfigDataList;
    }


    private void guarantyNoConcurrentSaves() throws InterruptedException{
        Random nonConcurrencyGenerator = new Random();
        Thread.sleep(((long)(nonConcurrencyGenerator.nextInt(333) +
                nonConcurrencyGenerator.nextInt(333) +
                nonConcurrencyGenerator.nextInt(334))
        ));
    }


    private void removeSupportForVendors(List<CdsConfigChangedData> cdsConfigChangedDataList, Connection connection, IBusinessContextInfo contextInfo) throws SQLException{
        List<CdsConfigChangedData> cdsConfigChangedDataWithDesiredVendorSupport = getCdsConfigChangedDataWithVendorSupport(false, cdsConfigChangedDataList);
        if(!cdsConfigChangedDataWithDesiredVendorSupport.isEmpty()) {
            StandardDataReaderWriter persister = new StandardDataReaderWriter(connection, contextInfo.getLoggedInUserValue().getUserNo());
            StringBuilder removeSupportSqlPrefix = new StringBuilder("delete from ");
            removeSupportSqlPrefix.append(getTableName() + " ");
            removeSupportSqlPrefix.append("where mappingId in ( ");
            removeSupportSqlPrefix.append("select mappingId from " + getTableName() + " where ");

            int step = MAX_VENDOR_TO_PROCEDURE_MAPPING_UPDATE_SIZE;
            for (int start = 0; start < cdsConfigChangedDataWithDesiredVendorSupport.size(); start += step) {
                int end = Math.min(start + step, cdsConfigChangedDataWithDesiredVendorSupport.size());
                List<String> wherePredicates = new ArrayList<>();
                List<Integer> paramValues = new ArrayList<>();

                cdsConfigChangedDataWithDesiredVendorSupport.subList(start, end).stream().forEach((cdsConfigChangedData)-> {
                    wherePredicates.add("(vendorId = ? AND procedureId = ?) ");
                    paramValues.add(cdsConfigChangedData.getVendorId());
                    paramValues.add(cdsConfigChangedData.getProcedureId());
                });

                StringBuilder removeSupportSql = new StringBuilder(removeSupportSqlPrefix.toString());
                removeSupportSql.append(StringUtils.join(wherePredicates, "or "));
                removeSupportSql.append(")");

                if (log.isDebugEnabled()) {
                    log.debug("removeSupportForVendors: " + removeSupportSql.toString() + " params: " + StringUtils.join(paramValues, ","));
                }

                persister.executeSql(removeSupportSql.toString(), paramValues.toArray());
            }
        }
    }
}
