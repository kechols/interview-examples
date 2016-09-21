package org.kevin.server.valueObject;

import  org.apache.commons.lang.NotImplementedException;

/**
 * User: kechols
 * Date: 9-Jan-2016
 * Time: 11:30:59 AM
 */
public class CdsModalityAndProcedureValue extends LookupValue implements ICacheValueObject {

    private int modalityId;
    private String modalityName;
    private int procedureId;
    private String procedureName;

    public String getFactoryId() { return getId(); }

    public String getId() { return Integer.toString(getProcedureId()); }

    public int getModalityId() { return this.modalityId; }

    public void setModalityId(int modalityId) { this.modalityId = modalityId; }

    public String getModalityName() {
        return modalityName;
    }

    public void setModalityName(String modalityName) {
        this.modalityName = modalityName;
    }

    public String getModalityTableName() { return TableDefinition.MODALITY_TABLE; }

    public int getPrimaryKey() {
        return getProcedureId();
    }

    public void setPrimaryKey(int primaryKey) {
        setProcedureId(primaryKey);
    }

    public int getProcedureId() { return procedureId; }

    public void setProcedureId(int procedureId) {
        this.procedureId = procedureId;
    }

    public String getProcedureName() {
        return procedureName;
    }

    public void setProcedureName(String procedureName) {
        this.procedureName = procedureName;
    }

    public String getProcedureTableName() { return TableDefinition.EXAMCODE_TABLE; }

    public String getTableName() { throw new NotImplementedException("Use getModalityTableName and getProcedureTableName instead"); }
}
