package org.kevin.digitalTurbine.poem;

import java.util.Arrays;
import java.util.List;

public abstract class AbstractSentenceRule extends AbstractRule implements SentenceRule {

    private static final String REFERENCE_RULE_MARKER = "<";

    public AbstractSentenceRule(RuleType ruleType, String ruleString) {
        super(ruleType, ruleString);
    }

    public List<String> getPossibleWords() {
        String possibleWordsString = getReferenceRule().substring(0, getReferenceRule().indexOf(REFERENCE_RULE_MARKER)).trim();
        return Arrays.asList(possibleWordsString.split("\\|"));
    }
}
