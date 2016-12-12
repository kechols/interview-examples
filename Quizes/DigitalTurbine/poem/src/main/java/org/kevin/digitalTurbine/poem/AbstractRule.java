package org.kevin.digitalTurbine.poem;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public abstract class AbstractRule implements Rule {

    private String value;
    private final RuleType type;
    private String referenceRule;
    private static final Pattern REFERENCE_RULE = Pattern.compile("\\<(.*?)\\>");
    private static final Pattern KEYWORD = Pattern.compile("\\$(.*)");

    public AbstractRule(RuleType ruleType, String ruleString) {
        this.type = ruleType;
        this.value = ruleString;
        if (value.length() > getType().getRuleName().length() - 1)
            setReferenceRule(value.substring(getType().getRuleName().length() - 1));
    }

    public String getDefinition() {
        return this.value.substring(this.type.getRuleName().length());
    }

    public String getValue() {
        return value;
    }

    public RuleType getType() {
        return type;
    }

    public String getReferenceRule() {
        return this.referenceRule;
    }

    public void setReferenceRule(String referenceRule) {
        this.referenceRule = referenceRule.trim().replaceFirst("\r", "").replaceFirst("\n", "").replaceFirst(":", "");
    }

    public List<Rule> getReferenceRules() {
        Matcher referenceRuleMatches = REFERENCE_RULE.matcher(getReferenceRule());
        ArrayList<Rule> referenceRules = new ArrayList<Rule>();
        while (referenceRuleMatches.find()) {
            referenceRules.add(RuleType.getRule(referenceRuleMatches.group(1)));
        }
        return referenceRules;
    }

    public List<KeyWordType> getKeyWords() {
        Matcher keyWordMatches = KEYWORD.matcher(getReferenceRule());
        ArrayList<KeyWordType> keyWords = new ArrayList<KeyWordType>();
        while (keyWordMatches.find()) {
            keyWords.add(KeyWordType.getType(keyWordMatches.group(1)));
        }
        return keyWords;
    }
}
