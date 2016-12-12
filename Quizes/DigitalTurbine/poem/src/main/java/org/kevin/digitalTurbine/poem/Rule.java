package org.kevin.digitalTurbine.poem;

import java.util.List;

public interface Rule {

    public String getValue();

    public String getDefinition();

    public RuleType getType();

    public abstract List<Rule> getReferenceRules();

    public void setReferenceRule(String referenceRule);

    public String getReferenceRule();

    public List<KeyWordType> getKeyWords();

}