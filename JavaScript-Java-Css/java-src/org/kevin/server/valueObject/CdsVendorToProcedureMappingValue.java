package org.kevin.server.valueObject;

import com.mbs.rlData.dataLayer.metadata.TableDefinition;

/**
 * User: kechols
 * Date: 18-Dec-2015
 * Time: 3:20:59 PM
 *
 */
public class CdsVendorToProcedureMappingValue extends LookupValue implements ICacheValueObject {

    private int mappingId;


    private int procedureId;


    private int vendorId;


    public String getFactoryId() { return getId(); }


    public String getTableName() {
        return TableDefinition.CDSVENDOR_TO_PROCEDURE_MAPPING_TABLE;
    }


    public String getId() {
        return Integer.toString(mappingId);
    }


    public int getPrimaryKey() {
        return mappingId;
    }


    public void setPrimaryKey(int primaryKey) {
        setMappingId(primaryKey);
    }


    public int getMappingId() { return mappingId; }


    public void setMappingId(int mappingId) {
        this.mappingId = mappingId;
        setFactoryId(mappingId);
    }


    public int getProcedureId() { return procedureId; }


    public void setProcedureId(int procedureId) {
        this.procedureId = procedureId;
    }


    public int getVendorId() { return vendorId; }


    public void setVendorId(int vendorId) {
        this.vendorId = vendorId;
    }
}
