package org.kevin.web.configureClinicalDecisionSupport;

import org.apache.struts.action.ActionForm;

/**
 * cdsConfigForm.java
 *
 * Date: 2015/11/09 19:49:41
 * Created by kechols
 */
public final class CdsConfigForm extends ActionForm {

    private String cdsConfigGridData;

    private String cdsConfigGridVendorColumns;

    private String cdsConfigChangedData;

    private String originalPersistableCdsConfigData;

    public String getCdsConfigGridData() { return cdsConfigGridData; }

    public void setCdsConfigGridData(String cdsConfigDisplayData) { this.cdsConfigGridData = cdsConfigDisplayData; }

    public String getCdsConfigGridVendorColumns() { return cdsConfigGridVendorColumns;  }

    public void setCdsConfigGridVendorColumns(String cdsConfigGridVendorColumns) { this.cdsConfigGridVendorColumns = cdsConfigGridVendorColumns; }

    public String getCdsConfigChangedData() { return cdsConfigChangedData; }

    public void setCdsConfigChangedData(String cdsConfigChangedData) { this.cdsConfigChangedData = cdsConfigChangedData; }

    public String getOriginalPersistableCdsConfigData() { return originalPersistableCdsConfigData; }

    public void setOriginalPersistableCdsConfigData(String originalPersistableCdsConfigData) { this.originalPersistableCdsConfigData = originalPersistableCdsConfigData; }
}
