package org.kevin.web.configureClinicalDecisionSupport;

/**
 * CdsConfigData.java
 * $Revision: 1.3 $
 * Date: 2016/2/04
 * Created by kechols
 */
public class CdsConfigData extends Object {

    private int procedureId;
    private boolean supported;
    private int vendorId;

    public CdsConfigData(){
    }

    public CdsConfigData(int procedureId, boolean supported, int vendorId){
        setProcedureId(procedureId);
        setSupported(supported);
        setVendorId(vendorId);
    }

    public int getProcedureId() { return procedureId; }

    public void setProcedureId(int procedureId) { this.procedureId = procedureId; }

    public boolean isSupported() { return supported; }

    public void setSupported(boolean supported) { this.supported = supported; }

    public int getVendorId() { return vendorId; }

    public void setVendorId(int vendorId) { this.vendorId = vendorId; }

    /**
     * Compares this to the specified object field by field
     */
    @Override
    public boolean equals(Object object) {
        // if argument is wrong class - return false
        if (object == null || !(object instanceof CdsConfigData)) {
            return false;
        }

        CdsConfigData cdsConfigData = (CdsConfigData) object;

        if ((cdsConfigData.isSupported() == this.isSupported()) &&
            (cdsConfigData.getProcedureId() == this.getProcedureId()) &&
            (cdsConfigData.getVendorId() == this.getVendorId())
            ) {
            return true;
        }
        else {
            return false;
        }
    }

}
