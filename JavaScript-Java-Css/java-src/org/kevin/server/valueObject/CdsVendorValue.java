package org.kevin.server.valueObject;

/**
 * User: kechols
 * Date: 24-Dec-2015
 * Time: 3:20:59 PM
 * To change this template use Options | File Templates.
 */
public class CdsVendorValue extends LookupValue implements ICacheValueObject {

    private int vendorId;
    private String name;

    public String getFactoryId() { return getId(); }

    public String getTableName() {
        return TableDefinition.CDSVENDOR_TABLE;
    }


    public String getName() {
        return name;
    }


    public void setName(String name) {
        this.name = name;
    }


    public String getId() {
        return Integer.toString(vendorId);
    }


    public void setPrimaryKey(int primaryKey) {
        setVendorId(primaryKey);
    }


    public int getPrimaryKey() {
        return vendorId;
    }

    public int getVendorId() { return vendorId; }


    public void setVendorId(int vendorId) {
        this.vendorId = vendorId;
    }


}
