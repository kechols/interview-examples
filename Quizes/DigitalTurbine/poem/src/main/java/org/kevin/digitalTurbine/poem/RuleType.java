package org.kevin.digitalTurbine.poem;

public enum RuleType {
    POEM("POEM"), LINE("LINE"), ADJECTIVE("ADJECTIVE"), NOUN("NOUN"), PRONOUN("PRONOUN"), VERB("VERB"), PREPOSITION("PREPOSITION"), UNKNOWN("UNKNOWN");

    private static final String RULE_COLON = ":";
    private static final String RULE_REFERNCE = "<%s>";
    private final String displayName;

    private RuleType(String displayName) {
        this.displayName = displayName;
    }

    public String getDisplayName() {
        return displayName;
    }

    public String getRuleName() {
        return displayName + RULE_COLON;
    }

    public String getReferenceName() {
        return String.format(RULE_REFERNCE, displayName);
    }

    public static RuleType getType(String ruleString) {
        if (ruleString.contains(POEM.getRuleName())) {
            return POEM;
        }
        else if (ruleString.contains(LINE.getRuleName()) || ruleString.equals(LINE.displayName)) {
            return LINE;
        }
        else if (ruleString.contains(ADJECTIVE.getRuleName()) || ruleString.equals(ADJECTIVE.displayName)) {
            return ADJECTIVE;
        }
        else if (ruleString.contains(PRONOUN.getRuleName()) || ruleString.equals(PRONOUN.displayName)) {
            return PRONOUN;
        }
        else if (ruleString.contains(NOUN.getRuleName()) || ruleString.equals(NOUN.displayName)) {
            return NOUN;
        }
        else if (ruleString.contains(VERB.getRuleName()) || ruleString.equals(VERB.displayName)) {
            return VERB;
        }
        else if (ruleString.contains(PREPOSITION.getRuleName()) || ruleString.equals(PREPOSITION.displayName)) {
            return PREPOSITION;
        }
        else {
            return UNKNOWN;
        }
    }

    public static Rule getRule(String ruleString) {
        switch (getType(ruleString)) {
        case POEM:
            return new PoemRule(ruleString);
        case LINE:
            return new LineRule(ruleString);
        case ADJECTIVE:
            return new AdjectiveRule(ruleString);
        case NOUN:
            return new NounRule(ruleString);
        case PRONOUN:
            return new PronounRule(ruleString);
        case VERB:
            return new VerbRule(ruleString);
        case PREPOSITION:
            return new PrepositionRule(ruleString);

        default:
            throw new IllegalArgumentException("Unable to determine Rule for Rule String " + ruleString);
        }
    }
}
