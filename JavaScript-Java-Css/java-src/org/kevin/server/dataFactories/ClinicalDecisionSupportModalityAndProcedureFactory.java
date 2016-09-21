package org.kevin.server.dataFactories;

import com.mbs.rlData.dataLayer.metadata.TableDefinition;
import com.mbs.rlManagers.valueObject.CdsModalityAndProcedureValue;
import org.apache.commons.lang.NotImplementedException;
import org.apache.commons.lang.StringUtils;
import org.apache.log4j.LogManager;
import org.apache.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.*;

/*
 * ClinicalDecisionSupportVendorFactory.java
 *
 * Creation :
 * User: kechols
 * Date: Dec 20, 2015
 * Time: 2:38:47 PM
 *
 */

public class ClinicalDecisionSupportModalityAndProcedureFactory extends BasicFactory implements IFactory {

    private final static Logger log = LogManager.getLogger(ClinicalDecisionSupportModalityAndProcedureFactory.class);

    private static ClinicalDecisionSupportModalityAndProcedureFactory instance;

    public static ClinicalDecisionSupportModalityAndProcedureFactory getInstance() throws FactoryException {
        if (instance == null) {
            instance = new ClinicalDecisionSupportModalityAndProcedureFactory();
        }
        return instance;
    }


    private ClinicalDecisionSupportModalityAndProcedureFactory() {
        // NOTE: Do nothing here because it has been coded so that the developer has to call get All for the correct thing to happen.
        // NOTE: Read comments at the top of getAll() for more details
        // And for you developers who have taken time to read this. It is a well known very bad habit to do real work in a constructor
        // In The First Place ***** One of the top smells documented in the clean code handbook is to never do real work in a constructor
    }


    public void clearCache() throws FactoryException {
        log.debug("clear the cache");
        // The implementation of getting data in the getAll method of this class does not store any data
        // in any fields of this class. The perfomrance is fast enough that it's not needed. So there is no implementation code
        // that typically clears the cache and then retrieves new data that is needed.
    }


    public void removeInstance() {
        instance = null;
    }


    public List<CdsModalityAndProcedureValue> getAll(){
        List<CdsModalityAndProcedureValue> cdsModalityAndProcedureValues= new ArrayList<>();

        StringBuilder cdsModalityAndProcedureGetAllSql = new StringBuilder();
        Connection connection = null;
        try {
            List <String> cdsModalityAndProcedureGetAllColumns = Arrays.asList(
                "modality.examtype_no as modalityId",
                "modality.desc_e as modalityName",
                "procedure_table.examcode_no as procedureId",
                "procedure_table.desc_e as procedureName"
            );

            List <String> cdsModalityAndProcedureGetAllTables = Arrays.asList(
                getModalityTableName() + " modality",
                getProcedureTableName() + " procedure_table"
            );

            List<String> wherePredicates = Arrays.asList(
                "(modality.examtype_no = procedure_table.examtype_no)",
                "(modality.Retired is null or modality.Retired = 0)",
                "(procedure_table.Retired is null or procedure_table.Retired = 0)",
                "(datalength(modality.desc_e) <> 0)",
                "(datalength(procedure_table.desc_e) <> 0)"
            );

            List <String> cdsModalityAndProcedureGetAllGroupByColumns = Arrays.asList(
                "modality.desc_e",
                "procedure_table.desc_e",
                "procedure_table.examcode_no",
                "modality.examtype_no"
            );

            List <String> cdsModalityAndProcedureGetAllOrderByColumns = Arrays.asList(
                "modality.desc_e asc",
                "procedure_table.desc_e asc"
            );

            cdsModalityAndProcedureGetAllSql.append("select ");
            cdsModalityAndProcedureGetAllSql.append(StringUtils.join(cdsModalityAndProcedureGetAllColumns, ", "));
            cdsModalityAndProcedureGetAllSql.append(" from ");
            cdsModalityAndProcedureGetAllSql.append(StringUtils.join(cdsModalityAndProcedureGetAllTables, ", "));
            cdsModalityAndProcedureGetAllSql.append(" where ");
            cdsModalityAndProcedureGetAllSql.append(StringUtils.join(wherePredicates, " And "));
            cdsModalityAndProcedureGetAllSql.append(" group by ");
            cdsModalityAndProcedureGetAllSql.append(StringUtils.join(cdsModalityAndProcedureGetAllGroupByColumns, ", "));
            cdsModalityAndProcedureGetAllSql.append(" order by ");
            cdsModalityAndProcedureGetAllSql.append(StringUtils.join(cdsModalityAndProcedureGetAllOrderByColumns, ", "));

            if (log.isDebugEnabled()) {
                log.debug("Executing ClinicalDecisionSupportModalityAndProcedureFactory:getAll: " + cdsModalityAndProcedureGetAllSql.toString() + " params: ");
            }

            connection = DataSourceFactory.getFactory().getConnection();

            try (PreparedStatement statement = connection.prepareStatement(cdsModalityAndProcedureGetAllSql.toString())) {
                try (ResultSet resultSet = statement.executeQuery() ) {
                    while (resultSet.next()) {
                        CdsModalityAndProcedureValue value = new CdsModalityAndProcedureValue();
                        value.setPrimaryKey(resultSet.getInt("procedureId"));
                        value.setModalityId(resultSet.getInt("modalityId"));
                        value.setProcedureName(resultSet.getString("procedureName"));
                        value.setModalityName(resultSet.getString("modalityName"));
                        cdsModalityAndProcedureValues.add(value);
                    }
                }
            }
        }
        catch(Exception ex){
            log.error("ClinicalDecisionSupportModalityAndProcedureFactory:getAll failed:  " + cdsModalityAndProcedureGetAllSql.toString() + " : ", ex);
        }
        finally {
            DataSourceFactory.getFactory().closeConnectionIfSafe(connection);
        }
        return cdsModalityAndProcedureValues;
    }


    public String getTableName() { throw new NotImplementedException("Use getModalityTableName and getProcedureTableName instead"); }


    public Map getLocaleValues(Locale locale) { throw new NotImplementedException("Use getAll instead"); }


    private String getModalityTableName() { return TableDefinition.MODALITY_TABLE; }


    private String getProcedureTableName() { return TableDefinition.EXAMCODE_TABLE; }
}
