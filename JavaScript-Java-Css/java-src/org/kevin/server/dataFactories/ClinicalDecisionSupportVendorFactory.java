package org.kevin.server.dataFactories;

import com.mbs.rlManagers.valueObject.CdsVendorValue;
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

public class ClinicalDecisionSupportVendorFactory extends BasicFactory implements IFactory {

    private final static Logger log = LogManager.getLogger(ClinicalDecisionSupportVendorFactory.class);


    private static ClinicalDecisionSupportVendorFactory instance;


    public static ClinicalDecisionSupportVendorFactory getInstance() throws FactoryException {
        if (instance == null) {
            instance = new ClinicalDecisionSupportVendorFactory();
        }
        return instance;
    }


    private ClinicalDecisionSupportVendorFactory() {
        // NOTE: Do nothing here because it has been coded so that the developer has to call get All for the correct thing to happen.
        // And for you developers who have taken time to read this. It is a well known very bad habit to do real work in a constructor
        // In The First Place ***** One of the top smells documented in the clean code handbook is to never do real work in a constructor
    }


    public void clearCache() throws FactoryException {
        log.debug("clear the cache");
    }



    public List<CdsVendorValue> getAll(){
        List<CdsVendorValue> cdsVendors= new ArrayList<>();

        Connection connection = null;
        try {
            StringBuilder clinicalDecisionSupportVendorFactoryGetAll = new StringBuilder();
            clinicalDecisionSupportVendorFactoryGetAll.append("select * from ").append(getTableName()).append(Keys.SQL.NOLOCK);
            connection = DataSourceFactory.getFactory().getConnection();

            if (log.isDebugEnabled()) {
                log.debug("Executing ClinicalDecisionSupportVendorFactory:getAll: " + clinicalDecisionSupportVendorFactoryGetAll.toString() + " params: ");
            }

            try (PreparedStatement statement = connection.prepareStatement(clinicalDecisionSupportVendorFactoryGetAll.toString())) {
                try (ResultSet resultSet = statement.executeQuery() ) {
                    while (resultSet.next()) {
                        CdsVendorValue vendor = new CdsVendorValue();
                        vendor.setPrimaryKey(resultSet.getInt(1));
                        vendor.setName(SecurityTool.sanitizeText(resultSet.getString(2)));
                        cdsVendors.add(vendor);
                    }
                }
            }
        }
        catch(Exception ex){
            log.error("ClinicalDecisionSupportVendorFactory:getAll : Failed: params: :" + ex);
        }
        finally {
            DataSourceFactory.getFactory().closeConnectionIfSafe(connection);
        }
        return cdsVendors;
    }


    public void removeInstance() {
        instance = null;
    }


    public String getTableName() {
        return TableDefinition.CDSVENDOR_TABLE;
    }


    public Map getLocaleValues(Locale locale) {
        if (shortValueObjects == null) {
            shortValueObjects = new TreeMap();
        }

        Map localCdsVendors = null;
        if (shortValueObjects.containsKey(locale.toString())) {
            localCdsVendors = (Map) shortValueObjects.get(locale.toString());
        } else {
            localCdsVendors = new TreeMap();

            shortValueObjects.put(locale.toString(), localCdsVendors);
            Iterator it = valueObjects.values().iterator();
            for(CdsVendorValue vendor : getAll()) {
                ShortCdsVendorValue shortCdsVendorValue = new ShortCdsVendorValue();
                shortCdsVendorValue.setId(vendor.getId());
                shortCdsVendorValue.setBasicDescription(vendor.getName());
                shortCdsVendorValue.setRetired(false);
                localCdsVendors.put(vendor.getId(), shortCdsVendorValue);
            }
        }
        return localCdsVendors;
    }
}
