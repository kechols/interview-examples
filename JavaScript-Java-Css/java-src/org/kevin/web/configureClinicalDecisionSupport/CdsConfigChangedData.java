package org.kevin.web.configureClinicalDecisionSupport;

/**
 * CdsConfigChangedData.java
 * $Revision: 1.3 $
 * Date: 2015/12/15
 * Created by kechols
 */
public class CdsConfigChangedData extends Object {

    private int procedureId;
    private boolean supported;
    private int vendorId;

    public CdsConfigChangedData(){
    }

    public CdsConfigChangedData(boolean supported){
        setSupported(supported);
    }

    public int getProcedureId() { return procedureId; }

    public void setProcedureId(int procedureId) { this.procedureId = procedureId; }

    public boolean isSupported() { return supported; }

    public void setSupported(boolean supported) { this.supported = supported; }

    public int getVendorId() { return vendorId; }

    public void setVendorId(int vendorId) { this.vendorId = vendorId; }

    /**
     * Compares this to the specified object "supported" field.
     */
    @Override
    public boolean equals(Object object) {
        // if argument is wrong class - return false
        if (object == null || !(object instanceof CdsConfigChangedData)) {
            return false;
        }

        CdsConfigChangedData cdsConfigChangedData = (CdsConfigChangedData) object;

        if (cdsConfigChangedData.isSupported() == this.isSupported()) {
            return true;
        } else {
            return false;
        }
    }

}
