package org.kevin.digitalTurbine.poem;

import static org.kevin.digitalTurbine.poem.RuleType.*;

public class PronounRule extends AbstractSentenceRule {
    public PronounRule(String ruleString) {
        super(PRONOUN, ruleString);
    }
}
