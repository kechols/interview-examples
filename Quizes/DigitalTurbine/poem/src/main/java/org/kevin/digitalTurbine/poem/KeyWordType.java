package org.kevin.digitalTurbine.poem;

public enum KeyWordType {
    END("END"), LINEBREAK("LINEBREAK"), UNKNOWN("UNKNOWN");

    private final String displayName;

    private KeyWordType(String displayName) {
        this.displayName = displayName;
    }

    public String getDisplayName() {
        return displayName;
    }

    public static KeyWordType getType(String keywordString) {
        if (keywordString.equals(END.getDisplayName())) {
            return END;
        }
        else if (keywordString.equals(LINEBREAK.getDisplayName())) {
            return LINEBREAK;
        }
        else {
            return UNKNOWN;
        }
    }
}
